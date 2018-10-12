using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private ManagementContext db;

        public EmployeeRepository(ManagementContext context)
        {
            this.db = context;
        }

        public void AddGitLink(int employeeId, string gitlink)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee != null)
                employee.GitLink = gitlink;
        }

        public void AddPhoneNumber(int employeeId, string phoneNumber)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee != null)
                employee.PhoneNumber = phoneNumber;
        }

        public void ChangeEmail(int employeeId, string newEmail)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee != null)
                employee.Email = newEmail;
        }

        public void ChangeGitLink(int employeeId, string newGitLink)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee != null)
                employee.GitLink = newGitLink;
        }

        public void ChangeName(int employeeId, string newName)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee != null)
                employee.EmployeeName = newName;
        }

        public void ChangePatronymic(int employeeId, string newPatronymic)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee != null)
                employee.EmployeePatronymic = newPatronymic;
        }

        public void ChangePhoneNumber(int employeeId, string newPhoneNumber)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee != null)
                employee.PhoneNumber = newPhoneNumber;
        }

        public void ChangeSurname(int employeeId, string newSurname)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee != null)
                employee.EmployeeSurname = newSurname;
        }

        public void ChangeRole(int employeeId, int roleId)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee != null)
            {
                employee.RoleId = roleId;
                employee.Role = db.Roles.Find(roleId);
            }
        }

        public void CreateEmployee(Employee item)
        {
            db.Employees.Add(item);
        }

        public void DeleteEmployeeByEmail(string email)
        {
            Employee employee = db.Employees.First(item => item.Email == email);
            if (employee != null)
                db.Employees.Remove(employee);
        }

        public void DeleteEmployeeById(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
                db.Employees.Remove(employee);
        }

        public void DeleteEmployeeBySurname(string surname)
        {
            Employee employee = db.Employees.First(item => item.EmployeeSurname == surname);
            if (employee != null)
                db.Employees.Remove(employee);
        }

        public void DeleteGitLinkByEmployeeId(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
                employee.GitLink = null;
        }

        public void DeletePhoneNumberByEmployeeId(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
                employee.PhoneNumber = null;
        }

        public IEnumerable<Employee> FindEmployee(Func<Employee, bool> predicate)
        {
            return db.Employees.Where(predicate).ToList();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return db.Employees;
        }

        public Employee GetEmployeeByEmail(string email)
        {
            return db.Employees.Where(item => item.Email == email).First();
        }

        public Employee GetEmployeeById(int id)
        {
            return db.Employees.Find(id);
        }

        public IEnumerable<Employee> GetEmployeesBySurname(string surname)
        {
            return db.Employees.Where(item=>item.EmployeeSurname==surname);
        }

        public IEnumerable<Employee> GetEmployeesByRole(int roleId)
        {
            return db.Employees.Where(item => item.RoleId == roleId);
        }

        public void UpdateEmployee(Employee item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
