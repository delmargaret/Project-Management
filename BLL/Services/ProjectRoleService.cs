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
        Map<ProjectRole, ProjectRoleDTO> Map = new Map<ProjectRole, ProjectRoleDTO>();

        public ProjectRoleService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public ProjectRoleDTO CreateProjectRole(string projectRoleName)
        {
            ProjectRole projectRole = new ProjectRole
            {
                ProjectRoleName = projectRoleName
            };
            var pr = Database.ProjectRoles.CreateProjectRole(projectRole);
            Database.Save();
            return Map.ObjectMap(pr);
        }

        public void DeleteProjectRoleById(int id)
        {
            Database.ProjectRoles.DeleteProjectRole(id);
            Database.Save();
        }

        public ProjectRoleDTO GetProjectRoleById(int id)
        {
            var role = Database.ProjectRoles.GetProjectRoleById(id);
            return Map.ObjectMap(role);
        }

        public IEnumerable<ProjectRoleDTO> GetProjectRoles()
        {
            var roles = Database.ProjectRoles.GetAllProjectRoles();
            return Map.ListMap(roles);
        }

        public void ChangeProjectRoleName(int id, string roleName)
        {
            Database.ProjectRoles.ChangeProjectRoleName(id, roleName);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
