using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataContext;
using DAL.Entities;
using BLL.Services;
using BLL.DTO;
using Repository.Interfaces;
using Repository.Repositories;
using BLL.Interfaces;
using BLL.Infrastructure;
using Ninject.Modules;
using Ninject;

namespace ConsoleApp
{

    class Program
    {
        static void Main(string[] args)
        {
            IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
            EmployeeService employeeService = new EmployeeService(uow);
            RoleService roleService = new RoleService(uow);
            ProjectRoleService projectRoleService = new ProjectRoleService(uow);
            PartisipationHistoryService historyService = new PartisipationHistoryService(uow);

            #region Employee
            //Console.WriteLine("введите фамилию:");
            //var surname = Console.ReadLine();
            //Console.WriteLine("введите имя:");
            //var name = Console.ReadLine();
            //Console.WriteLine("введите отчество:");
            //var patronymic = Console.ReadLine();
            //Console.WriteLine("введите id роли");
            //int roleId = Convert.ToInt32(Console.ReadLine());
            //EmployeeDTO employee1 = new EmployeeDTO
            //{
            //    EmployeeName = name,
            //    EmployeeSurname = surname,
            //    EmployeePatronymic = patronymic,
            //    RoleId = roleId
            //};
            //employeeService.CreateEmployee(employee1);
            //Console.WriteLine();
            //var employees = employeeService.GetAllEmployees();
            //foreach (var employee in employees)
            //{
            //    Console.WriteLine("{0} {1}  Роль: {2}", employee.EmployeeName, employee.EmployeeSurname, employee.RoleId);
            //}
            //Console.WriteLine();
            //Console.WriteLine("введите id роли для поиска сотрудников");
            //int role = Convert.ToInt32(Console.ReadLine());
            //var roleEmployees = employeeService.GetEmployeesByRoleId(role);
            //foreach (var employee in roleEmployees)
            //{
            //    Console.WriteLine("{0} {1}  Роль: {2}", employee.EmployeeName, employee.EmployeeSurname, employee.RoleId);
            //}
            //Console.WriteLine();

            //Console.WriteLine("введите id сотрудника для добавления email");
            //int idEmployeeForEmail = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("введите email");
            //var email = Console.ReadLine();
            //employeeService.ChangeEmail(idEmployeeForEmail, email);

            //Console.WriteLine();

            //Console.WriteLine("введите email для нахождения сотрудника");
            //var employeeEmail = Console.ReadLine();
            //var employeeByEmail = employeeService.GetEmployeeByEmail(employeeEmail);
            //Console.WriteLine("{0} Роль: {1}", employeeByEmail.EmployeeSurname, employeeByEmail.RoleId);

            //Console.WriteLine();
            //Console.WriteLine("введите id сотрудника для добавления gitlink");
            //int idEmployeeForGit = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("введите gitlink для сотрудника");
            //var gitlink = Console.ReadLine();
            //employeeService.AddGitLink(idEmployeeForGit, gitlink);
            //Console.WriteLine("{0} Git: {1}", employeeService.GetEmployeeById(idEmployeeForGit).EmployeeSurname, employeeService.GetEmployeeById(idEmployeeForGit).GitLink);

            //Console.WriteLine();
            //employeeService.DeleteGitLinkByEmployeeId(idEmployeeForGit);
            //Console.WriteLine("{0} Git: {1}", employeeService.GetEmployeeById(idEmployeeForGit).EmployeeSurname, employeeService.GetEmployeeById(idEmployeeForGit).GitLink);

            //Console.WriteLine();
            //Console.WriteLine("введите фамилию для удаления");
            //var employeesurn = Console.ReadLine();
            //foreach (var employee in employeeService.GetAllEmployees())
            //{
            //    Console.WriteLine("{0} {1}  Роль: {2}", employee.EmployeeName, employee.EmployeeSurname, employee.RoleId);
            //}
            //employeeService.DeleteEmployeeBySurname(employeesurn);
            //Console.WriteLine();
            //Console.WriteLine("удалено!");
            //foreach (var employee in employeeService.GetAllEmployees())
            //{
            //    Console.WriteLine("{0} {1}  Роль: {2}", employee.EmployeeName, employee.EmployeeSurname, employee.RoleId);
            //}
            #endregion

            #region Role
            //Console.WriteLine("введите название роли:");
            //var rolename = Console.ReadLine();
            //roleService.CreateRole(rolename);
            //Console.WriteLine();
            //foreach(var role in roleService.GetRoles())
            //{
            //    Console.WriteLine("{0} {1}", role.Id, role.RoleName);
            //}
            //Console.WriteLine();
            //roleService.DeleteRoleById(4);
            //foreach (var roles in roleService.GetRoles())
            //{
            //    Console.WriteLine("{0} {1}", roles.Id, roles.RoleName);
            //}

            //var role1 = roleService.GetRoleById(2);
            //Console.WriteLine("{0}", role1.RoleName);
            #endregion

            #region ProjectRole
            //Console.WriteLine("введите название роли в проекте:");
            //var projectrolename = Console.ReadLine();
            //projectRoleService.CreateProjectRole(projectrolename);
            //Console.WriteLine();
            //foreach (var projectRole in projectRoleService.GetProjectRoles())
            //{
            //    Console.WriteLine("{0} {1}", projectRole.Id, projectRole.ProjectRoleName);
            //}
            //Console.WriteLine();
            //projectRoleService.DeleteProjectRoleById(6);
            //foreach (var projectRole in projectRoleService.GetProjectRoles())
            //{
            //    Console.WriteLine("{0} {1}", projectRole.Id, projectRole.ProjectRoleName);
            //}

            //var projectRole1 = projectRoleService.GetProjectRoleById(3);
            //Console.WriteLine("{0}", projectRole1.ProjectRoleName);
            #endregion

            #region ParticipationHistory

            #endregion


        }
    }
}
