using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Repositories;
using Repository.Interfaces;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using AutoMapper;

namespace BLL.Services
{
    public class Service : IService
    {
        IUnitOfWork Database { get; set; }

        public Service(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void CreateRole(string roleName)
        {
            Role role = new Role
            {
                RoleName = roleName
            };
            Database.Roles.Create(role);
            Database.Save();
        }

        public void DeleteRole(int? id)
        {
            if (id == null)
                Console.WriteLine("Не установлено id роли");
            var role = Database.Roles.Get(id.Value);
            if (role == null)
                Console.WriteLine("роль не найдена");
            Database.Roles.Delete(id.Value);
            Database.Save();
        }

        public void CreateEmployee(EmployeeDTO employeeDTO)
        {
            Role role = Database.Roles.Get(employeeDTO.RoleId);

            if (role == null)
                Console.WriteLine("роль не существует");
            Employee employee = new Employee
            {
                EmployeeName=employeeDTO.EmployeeName,
                EmployeeSurname=employeeDTO.EmployeeSurname,
                EmployeePatronymic=employeeDTO.EmployeePatronymic,
                Email=employeeDTO.Email,
                GitLink=employeeDTO.GitLink,
                PhoneNumber=employeeDTO.PhoneNumber,
                Role = role
            };

            Database.Employees.Create(employee);
            Database.Save();
        }

        public void DeleteEmployee(int? id)
        {
            if (id == null)
                return;
            var employee = Database.Employees.Get(id.Value);
            if (employee == null)
                return;
            Database.Employees.Delete(id.Value);
            Database.Save();
        }

        public EmployeeDTO GetEmployee(int? id)
        {
            if (id == null)
                Console.WriteLine("Не установлено id сотрудника");
            var employee = Database.Employees.Get(id.Value);
            if (employee == null)
                Console.WriteLine("сотрудник не найден");
            return new EmployeeDTO { Id = employee.Id, EmployeeName = employee.EmployeeName, EmployeeSurname = employee.EmployeeSurname, EmployeePatronymic = employee.EmployeePatronymic,
            Email=employee.Email, GitLink=employee.GitLink, PhoneNumber=employee.PhoneNumber, RoleId=employee.Role.Id};
        }

        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(Database.Employees.GetAll());
        }

        public RoleDTO GetRole(int? id)
        {
            //if (id == null)
            //{ return; }
            Role role = Database.Roles.Get(id.Value);
            //if (role == null)
            //{ Console.WriteLine("роль не найдена"); }
            return new RoleDTO
            {
                Id = role.Id,
                RoleName = role.RoleName,
            };
        }

        public IEnumerable<RoleDTO> GetRoles()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Role>, List<RoleDTO>>(Database.Roles.GetAll());
        }
    }
}
