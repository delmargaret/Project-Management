using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IRoleService
    {
        void CreateRole(string roleName);
        void DeleteRoleById(int id);
        RoleDTO GetRoleById(int id);
        IEnumerable<RoleDTO> GetRoles();
        void Dispose();
    }
}
