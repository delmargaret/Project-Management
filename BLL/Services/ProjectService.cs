using System;
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
        Map<Project, ProjectDTO> Map = new Map<Project, ProjectDTO>();

        public ProjectService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void ChangeProjectDescription(int projectId, string newProjectDescription)
        {
            Database.Projects.ChangeProjectDescription(projectId, newProjectDescription);
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
            Database.Projects.ChangeProjectName(projectId, newProjectName);
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
            Database.Projects.ChangeProjectStatus(projectId, projectStatusId);
            Database.Save();
        }

        public ProjectDTO CreateProject(ProjectDTO item)
        {
            ProjectStatus projectStatus = Database.ProjectStatuses.GetProjectStatusById(1);
            if (item.ProjectStartDate > item.ProjectEndDate)
            {
                throw new InvalidDateException();
            }
            Database.Projects.FindSameProject(item.ProjectName, item.ProjectDescription, item.ProjectStartDate,
                item.ProjectEndDate, item.ProjectStatusId);
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
            Database.Projects.DeleteProjectById(id);
            Database.Save();
        }

        public IEnumerable<ProjectDTO> GetAllProjectsByStatusId(int statusId)
        {
            var projects = Database.Projects.GetAllProjectsByStatusId(statusId);
            return Map.ListMap(projects);
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            var projects = Database.Projects.GetAllProjects();
            return Map.ListMap(projects);
        }

        public IEnumerable<ProjectDTO> SortByNameAsc()
        {
            var projects = Database.Projects.SortByNameAsc();
            return Map.ListMap(projects);
        }

        public IEnumerable<ProjectDTO> SortByNameDesc()
        {
            var projects = Database.Projects.SortByNameDesc();
            return Map.ListMap(projects);
        }

        public IEnumerable<ProjectDTO> SortByStartDateAsc()
        {
            var projects = Database.Projects.SortByStartDateAsc();
            return Map.ListMap(projects);
        }
        public IEnumerable<ProjectDTO> SortByStartDateDesc()
        {
            var projects = Database.Projects.SortByStartDateDesc();
            return Map.ListMap(projects);
        }

        public IEnumerable<ProjectDTO> SortByEndDateAsc()
        {
            var projects = Database.Projects.SortByEndDateAsc();
            return Map.ListMap(projects);
        }

        public IEnumerable<ProjectDTO> SortByEndDateDesc()
        {
            var projects = Database.Projects.SortByEndDateDesc();
            return Map.ListMap(projects);
        }

        public IEnumerable<ProjectDTO> SortByStatusAsc()
        {
            var projects = Database.Projects.SortByStatusAsc();
            return Map.ListMap(projects);
        }

        public IEnumerable<ProjectDTO> SortByStatusDesc()
        {
            var projects = Database.Projects.SortByStatusDesc();
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
            Database.Projects.CloseProject(projectId);
            Database.Save();
        }
    }
}
