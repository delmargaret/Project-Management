﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interfaces;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using AutoMapper;

namespace BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        IUnitOfWork Database { get; set; }

        public EmployeeService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void CreateEmployee(EmployeeDTO employeeDTO)
        {
            Role role = Database.Roles.GetRoleById(employeeDTO.RoleId);

            if (role == null)
                Console.WriteLine("роль не существует");
            Employee employee = new Employee
            {
                EmployeeName = employeeDTO.EmployeeName,
                EmployeeSurname = employeeDTO.EmployeeSurname,
                EmployeePatronymic = employeeDTO.EmployeePatronymic,
                Email = employeeDTO.Email,
                GitLink = employeeDTO.GitLink,
                PhoneNumber = employeeDTO.PhoneNumber,
                RoleId = employeeDTO.RoleId,
                Role = role
            };

            Database.Employees.CreateEmployee(employee);
            Database.Save();
        }

        public void DeleteEmployeeById(int? id)
        {
            if (id == null)
                Console.WriteLine("не установлено id сотрудника"); 
            var employee = Database.Employees.GetEmployeeById(id.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.DeleteEmployeeById(id.Value);
            Database.Save();
        }

        public void DeleteEmployeeBySurname(string surname)
        {
            var employees = Database.Employees.GetEmployeesBySurname(surname);
            var employee = employees.First();
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.DeleteEmployeeBySurname(surname);
            Database.Save();
        }

        public void DeleteEmployeeByEmail(string email)
        {
            var employee = Database.Employees.GetEmployeeByEmail(email);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.DeleteEmployeeByEmail(email);
            Database.Save();
        }

        public EmployeeDTO GetEmployeeById(int? id)
        {
            var employee = Database.Employees.GetEmployeeById(id.Value);
            if (employee == null)
                Console.WriteLine("сотрудник не найден");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();
            return mapper.Map<Employee, EmployeeDTO>(Database.Employees.GetEmployeeById(id.Value));
        }

        public IEnumerable<EmployeeDTO> GetEmployeesBySurname(string surname)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(Database.Employees.GetEmployeesBySurname(surname));
        }

        public EmployeeDTO GetEmployeeByEmail(string email)
        {
            var employee = Database.Employees.GetEmployeeByEmail(email);
            if (employee == null)
                Console.WriteLine("сотрудник не найден");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();
            return mapper.Map<Employee, EmployeeDTO>(Database.Employees.GetEmployeeByEmail(email));
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(Database.Employees.GetAllEmployees());
        }

        public IEnumerable<EmployeeDTO> GetEmployeesByRoleId(int roleId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(Database.Employees.GetEmployeesByRole(roleId));
        }

        public void AddGitLink(int? employeeId, string gitlink)
        {
            if (employeeId == null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.AddGitLink(employeeId.Value, gitlink);
            Database.Save();
        }

        public void DeleteGitLinkByEmployeeId(int? id)
        {
            if (id == null)
                Console.WriteLine("не установлено id сотрудника"); 
            var employee = Database.Employees.GetEmployeeById(id.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.DeleteGitLinkByEmployeeId(id.Value);
            Database.Save();
        }

        public void AddPhoneNumber(int? employeeId, string phoneNumber)
        {
            if (employeeId == null)
                Console.WriteLine("не установлено id сотрудника"); 
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.AddPhoneNumber(employeeId.Value, phoneNumber);
            Database.Save();
        }

        public void DeletePhoneNumberByEmployeeId(int? id)
        {
            if (id == null)
                Console.WriteLine("не установлено id сотрудника"); 
            var employee = Database.Employees.GetEmployeeById(id.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.DeletePhoneNumberByEmployeeId(id.Value);
            Database.Save();
        }

        public void ChangeName(int? employeeId, string newName)
        {
            if (employeeId == null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.ChangeName(employeeId.Value, newName);
            Database.Save();
        }

        public void ChangeSurname(int? employeeId, string newSurname)
        {
            if (employeeId == null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.ChangeSurname(employeeId.Value, newSurname);
            Database.Save();
        }

        public void ChangePatronymic(int? employeeId, string newPatronymic)
        {
            if (employeeId == null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.ChangePatronymic(employeeId.Value, newPatronymic);
            Database.Save();
        }

        public void ChangeEmail(int? employeeId, string newEmail)
        {
            if (employeeId == null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.ChangeEmail(employeeId.Value, newEmail);
            Database.Save();
        }

        public void ChangeGitLink(int? employeeId, string newGitLink)
        {
            if (employeeId == null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.ChangeGitLink(employeeId.Value, newGitLink);
            Database.Save();
        }

        public void ChangePhoneNumber(int? employeeId, string newPhoneNumber)
        {
            if (employeeId == null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует"); 
            Database.Employees.ChangePhoneNumber(employeeId.Value, newPhoneNumber);
            Database.Save();
        }

        public void ChangeRole(int? employeeId, int? roleId)
        {
            if (employeeId == null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует");
            if (roleId == null)
                Console.WriteLine("не установлено id роли");
            var role = Database.Roles.GetRoleById(roleId.Value);
            if (role == null)
                Console.WriteLine("роли не существует");
            Database.Employees.ChangeRole(employeeId.Value, roleId.Value);
            Database.Save();
        }

    }
}