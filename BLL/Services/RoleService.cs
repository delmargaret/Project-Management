using DAL.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTO;
using BLL.Mapping;
using BLL.Infrastructure;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        IUnitOfWork Database { get; set; }
        Map<Role, RoleDTO> Map { get; set; }

        public RoleService(IUnitOfWork uow, Map<Role, RoleDTO> map)
        {
            Database = uow;
            Map = map;
        }

        public void CreateRole(string roleName)
        {
            Role role = new Role
            {
                RoleName = roleName
            };
            Database.Roles.CreateRole(role);
            Database.Save();
        }

        public void DeleteRoleById(int id)
        {
            var role = Database.Roles.GetRoleById(id);
            Database.Roles.DeleteRole(role.Id);
            Database.Save();
        }

        public RoleDTO GetRoleById(int id)
        {
            var role = Database.Roles.GetRoleById(id);
            return Map.ObjectMap(role);
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            var roles = Database.Roles.GetAllRoles();
            return Map.ListMap(roles);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
