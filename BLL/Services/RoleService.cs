using DAL.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTO;
using AutoMapper;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        IUnitOfWork Database { get; set; }

        public RoleService(IUnitOfWork uow)
        {
            Database = uow;
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
                Console.WriteLine("Не установлено id роли");
                return;
            }
            var role = Database.Roles.GetRoleById(id.Value);
            if (role == null)
            {
                Console.WriteLine("роль не найдена");
                return;
            }
            Database.Roles.DeleteRole(id.Value);
            Database.Save();
        }

        public RoleDTO GetRoleById(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("Не установлено id роли");
                return null;
            }
            var role = Database.Roles.GetRoleById(id.Value);
            if (role == null)
            {
                Console.WriteLine("роль не найдена");
                return null;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<Role, RoleDTO>(Database.Roles.GetRoleById(id.Value));
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            var roles = Database.Roles.GetAllRoles();
            if (roles.Count() == 0)
            {
                Console.WriteLine("роли не найдены");
                return null;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Role>, List<RoleDTO>>(Database.Roles.GetAllRoles());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
