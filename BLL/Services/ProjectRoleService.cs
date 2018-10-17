using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTO;
using Repository.Interfaces;
using DAL.Entities;
using BLL.Mapping;

namespace BLL.Services
{
    public class ProjectRoleService : IProjectRoleService
    {
        IUnitOfWork Database { get; set; }
        Maps<ProjectRole, ProjectRoleDTO> Map { get; set; }

        public ProjectRoleService(IUnitOfWork uow, Maps<ProjectRole, ProjectRoleDTO> map)
        {
            Database = uow;
            Map = map;
        }

        public void CreateProjectRole(string projectRoleName)
        {
            ProjectRole projectRole = new ProjectRole
            {
                ProjectRoleName = projectRoleName
            };
            Database.ProjectRoles.CreateProjectRole(projectRole);
            Database.Save();
        }

        public void DeleteProjectRoleById(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("Не установлено id роли в проекте");
                return;
            }
            var role = Database.ProjectRoles.GetProjectRoleById(id.Value);
            if (role == null)
            {
                Console.WriteLine("роль не найдена");
                return;
            }
            Database.ProjectRoles.DeleteProjectRole(id.Value);
            Database.Save();
        }

        public ProjectRoleDTO GetProjectRoleById(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("Не установлено id роли в проекте");
                return null;
            }
            var role = Database.ProjectRoles.GetProjectRoleById(id.Value);
            if (role == null)
            {
                Console.WriteLine("роль не найдена");
                return null;
            }
            return Map.Map(role);
        }

        public IEnumerable<ProjectRoleDTO> GetProjectRoles()
        {
            var roles = Database.ProjectRoles.GetAllProjectRoles();
            if (roles.Count() == 0)
            {
                Console.WriteLine("роли не найдены");
                return null;
            }
            return Map.ListMap(roles);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
