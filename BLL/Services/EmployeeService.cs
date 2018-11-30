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
        Map<Employee, EmployeeDTO> Map = new Map<Employee, EmployeeDTO>();

        public EmployeeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public EmployeeDTO CreateEmployee(EmployeeDTO employeeDTO)
        {
            Role role = Database.Roles.GetRoleById(employeeDTO.RoleId);
            Database.Employees.FindSameEmployee(employeeDTO.EmployeeName, employeeDTO.EmployeeSurname,
                employeeDTO.EmployeePatronymic, employeeDTO.Email, employeeDTO.GitLink, employeeDTO.PhoneNumber,
                employeeDTO.RoleId, employeeDTO.PercentOrScheduleId);
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
                PercentOrScheduleId = 3,
                PercentOrSchedule = Database.WorkLoads.GetTypeById(3)
            };

            var emp = Database.Employees.CreateEmployee(employee);
            Database.Save();
            return Map.ObjectMap(emp);
        }

        public IEnumerable<EmployeeDTO> FindEmployeesNotOnProject(int projectId)
        {
            var employees = Database.Employees.FindEmployeesNotOnProject(projectId);
            return Map.ListMap(employees);
        }

        public void ChangeWorkLoad(int employeeId, int workLoadId)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            if (employee.PercentOrScheduleId == 3 && workLoadId != 3)
            {
                Database.Employees.SetWorkLoadType(employeeId, workLoadId);
            }
            else if (employee.PercentOrScheduleId == 1 && workLoadId != 1)
            {
                var projects = Database.ProjectWorks.GetEmployeesProjects(employeeId);
                foreach (var project in projects)
                {
                    if (project.WorkLoad != null)
                    {
                        throw new PercentOrScheduleException();
                    }
                }
                Database.Employees.SetWorkLoadType(employeeId, workLoadId);
            }
            else if (employee.PercentOrScheduleId == 2 && workLoadId != 2)
            {
                var projects = Database.ProjectWorks.GetEmployeesProjects(employeeId);
                foreach (var project in projects)
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

        public IEnumerable<EmployeeDTO> SortEmployeesBySurnameAsc()
        {
            var employees = Database.Employees.SortEmployeesBySurnameAsc();
            return Map.ListMap(employees);
        }

        public IEnumerable<EmployeeDTO> SortEmployeesBySurnameDesc()
        {
            var employees = Database.Employees.SortEmployeesBySurnameDesc();
            return Map.ListMap(employees);
        }

        public IEnumerable<EmployeeDTO> SortEmployeesByRoleAsc()
        {
            var employees = Database.Employees.SortEmployeesByRoleAsc();
            return Map.ListMap(employees);
        }

        public IEnumerable<EmployeeDTO> SortEmployeesByRoleDesc()
        {
            var employees = Database.Employees.SortEmployeesByRoleDesc();
            return Map.ListMap(employees);
        }

        public IEnumerable<EmployeeDTO> GetEmployeesByRoleId(int roleId)
        {
            var employees = Database.Employees.GetEmployeesByRole(roleId);
            return Map.ListMap(employees);
        }

        public void AddGitLink(int employeeId, string gitlink)
        {
            Database.Employees.AddGitLink(employeeId, gitlink);
            Database.Save();
        }

        public void DeleteGitLinkByEmployeeId(int id)
        {
            Database.Employees.DeleteGitLinkByEmployeeId(id);
            Database.Save();
        }

        public void AddPhoneNumber(int employeeId, string phoneNumber)
        {
            Database.Employees.AddPhoneNumber(employeeId, phoneNumber);
            Database.Save();
        }

        public void DeletePhoneNumberByEmployeeId(int id)
        {
            Database.Employees.DeletePhoneNumberByEmployeeId(id);
            Database.Save();
        }

        public void ChangeName(int employeeId, string newName)
        {
            Database.Employees.ChangeName(employeeId, newName);
            Database.Save();
        }

        public void ChangeSurname(int employeeId, string newSurname)
        {
            Database.Employees.ChangeSurname(employeeId, newSurname);
            Database.Save();
        }

        public void ChangePatronymic(int employeeId, string newPatronymic)
        {
            Database.Employees.ChangePatronymic(employeeId, newPatronymic);
            Database.Save();
        }

        public void ChangeEmail(int employeeId, string newEmail)
        {
            Database.Employees.ChangeEmail(employeeId, newEmail);
            Database.Save();
        }

        public void ChangeGitLink(int employeeId, string newGitLink)
        {
            Database.Employees.ChangeGitLink(employeeId, newGitLink);
            Database.Save();
        }

        public void ChangePhoneNumber(int employeeId, string newPhoneNumber)
        {
            Database.Employees.ChangePhoneNumber(employeeId, newPhoneNumber);
            Database.Save();
        }

        public void ChangeRole(int employeeId, int roleId)
        {
            Database.Employees.ChangeRole(employeeId, roleId);
            Database.Save();
        }
    }
}
