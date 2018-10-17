using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interfaces;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using BLL.Mapping;
using BLL.Infrastructure;

namespace BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        IUnitOfWork Database { get; set; }
        Map<Employee, EmployeeDTO> Map { get; set; }

        public EmployeeService(IUnitOfWork uow, Map<Employee, EmployeeDTO> map)
        {
            Database = uow;
            Map = map;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void CreateEmployee(EmployeeDTO employeeDTO)
        {
            Role role = Database.Roles.GetRoleById(employeeDTO.RoleId);
            if (role == null)
            {
                throw new ProjectException("Роль не найдена");
            }
            Employee employee = new Employee
            {
                EmployeeName = employeeDTO.EmployeeName,
                EmployeeSurname = employeeDTO.EmployeeSurname,
                EmployeePatronymic = employeeDTO.EmployeePatronymic,
                Email = employeeDTO.Email,
                GitLink = employeeDTO.GitLink,
                PhoneNumber = employeeDTO.PhoneNumber,
                RoleId = employeeDTO.RoleId,
                Role = role,
                PercentOrScheduleId=3,
                PercentOrSchedule=Database.WorkLoads.GetTypeById(3)
            };

            Database.Employees.CreateEmployee(employee);
            Database.Save();
        }

        public void DeleteEmployeeById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не указан идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(id.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.DeleteEmployeeById(id.Value);
            Database.Save();
        }

        public void DeleteEmployeeBySurname(string surname)
        {
            if (surname.Length == 0)
            {
                throw new ProjectException("Не указана фамилия сотрудника");
            }
            var employees = Database.Employees.GetEmployeesBySurname(surname);
            var employee = employees.First();
            if (employee == null)
            {
                throw new ProjectException("Сотруник не найден");
            }
            Database.Employees.DeleteEmployeeBySurname(surname);
            Database.Save();
        }

        public void DeleteEmployeeByEmail(string email)
        {
            if (email.Length == 0)
            {
                throw new ProjectException("Не указан e-mail сотрудника");
            }
            var employee = Database.Employees.GetEmployeeByEmail(email);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.DeleteEmployeeByEmail(email);
            Database.Save();
        }

        public EmployeeDTO GetEmployeeById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не указан идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(id.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            return Map.ObjectMap(employee);
        }

        public IEnumerable<EmployeeDTO> GetEmployeesBySurname(string surname)
        {
            if (surname.Length == 0)
            {
                throw new ProjectException("Не указана фамилия сотрудника");
            }
            var employees = Database.Employees.GetEmployeesBySurname(surname);
            if (employees.Count()==0)
            {
                throw new ProjectException("Сотрудники не найдены");
            }
            return Map.ListMap(employees);
        }

        public EmployeeDTO GetEmployeeByEmail(string email)
        {
            if (email.Length == 0)
            {
                throw new ProjectException("Не указан e-mail сотрудника");
            }
            var employee = Database.Employees.GetEmployeeByEmail(email);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            return Map.ObjectMap(employee);
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            var employees = Database.Employees.GetAllEmployees();
            if (employees.Count() == 0)
            {
                throw new ProjectException("Сотрудники не найдены");
            }
            return Map.ListMap(employees);
        }

        public IEnumerable<EmployeeDTO> GetEmployeesByRoleId(int? roleId)
        {
            if (roleId == null)
            {
                throw new ProjectException("Не указан идентификатор роли");
            }
            var role = Database.Roles.GetRoleById(roleId.Value);
            if (role == null)
            {
                throw new ProjectException("Роль не найдена");
            }
            var employees = Database.Employees.GetEmployeesByRole(roleId.Value);
            if (employees.Count() == 0)
            {
                throw new ProjectException("Сотрудники не найдены");
            }
            return Map.ListMap(employees);
        }

        public void AddGitLink(int? employeeId, string gitlink)
        {
            if (gitlink.Length == 0)
            {
                throw new ProjectException("Не указан gitLink сотрудника");
            }
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.AddGitLink(employeeId.Value, gitlink);
            Database.Save();
        }

        public void DeleteGitLinkByEmployeeId(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(id.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.DeleteGitLinkByEmployeeId(id.Value);
            Database.Save();
        }

        public void AddPhoneNumber(int? employeeId, string phoneNumber)
        {
            if (phoneNumber.Length == 0)
            {
                throw new ProjectException("Не указан номер телефона сотрудника");
            }
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.AddPhoneNumber(employeeId.Value, phoneNumber);
            Database.Save();
        }

        public void DeletePhoneNumberByEmployeeId(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(id.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.DeletePhoneNumberByEmployeeId(id.Value);
            Database.Save();
        }

        public void ChangeName(int? employeeId, string newName)
        {
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.ChangeName(employeeId.Value, newName);
            Database.Save();
        }

        public void ChangeSurname(int? employeeId, string newSurname)
        {
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.ChangeSurname(employeeId.Value, newSurname);
            Database.Save();
        }

        public void ChangePatronymic(int? employeeId, string newPatronymic)
        {
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.ChangePatronymic(employeeId.Value, newPatronymic);
            Database.Save();
        }

        public void ChangeEmail(int? employeeId, string newEmail)
        {
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.ChangeEmail(employeeId.Value, newEmail);
            Database.Save();
        }

        public void ChangeGitLink(int? employeeId, string newGitLink)
        {
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.ChangeGitLink(employeeId.Value, newGitLink);
            Database.Save();
        }

        public void ChangePhoneNumber(int? employeeId, string newPhoneNumber)
        {
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.Employees.ChangePhoneNumber(employeeId.Value, newPhoneNumber);
            Database.Save();
        }

        public void ChangeRole(int? employeeId, int? roleId)
        {
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            if (roleId == null)
            {
                throw new ProjectException("Не установлен идентификатор роли");
            }
            var role = Database.Roles.GetRoleById(roleId.Value);
            if (role == null)
            {
                throw new ProjectException("Роль не найдена");
            }
            Database.Employees.ChangeRole(employeeId.Value, roleId.Value);
            Database.Save();
        }

    }
}
