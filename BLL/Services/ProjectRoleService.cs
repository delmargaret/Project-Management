using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.DTO;
using AutoMapper;
using Repository.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class ProjectRoleService : IProjectRoleService
    {
        IUnitOfWork Database { get; set; }

        public ProjectRoleService(IUnitOfWork uow)
        {
            Database = uow;
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
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectRole, ProjectRoleDTO>()).CreateMapper();
            return mapper.Map<ProjectRole, ProjectRoleDTO>(Database.ProjectRoles.GetProjectRoleById(id.Value));
        }

        public IEnumerable<ProjectRoleDTO> GetProjectRoles()
        {
            var roles = Database.ProjectRoles.GetAllProjectRoles();
            if (roles.Count() == 0)
            {
                Console.WriteLine("роли не найдены");
                return null;
            }
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectRole, ProjectRoleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ProjectRole>, List<ProjectRoleDTO>>(Database.ProjectRoles.GetAllProjectRoles());
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
