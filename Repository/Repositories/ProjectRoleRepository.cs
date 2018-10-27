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
    public class ProjectRoleRepository : IProjectRoleRepository
    {
        private ManagementContext db;

        public ProjectRoleRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<ProjectRole> GetAllProjectRoles()
        {
            if (db.ProjectRoles.Count() == 0) 
            {
                throw new NotFoundException();
            }
            return db.ProjectRoles;
        }

        public ProjectRole GetProjectRoleById(int id)
        {
            if (db.ProjectRoles.Find(id) == null)
            {
                throw new NotFoundException();
            }
            return db.ProjectRoles.Find(id);
        }

        public ProjectRole CreateProjectRole(ProjectRole projectrole)
        {
            var pr = db.ProjectRoles.Add(projectrole);
            return pr;
        }

        public void UpdateProjectRole(ProjectRole projectrole)
        {
            db.Entry(projectrole).State = EntityState.Modified;
        }

        public IEnumerable<ProjectRole> FindProjectRole(Func<ProjectRole, Boolean> predicate)
        {
            if (db.ProjectRoles.Where(predicate).ToList().Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ProjectRoles.Where(predicate).ToList();
        }

        public void DeleteProjectRole(int id)
        {
            ProjectRole projectrole = db.ProjectRoles.Find(id);
            if (projectrole == null)
            {
                throw new NotFoundException();
            }
                db.ProjectRoles.Remove(projectrole);
        }

        public void ChangeProjectRoleName(int projectRoleId, string projectRoleName)
        {
            ProjectRole role = db.ProjectRoles.Find(projectRoleId);
            if (role == null)
            {
                throw new NotFoundException();
            }
            role.ProjectRoleName = projectRoleName;
        }
    }
}
