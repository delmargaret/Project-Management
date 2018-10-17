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

        public void DeleteRoleById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор роли");
            }
            var role = Database.Roles.GetRoleById(id.Value);
            if (role == null)
            {
                throw new ProjectException("Роль не найдена");
            }
            Database.Roles.DeleteRole(id.Value);
            Database.Save();
        }

        public RoleDTO GetRoleById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор роли");
            }
            var role = Database.Roles.GetRoleById(id.Value);
            if (role == null)
            {
                throw new ProjectException("Роль не найдена");
            }
            return Map.ObjectMap(role);
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            var roles = Database.Roles.GetAllRoles();
            if (roles.Count() == 0)
            {
                throw new ProjectException("Роли не найдены");
            }
            return Map.ListMap(roles);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
