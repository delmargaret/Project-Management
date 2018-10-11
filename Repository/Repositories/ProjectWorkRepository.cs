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
    public class ProjectWorkRepository : IProjectWorkRepository
    {
        private ManagementContext db;

        public ProjectWorkRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<ProjectWork> GetAll()
        {
            return db.ProjectWorks;
        }

        public ProjectWork Get(int id)
        {
            return db.ProjectWorks.Find(id);
        }

        public void Create(ProjectWork projectwork)
        {
            db.ProjectWorks.Add(projectwork);
        }

        public void Update(ProjectWork projectwork)
        {
            db.Entry(projectwork).State = EntityState.Modified;
        }

        public IEnumerable<ProjectWork> Find(Func<ProjectWork, Boolean> predicate)
        {
            return db.ProjectWorks.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            ProjectWork projectwork = db.ProjectWorks.Find(id);
            if (projectwork != null)
                db.ProjectWorks.Remove(projectwork);
        }
    }
}
