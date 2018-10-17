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
using BLL.Infrastructure;

namespace BLL.Services
{
    public class ProjectRoleService : IProjectRoleService
    {
        IUnitOfWork Database { get; set; }
        Map<ProjectRole, ProjectRoleDTO> Map { get; set; }

        public ProjectRoleService(IUnitOfWork uow, Map<ProjectRole, ProjectRoleDTO> map)
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
                throw new ProjectException("Не установлен идентификатор роли на проекте");
            }
            var role = Database.ProjectRoles.GetProjectRoleById(id.Value);
            if (role == null)
            {
                throw new ProjectException("Роль не найдена");
            }
            Database.ProjectRoles.DeleteProjectRole(id.Value);
            Database.Save();
        }

        public ProjectRoleDTO GetProjectRoleById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор роли на проекте");
            }
            var role = Database.ProjectRoles.GetProjectRoleById(id.Value);
            if (role == null)
            {
                throw new ProjectException("Роль не найдена");
            }
            return Map.ObjectMap(role);
        }

        public IEnumerable<ProjectRoleDTO> GetProjectRoles()
        {
            var roles = Database.ProjectRoles.GetAllProjectRoles();
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
