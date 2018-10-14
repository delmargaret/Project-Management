using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;

namespace Repository.Repositories 
{
    public class ProjectRepository : IProjectRepository
    {
        private ManagementContext db;

        public ProjectRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return db.Projects;
        }

        public IEnumerable<Project> GetAllOpenedProjects()
        {
            return db.Projects.Where(item=>item.ProjectStatusId==1);
        }

        public IEnumerable<Project> GetAllClosedProjects()
        {
            return db.Projects.Where(item => item.ProjectStatusId == 2);
        }

        public List<Project> GetProjectsEndingInNDays(int numberOfDays)
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
            return list;
        }

        public Project GetProjectById(int id)
        {
            return db.Projects.Find(id);
        }

        public void CreateProject(Project project)
        {
            db.Projects.Add(project);
        }

        public void UpdateProject(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
        }

        public IEnumerable<Project> FindProject(Func<Project, Boolean> predicate)
        {
            return db.Projects.Where(predicate).ToList();
        }

        public void DeleteProjectById(int id)
        {
            Project project = db.Projects.Find(id);
            if (project != null)
                db.Projects.Remove(project);
        }

        public void ChangeProjectName(int projectId, string newProjectName)
        {
            Project project = db.Projects.Find(projectId);
            if (project != null)
                project.ProjectName = newProjectName;
        }

        public void ChangeProjectDescription(int projectId, string newProjectDescription)
        {
            Project project = db.Projects.Find(projectId);
            if (project != null)
                project.ProjectDescription = newProjectDescription;
        }

        public void ChangeProjectStartDate(int projectId, DateTimeOffset newStartDate)
        {
            Project project = db.Projects.Find(projectId);
            if (project != null)
                project.ProjectStartDate = newStartDate;
        }

        public void ChangeProjectEndDate(int projectId, DateTimeOffset newEndDate)
        {
            Project project = db.Projects.Find(projectId);
            if (project != null)
                project.ProjectEndDate = newEndDate;
        }

        public void ChangeProjectStatus(int projectId, int projectStatusId)
        {
            Project project = db.Projects.Find(projectId);
            if (project != null)
            {
                project.ProjectStatusId = projectStatusId;
                project.ProjectStatus = db.ProjectStatuses.Find(projectStatusId);
            }
        }

    }
}
