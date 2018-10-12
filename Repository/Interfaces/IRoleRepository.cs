using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAllRoles();
        Role GetRoleById(int id);
        IEnumerable<Role> FindRole(Func<Role, Boolean> predicate);
        void CreateRole(Role item);
        void UpdateRole(Role item);
        void DeleteRole(int id);
    }
}
