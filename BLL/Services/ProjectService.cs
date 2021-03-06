﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Repository.Interfaces;
using BLL.Mapping;
using Exeption;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        IUnitOfWork Database { get; set; }
        Map<Project, ProjectDTO> Map { get; set; }

        public ProjectService(IUnitOfWork uow, Map<Project, ProjectDTO> map)
        {
            Database = uow;
            Map = map;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void ChangeProjectDescription(int projectId, string newProjectDescription)
        {
            var project = Database.Projects.GetProjectById(projectId);
            Database.Projects.ChangeProjectDescription(project.Id, newProjectDescription);
            Database.Save();
        }

        public void ChangeProjectEndDate(int projectId, DateTimeOffset newEndDate)
        {
            var project = Database.Projects.GetProjectById(projectId);
            if (project.ProjectStartDate > newEndDate)
            {
                throw new InvalidDateException();
            }
            Database.Projects.ChangeProjectEndDate(project.Id, newEndDate);
            Database.Save();
        }

        public void ChangeProjectName(int projectId, string newProjectName)
        {
            var project = Database.Projects.GetProjectById(projectId);
            Database.Projects.ChangeProjectName(project.Id, newProjectName);
            Database.Save();
        }

        public void ChangeProjectStartDate(int projectId, DateTimeOffset newStartDate)
        {
            var project = Database.Projects.GetProjectById(projectId);
            if (project.ProjectEndDate < newStartDate)
            {
                throw new InvalidDateException();
            }
            Database.Projects.ChangeProjectStartDate(project.Id, newStartDate);
            Database.Save();
        }

        public void ChangeProjectStatus(int projectId, int projectStatusId)
        {
            var project = Database.Projects.GetProjectById(projectId);
            var projectStatus = Database.ProjectStatuses.GetProjectStatusById(projectStatusId);
            Database.Projects.ChangeProjectStatus(project.Id, projectStatus.Id);
            Database.Save();
        }

        public ProjectDTO CreateProject(ProjectDTO item)
        {
            ProjectStatus projectStatus = Database.ProjectStatuses.GetProjectStatusById(1);
            if (item.ProjectStartDate > item.ProjectEndDate)
            {
                throw new InvalidDateException();
            }
            Project project = new Project
            {
                ProjectName = item.ProjectName,
                ProjectDescription = item.ProjectDescription,
                ProjectStartDate = item.ProjectStartDate,
                ProjectEndDate = item.ProjectEndDate,
                ProjectStatusId = 1,
                ProjectStatus = projectStatus
            };

            var proj = Database.Projects.CreateProject(project);
            Database.Save();

            return Map.ObjectMap(proj);
        }

        public void DeleteProjectById(int id)
        {
            var project = Database.Projects.GetProjectById(id);
            Database.Projects.DeleteProjectById(project.Id);
            Database.Save();
        }

        public IEnumerable<ProjectDTO> GetAllProjectsByStatusId(int statusId)
        {
            var projectStatus = Database.ProjectStatuses.GetProjectStatusById(statusId);
            var projects = Database.Projects.GetAllProjectsByStatusId(projectStatus.Id);
            return Map.ListMap(projects);
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            var projects = Database.Projects.GetAllProjects();
            return Map.ListMap(projects);
        }

        public ProjectDTO GetProjectById(int id)
        {
            var project = Database.Projects.GetProjectById(id);
            return Map.ObjectMap(project);
        }

        public IEnumerable<ProjectDTO> GetProjectsEndingInNDays(int numberOfDays)
        {
            var projects = Database.Projects.GetProjectsEndingInNDays(numberOfDays);
            return Map.ListMap(projects);
        }

        public void CloseProject(int projectId)
        {
            var project = Database.Projects.GetProjectById(projectId);
            Database.Projects.CloseProject(project.Id);
            Database.Save();
        }
    }
}
