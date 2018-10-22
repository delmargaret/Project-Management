using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectRoleService
    {
        void CreateProjectRole(string roleName);
        void DeleteProjectRoleById(int id);
        ProjectRoleDTO GetProjectRoleById(int id);
        IEnumerable<ProjectRoleDTO> GetProjectRoles();
        void Dispose();
    }
}
