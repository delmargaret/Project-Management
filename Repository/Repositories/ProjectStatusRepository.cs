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

        public IEnumerable<ProjectStatus> GetAll()
        {
            return db.ProjectStatuses;
        }

        public ProjectStatus Get(int id)
        {
            return db.ProjectStatuses.Find(id);
        }

        public void Create(ProjectStatus projectstatus)
        {
            db.ProjectStatuses.Add(projectstatus);
        }

        public void Update(ProjectStatus projectstatus)
        {
            db.Entry(projectstatus).State = EntityState.Modified;
        }

        public IEnumerable<ProjectStatus> Find(Func<ProjectStatus, Boolean> predicate)
        {
            return db.ProjectStatuses.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            ProjectStatus projectstatus = db.ProjectStatuses.Find(id);
            if (projectstatus != null)
                db.ProjectStatuses.Remove(projectstatus);
        }
    }
}
