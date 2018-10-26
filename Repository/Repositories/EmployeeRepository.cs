using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;
using Exeption;

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
            if (employee == null)
            {
                throw new NotFoundException();
            }
                employee.GitLink = gitlink;
        }

        public void AddPhoneNumber(int employeeId, string phoneNumber)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.PhoneNumber = phoneNumber;
        }

        public void ChangeEmail(int employeeId, string newEmail)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.Email = newEmail;
        }

        public void ChangeGitLink(int employeeId, string newGitLink)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.GitLink = newGitLink;
        }

        public void ChangeName(int employeeId, string newName)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.EmployeeName = newName;
        }

        public void ChangePatronymic(int employeeId, string newPatronymic)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.EmployeePatronymic = newPatronymic;
        }

        public void ChangePhoneNumber(int employeeId, string newPhoneNumber)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.PhoneNumber = newPhoneNumber;
        }

        public void ChangeSurname(int employeeId, string newSurname)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.EmployeeSurname = newSurname;
        }

        public void ChangeRole(int employeeId, int roleId)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.RoleId = roleId;
            employee.Role = db.Roles.Find(roleId);
        }

        public void SetWorkLoadType(int employeeId, int WorkLoadTypeId)
        {
            Employee employee = db.Employees.Find(employeeId);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.PercentOrScheduleId = WorkLoadTypeId;
            employee.PercentOrSchedule = db.PercentOrSchedules.Find(WorkLoadTypeId);
        }
        
        public void CreateEmployee(Employee item)
        {
            db.Employees.Add(item);
        }

        public void DeleteEmployeeByEmail(string email)
        {
            List<Employee> list = new List<Employee>();
            list = db.Employees.Where(item => item.Email == email).ToList();
            if (list.Count == 0)
            {
                throw new NotFoundException();
            }
            db.Employees.Remove(list.First());
        }

        public void DeleteEmployeeById(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            db.Employees.Remove(employee);
        }

        public void DeleteEmployeeBySurname(string surname)
        {
            List<Employee> list = new List<Employee>();
            list = db.Employees.Where(item => item.EmployeeSurname == surname).ToList();
            if (list.Count == 0)
            {
                throw new NotFoundException();
            }
            db.Employees.Remove(list.First());
        }

        public void DeleteGitLinkByEmployeeId(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.GitLink = null;
        }

        public void DeletePhoneNumberByEmployeeId(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                throw new NotFoundException();
            }
            employee.PhoneNumber = null;
        }

        public IEnumerable<Employee> FindEmployee(Func<Employee, bool> predicate)
        {
            if (db.Employees.Where(predicate).ToList().Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Employees.Where(predicate).ToList();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            if (db.Employees.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Employees;
        }

        public Employee GetEmployeeByEmail(string email)
        {
            List<Employee> list = new List<Employee>();
            list = db.Employees.Where(item => item.Email == email).ToList();
            if (list.Count == 0)
            {
                throw new NotFoundException();
            }
            return list.First();
        }

        public Employee GetEmployeeById(int id)
        {
            if (db.Employees.Find(id) == null)
            {
                throw new NotFoundException();
            }
            return db.Employees.Find(id);
        }

        public IEnumerable<Employee> GetEmployeesBySurname(string surname)
        {
            if (db.Employees.Where(item => item.EmployeeSurname == surname).Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Employees.Where(item=>item.EmployeeSurname==surname);
        }

        public IEnumerable<Employee> GetEmployeesByRole(int roleId)
        {
            if (db.Employees.Where(item => item.RoleId == roleId).Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Employees.Where(item => item.RoleId == roleId);
        }

        public void UpdateEmployee(Employee item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
