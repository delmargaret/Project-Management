using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;
using Exeption;

namespace Repository.Repositories 
{
    public class ProjectRepository : IProjectRepository
    {
        private ManagementContext db;

        public ProjectRepository(ManagementContext context)
        {
            this.db = context;
        }

        public void FindSameProject(string projName, string projDescr, DateTimeOffset start, DateTimeOffset end, int projStatus)
        {
            List<Project> list = new List<Project>();
            list = db.Projects.Where(item => item.ProjectName == projName && 
            item.ProjectDescription == projDescr && item.ProjectStartDate == start &&
            item.ProjectEndDate == end && item.ProjectStatusId == projStatus).ToList();
            if (list.Count != 0)
            {
                throw new ObjectAlreadyExistsException();
            }
        }

        public IEnumerable<Project> GetAllProjects()
        {
            if (db.Projects.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Projects;
        }

        public IEnumerable<Project> SortByNameAsc()
        {
            if (db.Projects.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Projects.OrderBy(item => item.ProjectName);
        }

        public IEnumerable<Project> SortByNameDesc()
        {
            if (db.Projects.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Projects.OrderByDescending(item => item.ProjectName);
        }

        public IEnumerable<Project> SortByStartDateAsc()
        {
            if (db.Projects.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Projects.OrderBy(item => item.ProjectStartDate);
        }

        public IEnumerable<Project> SortByStartDateDesc()
        {
            if (db.Projects.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Projects.OrderByDescending(item => item.ProjectStartDate);
        }

        public IEnumerable<Project> SortByEndDateAsc()
        {
            if (db.Projects.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Projects.OrderBy(item => item.ProjectEndDate);
        }

        public IEnumerable<Project> SortByEndDateDesc()
        {
            if (db.Projects.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Projects.OrderByDescending(item => item.ProjectEndDate);
        }

        public IEnumerable<Project> SortByStatusAsc()
        {
            if (db.Projects.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Projects.OrderBy(item => item.ProjectStatusId);
        }

        public IEnumerable<Project> SortByStatusDesc()
        {
            if (db.Projects.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Projects.OrderByDescending(item => item.ProjectStatusId);
        }

        public IEnumerable<Project> GetAllProjectsByStatusId(int projectStatusId)
        {
            if(db.Projects.Where(item => item.ProjectStatusId == projectStatusId).Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Projects.Where(item => item.ProjectStatusId == projectStatusId);
        }

        public IEnumerable<Project> GetProjectsEndingInNDays(int numberOfDays)
        {
            List<Project> list = new List<Project>();
            var projects = db.Projects;
            foreach(var project in projects)
            {
                if(Math.Abs((DateTimeOffset.Now).Subtract(project.ProjectEndDate).TotalDays) <= numberOfDays)
                {
                    list.Add(db.Projects.Find(project.Id));
                }
            }
            if (list.Count() == 0)
            {
                throw new NotFoundException();
            }
            return list;
        }

        public Project GetProjectById(int id)
        {
            if (db.Projects.Find(id) == null)
            {
                throw new NotFoundException();
            }
            return db.Projects.Find(id);
        }

        public Project CreateProject(Project project)
        {
            var pr = db.Projects.Add(project);
            return pr;
        }

        public void UpdateProject(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
        }

        public IEnumerable<Project> FindProject(Func<Project, Boolean> predicate)
        {
            if (db.Projects.Where(predicate).ToList().Count() == 0) 
            {
                throw new NotFoundException();
            }
            return db.Projects.Where(predicate).ToList();
        }

        public void DeleteProjectById(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                throw new NotFoundException();
            }
                db.Projects.Remove(project);
        }

        public void ChangeProjectName(int projectId, string newProjectName)
        {
            Project project = db.Projects.Find(projectId);
            if (project == null)
            {
                throw new NotFoundException();
            }
                project.ProjectName = newProjectName;
        }

        public void ChangeProjectDescription(int projectId, string newProjectDescription)
        {
            Project project = db.Projects.Find(projectId);
            if (project == null)
            {
                throw new NotFoundException();
            }
                project.ProjectDescription = newProjectDescription;
        }

        public void ChangeProjectStartDate(int projectId, DateTimeOffset newStartDate)
        {
            Project project = db.Projects.Find(projectId);
            if (project == null)
            {
                throw new NotFoundException();
            }
                project.ProjectStartDate = newStartDate;
        }

        public void ChangeProjectEndDate(int projectId, DateTimeOffset newEndDate)
        {
            Project project = db.Projects.Find(projectId);
            if (project == null)
            {
                throw new NotFoundException();
            }
                project.ProjectEndDate = newEndDate;
        }

        public void ChangeProjectStatus(int projectId, int projectStatusId)
        {
            Project project = db.Projects.Find(projectId);
            if (project == null)
            {
                throw new NotFoundException();
            }
            project.ProjectStatusId = projectStatusId;
            project.ProjectStatus = db.ProjectStatuses.Find(projectStatusId);
        }

        public void CloseProject(int projectId)
        {
            Project project = db.Projects.Find(projectId);
            if (project == null)
            {
                throw new NotFoundException();
            }
            project.ProjectStatusId = 2;
            project.ProjectStatus = db.ProjectStatuses.Find(2);
            project.ProjectEndDate = DateTimeOffset.Now;
        }

    }
}
