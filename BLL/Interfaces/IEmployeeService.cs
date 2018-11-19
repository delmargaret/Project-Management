using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDTO CreateEmployee(EmployeeDTO employeeDTO);
        void DeleteEmployeeById(int id);
        void DeleteEmployeeBySurname(string surname);
        void DeleteEmployeeByEmail(string email);
        EmployeeDTO GetEmployeeById(int id);
        EmployeeDTO GetEmployeeByEmail(string email);
        IEnumerable<EmployeeDTO> FindEmployeesNotOnProject(int projectId);
        IEnumerable<EmployeeDTO> GetEmployeesBySurname(string surname);
        IEnumerable<EmployeeDTO> GetAllEmployees();
        IEnumerable<EmployeeDTO> SortEmployeesBySurnameAsc();
        IEnumerable<EmployeeDTO> SortEmployeesByRoleAsc();
        IEnumerable<EmployeeDTO> SortEmployeesBySurnameDesc();
        IEnumerable<EmployeeDTO> SortEmployeesByRoleDesc();
        IEnumerable<EmployeeDTO> GetEmployeesByRoleId(int roleId);
        void AddGitLink(int employeeId, string gitlink);
        void DeleteGitLinkByEmployeeId(int id);
        void AddPhoneNumber(int employeeId, string phoneNumber);
        void DeletePhoneNumberByEmployeeId(int id);
        void ChangeName(int employeeId, string newName);
        void ChangeSurname(int employeeId, string newSurname);
        void ChangePatronymic(int employeeId, string newPatronymic);
        void ChangeEmail(int employeeId, string newEmail);
        void ChangeGitLink(int employeeId, string newGitLink);
        void ChangePhoneNumber(int employeeId, string newPhoneNumber);
        void ChangeRole(int employeeId, int roleId);
        void ChangeWorkLoad(int employeeId, int workLoadId);
        void Dispose();
    }
}
