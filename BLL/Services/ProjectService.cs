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
using BLL.Infrastructure;

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

        public void ChangeProjectDescription(int? projectId, string newProjectDescription)
        {
            if (projectId == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            Database.Projects.ChangeProjectDescription(projectId.Value, newProjectDescription);
            Database.Save();
        }

        public void ChangeProjectEndDate(int? projectId, DateTimeOffset? newEndDate)
        {
            if (projectId == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            if (newEndDate == null)
            {
                throw new ProjectException("Не установлена дата окончания проекта");
            }
            if (project.ProjectStartDate > newEndDate)
            {
                throw new ProjectException("Неверная дата окончания проекта");
            }
            Database.Projects.ChangeProjectEndDate(projectId.Value, newEndDate.Value);
            Database.Save();
        }

        public void ChangeProjectName(int? projectId, string newProjectName)
        {
            if (projectId == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            Database.Projects.ChangeProjectName(projectId.Value, newProjectName);
            Database.Save();
        }

        public void ChangeProjectStartDate(int? projectId, DateTimeOffset? newStartDate)
        {
            if (projectId == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            if (newStartDate == null)
            {
                throw new ProjectException("Не установлена дата начала проекта");
            }
            if (project.ProjectEndDate < newStartDate)
            {
                throw new ProjectException("Неверная дата начала проекта");
            }
            Database.Projects.ChangeProjectStartDate(projectId.Value, newStartDate.Value);
            Database.Save();
        }

        public void ChangeProjectStatus(int? projectId, int? projectStatusId)
        {
            if (projectId == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            if (projectStatusId == null)
            {
                throw new ProjectException("Не установлен идентификатор статуса проекта");
            }
            var projectStatus = Database.ProjectStatuses.GetProjectStatusById(projectStatusId.Value);
            if (projectStatus == null)
            {
                throw new ProjectException("Статус проекта не найден");
            }
            Database.Projects.ChangeProjectStatus(projectId.Value, projectStatusId.Value);
            Database.Save();
        }

        public void CreateProject(ProjectDTO item)
        {
            ProjectStatus projectStatus = Database.ProjectStatuses.GetProjectStatusById(1);

            if (projectStatus == null)
            {
                throw new ProjectException("Статус проекта не найден");
            }
            if (item.ProjectStartDate > item.ProjectEndDate)
            {
                throw new ProjectException("Неверные даты");
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

            Database.Projects.CreateProject(project);
            Database.Save();
        }

        public void DeleteProjectById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(id.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            Database.Projects.DeleteProjectById(id.Value);
            Database.Save();
        }

        public IEnumerable<ProjectDTO> GetAllProjectsByStatusId(int? statusId)
        {
            if (statusId == null)
            {
                throw new ProjectException("Не установлен идентификатор статуса проекта");
            }
            var projectStatus = Database.ProjectStatuses.GetProjectStatusById(statusId.Value);
            if (projectStatus == null)
            {
                throw new ProjectException("Статус проекта не найден");
            }
            var projects = Database.Projects.GetAllProjectsByStatusId(statusId.Value);
            if (projects.Count() == 0)
            {
                throw new ProjectException("Проекты не найдены");
            }
            return Map.ListMap(projects);
        }

        public IEnumerable<ProjectDTO> GetAllProjects()
        {
            var projects = Database.Projects.GetAllProjects();
            if (projects.Count() == 0)
            {
                throw new ProjectException("Проекты не найдены");
            }
            return Map.ListMap(projects);
        }

        public ProjectDTO GetProjectById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(id.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            return Map.ObjectMap(project);
        }

        public IEnumerable<ProjectDTO> GetProjectsEndingInNDays(int? numberOfDays)
        {
            if (numberOfDays == null)
            {
                throw new ProjectException("Не установлено количество дней");
            }
            var projects = Database.Projects.GetProjectsEndingInNDays(numberOfDays.Value);
            if (projects.Count() == 0)
            {
                throw new ProjectException("Проекты не найдены");
            }
            return Map.ListMap(projects);
        }

        public void CloseProject(int? projectId)
        {
            if (projectId == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            Database.Projects.CloseProject(projectId.Value);
            Database.Save();
        }

    }
}
