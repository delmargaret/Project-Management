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
    public class RoleRepository : IRoleRepository
    {
        private ManagementContext db;

        public RoleRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            if (db.Roles.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Roles;
        }

        public Role GetRoleById(int id)
        {
            if (db.Roles.Find(id) == null)
            {
                throw new NotFoundException();
            }
            return db.Roles.Find(id);
        }

        public void CreateRole(Role role)
        {
            db.Roles.Add(role);
        }

        public void UpdateRole(Role role)
        {
            db.Entry(role).State = EntityState.Modified;
        }

        public IEnumerable<Role> FindRole(Func<Role, Boolean> predicate)
        {
            if (db.Roles.Where(predicate).ToList().Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Roles.Where(predicate).ToList();
        }

        public void DeleteRole(int id)
        {
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                throw new NotFoundException();
            }
            db.Roles.Remove(role);
        }
    }
}
