using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IService
    {
        void CreateRole(string roleName);
        void DeleteRole(int? id);
        void CreateEmployee(EmployeeDTO employeeDTO);
        void DeleteEmployee(int? id);
        EmployeeDTO GetEmployee(int? id);
        IEnumerable<EmployeeDTO> GetEmployees();
        RoleDTO GetRole(int? id);
        IEnumerable<RoleDTO> GetRoles();
        void Dispose();
    }
}
