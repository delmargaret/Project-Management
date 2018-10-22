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

        public void DeleteProjectRoleById(int id)
        {
            var role = Database.ProjectRoles.GetProjectRoleById(id);
            Database.ProjectRoles.DeleteProjectRole(role.Id);
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

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
