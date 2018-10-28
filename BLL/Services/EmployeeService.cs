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
using Exeption;

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

        public EmployeeDTO CreateEmployee(EmployeeDTO employeeDTO)
        {
            Role role = Database.Roles.GetRoleById(employeeDTO.RoleId);
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

            var emp = Database.Employees.CreateEmployee(employee);
            Database.Save();
            return Map.ObjectMap(emp);
        }

        public void ChangeWorkLoad(int employeeId, int workLoadId)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            if (employee.PercentOrScheduleId == 3 && workLoadId != 3)
            {
                Database.Employees.SetWorkLoadType(employeeId, workLoadId);
            }
            else if(employee.PercentOrScheduleId == 1 && workLoadId != 1)
            {
                var projects = Database.ProjectWorks.GetEmployeesProjects(employeeId);
                foreach(var project in projects)
                {
                    if(project.WorkLoad != null)
                    {
                        throw new PercentOrScheduleException();
                    }
                }
                Database.Employees.SetWorkLoadType(employeeId, workLoadId);
            }
            else if (employee.PercentOrScheduleId == 2 && workLoadId != 2)
            {
                var projects = Database.ProjectWorks.GetEmployeesProjects(employeeId);
                foreach(var project in projects)
                {
                    var schedules = Database.Schedules.GetScheduleOnProjectWork(project.Id).ToList();
                    if (schedules.Count != 0)
                    {
                        throw new PercentOrScheduleException();
                    }
                }
                Database.Employees.SetWorkLoadType(employeeId, workLoadId);
            }
            else
            {
                throw new PercentOrScheduleException();
            }
        }

        public void DeleteEmployeeById(int id)
        {
            Database.Employees.DeleteEmployeeById(id);
            Database.Save();
        }

        public void DeleteEmployeeBySurname(string surname)
        {
            Database.Employees.DeleteEmployeeBySurname(surname);
            Database.Save();
        }

        public void DeleteEmployeeByEmail(string email)
        {
            Database.Employees.DeleteEmployeeByEmail(email);
            Database.Save();
        }

        public EmployeeDTO GetEmployeeById(int id)
        {
            var employee = Database.Employees.GetEmployeeById(id);
            return Map.ObjectMap(employee);
        }

        public IEnumerable<EmployeeDTO> GetEmployeesBySurname(string surname)
        {
            var employees = Database.Employees.GetEmployeesBySurname(surname);
            return Map.ListMap(employees);
        }

        public EmployeeDTO GetEmployeeByEmail(string email)
        {
            var employee = Database.Employees.GetEmployeeByEmail(email);
            return Map.ObjectMap(employee);
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            var employees = Database.Employees.GetAllEmployees();
            return Map.ListMap(employees);
        }

        public IEnumerable<EmployeeDTO> GetEmployeesByRoleId(int roleId)
        {
            var role = Database.Roles.GetRoleById(roleId);
            var employees = Database.Employees.GetEmployeesByRole(role.Id);
            return Map.ListMap(employees);
        }

        public void AddGitLink(int employeeId, string gitlink)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            Database.Employees.AddGitLink(employee.Id, gitlink);
            Database.Save();
        }

        public void DeleteGitLinkByEmployeeId(int id)
        {
            var employee = Database.Employees.GetEmployeeById(id);
            Database.Employees.DeleteGitLinkByEmployeeId(employee.Id);
            Database.Save();
        }

        public void AddPhoneNumber(int employeeId, string phoneNumber)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            Database.Employees.AddPhoneNumber(employee.Id, phoneNumber);
            Database.Save();
        }

        public void DeletePhoneNumberByEmployeeId(int id)
        {
            var employee = Database.Employees.GetEmployeeById(id);
            Database.Employees.DeletePhoneNumberByEmployeeId(employee.Id);
            Database.Save();
        }

        public void ChangeName(int employeeId, string newName)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            Database.Employees.ChangeName(employee.Id, newName);
            Database.Save();
        }

        public void ChangeSurname(int employeeId, string newSurname)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            Database.Employees.ChangeSurname(employee.Id, newSurname);
            Database.Save();
        }

        public void ChangePatronymic(int employeeId, string newPatronymic)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            Database.Employees.ChangePatronymic(employee.Id, newPatronymic);
            Database.Save();
        }

        public void ChangeEmail(int employeeId, string newEmail)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            Database.Employees.ChangeEmail(employee.Id, newEmail);
            Database.Save();
        }

        public void ChangeGitLink(int employeeId, string newGitLink)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            Database.Employees.ChangeGitLink(employee.Id, newGitLink);
            Database.Save();
        }

        public void ChangePhoneNumber(int employeeId, string newPhoneNumber)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            Database.Employees.ChangePhoneNumber(employee.Id, newPhoneNumber);
            Database.Save();
        }

        public void ChangeRole(int employeeId, int roleId)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            var role = Database.Roles.GetRoleById(roleId);
            Database.Employees.ChangeRole(employee.Id, role.Id);
            Database.Save();
        }

    }
}
