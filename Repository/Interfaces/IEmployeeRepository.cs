using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> SortEmployeesBySurnameAsc();
        IEnumerable<Employee> SortEmployeesByRoleAsc();
        IEnumerable<Employee> SortEmployeesBySurnameDesc();
        IEnumerable<Employee> SortEmployeesByRoleDesc();
        IEnumerable<Employee> GetEmployeesByRole(int roleId);
        IEnumerable<Employee> GetEmployeesBySurname(string surname);
        void FindSameEmployee(string name, string surname, string patronymic, string email, string git, string phone, int roleId, int workloadId);
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByEmail(string email);
        IEnumerable<Employee> FindEmployee(Func<Employee, Boolean> predicate);
        Employee CreateEmployee(Employee item);
        void UpdateEmployee(Employee item);
        void DeleteEmployeeById(int id);
        void DeleteEmployeeBySurname(string surname);
        void DeleteEmployeeByEmail(string email);
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
        void ChangeRole(int employeeId, int RoleId);
        void SetWorkLoadType(int employeeId, int WorkLoadTypeId);
    }
}
