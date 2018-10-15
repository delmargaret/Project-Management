﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Repository.Interfaces;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        IUnitOfWork Database { get; set; }

        public ProjectService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void ChangeProjectDescription(int? projectId, string newProjectDescription)
        {
            if (projectId == null)
            {
                Console.WriteLine("не установлено id проекта");
                return;
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                Console.WriteLine("проекта не существует");
                return;
            }
            Database.Projects.ChangeProjectDescription(projectId.Value, newProjectDescription);
            Database.Save();
        }

        public void ChangeProjectEndDate(int? projectId, DateTimeOffset? newEndDate)
        {
            if (projectId == null)
            {
                Console.WriteLine("не установлено id проекта");
                return;
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                Console.WriteLine("проекта не существует");
                return;
            }
            if (newEndDate == null)
            {
                Console.WriteLine("не установлена дата окончания проекта");
                return;
            }
            Database.Projects.ChangeProjectEndDate(projectId.Value, newEndDate.Value);
            Database.Save();
        }

        public void ChangeProjectName(int? projectId, string newProjectName)
        {
            if (projectId == null)
            {
                Console.WriteLine("не установлено id проекта");
                return;
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                Console.WriteLine("проекта не существует");
                return;
            }
            Database.Projects.ChangeProjectName(projectId.Value, newProjectName);
            Database.Save();
        }

        public void ChangeProjectStartDate(int? projectId, DateTimeOffset? newStartDate)
        {
            if (projectId == null)
            {
                Console.WriteLine("не установлено id проекта");
                return;
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                Console.WriteLine("проекта не существует");
                return;
            }
            if (newStartDate == null)
            {
                Console.WriteLine("не установлена дата начала проекта");
                return;
            }
            Database.Projects.ChangeProjectStartDate(projectId.Value, newStartDate.Value);
            Database.Save();
        }

        public void ChangeProjectStatus(int? projectId, int? projectStatusId)
        {
            if (projectId == null)
            {
                Console.WriteLine("не установлено id проекта");
                return;
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                Console.WriteLine("проекта не существует");
                return;
            }
            if (projectStatusId == null)
            {
                Console.WriteLine("не установлено id статуса проекта");
                return;
            }
            var projectStatus = Database.ProjectStatuses.GetProjectStatusById(projectStatusId.Value);
            if (projectStatus == null)
            {
                Console.WriteLine("статуса проекта не существует");
                return;
            }
            Database.Projects.ChangeProjectStatus(projectId.Value, projectStatusId.Value);
            Database.Save();
        }

        public void CreateProject(ProjectDTO item)
        {
            ProjectStatus projectStatus = Database.ProjectStatuses.GetProjectStatusById(item.ProjectStatusId);

            if (projectStatus == null)
            {
                Console.WriteLine("статуса проекта не существует");
                return;
            }
            Project project = new Project
            {
                ProjectName = item.ProjectName,
                ProjectDescription = item.ProjectDescription,
                ProjectStartDate = item.ProjectStartDate,
                ProjectEndDate = item.ProjectEndDate,
                ProjectStatusId = item.ProjectStatusId,
                ProjectStatus = projectStatus
            };

            Database.Projects.CreateProject(project);
            Database.Save();
        }

        public void DeleteProjectById(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("не установлено id проекта");
                return;
            }
            var project = Database.Projects.GetProjectById(id.Value);
            if (project == null)
            {
                Console.WriteLine("проекта не существует");
                return;
            }
            Database.Projects.DeleteProjectById(id.Value);
            Database.Save();
        }

        public IEnumerable<ProjectDTO> GetAllClosedProjects()
        {
            var projects = Database.Projects.GetAllClosedProjects();
            if (projects.Count() == 0)
            {
                Console.WriteLine("закрытых проектов не найдено");
                return null;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Project>, List<ProjectDTO>>(Database.Projects.GetAllClosedProjects());
        }

        public IEnumerable<ProjectDTO> GetAllOpenedProjects()
        {
            var projects = Database.Projects.GetAllOpenedProjects();
            if (projects.Count() == 0)
            {
                Console.WriteLine("открытых проектов не найдено");
                return null;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Project>, List<ProjectDTO>>(Database.Projects.GetAllOpenedProjects());
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            var projects = Database.Projects.GetAllProjects();
            if (projects.Count() == 0)
            {
                Console.WriteLine("проектов не найдено");
                return null;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Project>, List<ProjectDTO>>(Database.Projects.GetAllProjects());
        }

        public ProjectDTO GetProjectById(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("не указан id проекта");
                return null;
            }
            var project = Database.Projects.GetProjectById(id.Value);
            if (project == null)
            {
                Console.WriteLine("проект не найден");
                return null;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<Project, ProjectDTO>(Database.Projects.GetProjectById(id.Value));
        }

        public List<ProjectDTO> GetProjectsEndingInNDays(int? numberOfDays)
        {
            if (numberOfDays == null)
            {
                Console.WriteLine("не установлено количество дней");
                return null;
            }
            var projects = Database.Projects.GetProjectsEndingInNDays(numberOfDays.Value);
            if (projects.Count == 0)
            {
                Console.WriteLine("проекты не найдены");
                return null;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<List<Project>, List<ProjectDTO>>(Database.Projects.GetProjectsEndingInNDays(numberOfDays.Value));
        }
    }
}
