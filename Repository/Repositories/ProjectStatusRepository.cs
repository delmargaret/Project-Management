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
    public class ProjectStatusRepository : IProjectStatusRepository
    {
        private ManagementContext db;

        public ProjectStatusRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<ProjectStatus> GetAllProjectStatuses()
        {
            if (db.ProjectStatuses.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ProjectStatuses;
        }

        public ProjectStatus GetProjectStatusById(int id)
        {
            if (db.ProjectStatuses.Find(id) == null)
            {
                throw new NotFoundException();
            }
            return db.ProjectStatuses.Find(id);
        }

        public ProjectStatus CreateProjectStatus(ProjectStatus projectstatus)
        {
            var ps = db.ProjectStatuses.Add(projectstatus);
            return ps;
        }

        public void UpdateProjectStatus(ProjectStatus projectstatus)
        {
            db.Entry(projectstatus).State = EntityState.Modified;
        }

        public IEnumerable<ProjectStatus> FindProjectStatus(Func<ProjectStatus, Boolean> predicate)
        {
            if (db.ProjectStatuses.Where(predicate).ToList().Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ProjectStatuses.Where(predicate).ToList();
        }

        public void DeleteProjectStatus(int id)
        {
            ProjectStatus projectstatus = db.ProjectStatuses.Find(id);
            if (projectstatus == null)
            {
                throw new NotFoundException();
            }
                db.ProjectStatuses.Remove(projectstatus);
        }
    }
}