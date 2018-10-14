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
    public class ProjectStatusRepository : IProjectStatusRepository
    {
        private ManagementContext db;

        public ProjectStatusRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<ProjectStatus> GetAllProjectStatuses()
        {
            return db.ProjectStatuses;
        }

        public ProjectStatus GetProjectStatusById(int id)
        {
            return db.ProjectStatuses.Find(id);
        }

        public void CreateProjectStatus(ProjectStatus projectstatus)
        {
            db.ProjectStatuses.Add(projectstatus);
        }

        public void UpdateProjectStatus(ProjectStatus projectstatus)
        {
            db.Entry(projectstatus).State = EntityState.Modified;
        }

        public IEnumerable<ProjectStatus> FindProjectStatus(Func<ProjectStatus, Boolean> predicate)
        {
            return db.ProjectStatuses.Where(predicate).ToList();
        }

        public void DeleteProjectStatus(int id)
        {
            ProjectStatus projectstatus = db.ProjectStatuses.Find(id);
            if (projectstatus != null)
                db.ProjectStatuses.Remove(projectstatus);
        }
    }
}