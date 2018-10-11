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

        public IEnumerable<Project> GetAll()
        {
            return db.Projects;
        }

        public Project Get(int id)
        {
            return db.Projects.Find(id);
        }

        public void Create(Project project)
        {
            db.Projects.Add(project);
        }

        public void Update(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
        }

        public IEnumerable<Project> Find(Func<Project, Boolean> predicate)
        {
            return db.Projects.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Project project = db.Projects.Find(id);
            if (project != null)
                db.Projects.Remove(project);
        }
    }
}
