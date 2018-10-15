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
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            ScheduleServise scheduleServise = new ScheduleServise(uow);

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

            #region Project
            //ProjectDTO project1 = new ProjectDTO
            //{
            //    ProjectName = "проект 1",
            //    ProjectDescription = "проект номер один",
            //    ProjectStartDate = new DateTimeOffset(2018, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
            //    ProjectEndDate = new DateTimeOffset(2018, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0)),
            //    ProjectStatusId = 1
            //};
            //ProjectDTO project2 = new ProjectDTO
            //{
            //    ProjectName = "проект 2",
            //    ProjectDescription = "проект номер два",
            //    ProjectStartDate = new DateTimeOffset(2018, 11, 1, 10, 15, 35, new TimeSpan(3, 0, 0)),
            //    ProjectEndDate = new DateTimeOffset(2018, 12, 11, 10, 15, 35, new TimeSpan(3, 0, 0)),
            //    ProjectStatusId = 1
            //};
            //projectService.CreateProject(project1);
            //projectService.CreateProject(project2);
            //var projects = projectService.GetAllProjects();
            //foreach(var proj in projects)
            //{
            //    Console.WriteLine("{0}", proj.ProjectName);
            //}
            //Console.WriteLine();

            //var projects2 = projectService.GetAllOpenedProjects();
            //foreach (var proj in projects2)
            //{
            //    Console.WriteLine("{0}", proj.ProjectName);
            //}
            //Console.WriteLine();

            //projectService.ChangeProjectName(1, "Проектик");
            //Console.WriteLine("{0}", projectService.GetProjectById(1).ProjectName);
            //Console.WriteLine();

            //projectService.ChangeProjectStatus(2, 2);
            //var projects4 = projectService.GetAllClosedProjects();
            //foreach (var proj in projects4)
            //{
            //    Console.WriteLine("{0}", proj.ProjectName);
            //}

            //var pr = projectService.GetProjectsEndingInNDays(6);
            //foreach (var proj in pr)
            //{
            //    Console.WriteLine("{0}", proj.ProjectName);
            //}

            #endregion

            #region ProjectWork
            //ProjectWorkDTO work1 = new ProjectWorkDTO
            //{
            //    EmployeeId = 3,
            //    ProjectId = 1,
            //    ProjectRoleId = 2,
            //    WorkLoad = 30
            //};

            //ProjectWorkDTO work2 = new ProjectWorkDTO
            //{
            //    EmployeeId = 4,
            //    ProjectId = 1,
            //    ProjectRoleId = 4,
            //    WorkLoad = 50
            //};

            //ProjectWorkDTO work3 = new ProjectWorkDTO
            //{
            //    EmployeeId = 4,
            //    ProjectId = 1,
            //    ProjectRoleId = 1,
            //    WorkLoad = 50
            //};

            //ProjectWorkDTO work4 = new ProjectWorkDTO
            //{
            //    EmployeeId = 4,
            //    ProjectId = 2,
            //    ProjectRoleId = 3,
            //    WorkLoad = 20
            //};

            //projectWorkService.CreateProjectWork(work1);
            //projectWorkService.CreateProjectWork(work2);
            //projectWorkService.CreateProjectWork(work3);
            //projectWorkService.CreateProjectWork(work4);

            //var works = projectWorkService.GetAllProjectWorks();
            //foreach (var work in works)
            //{
            //    Console.WriteLine("{0} {1} {2} {3}", work.ProjectId, work.EmployeeId, work.ProjectRoleId, work.WorkLoad);
            //}
            //Console.WriteLine();

            //var works2 = projectWorkService.GetEmployeesProjects(4);
            //foreach (var work in works2)
            //{
            //    Console.WriteLine("{0} {1} {2} ", work.ProjectId, work.ProjectRoleId, work.WorkLoad);
            //}
            //Console.WriteLine();

            //var works3 = projectWorkService.GetNamesAndLoadOnProject(2);
            //foreach (var work in works3)
            //{
            //    Console.WriteLine("{0} {1} {2} ", work.EmployeeId, work.ProjectRoleId, work.WorkLoad);
            //}
            //Console.WriteLine();

            //var works4 = projectWorkService.GetNamesOnProject(2);
            //foreach (var work in works4)
            //{
            //    Console.WriteLine("{0} {1}", work.name, work.role);
            //}
            //Console.WriteLine();

            //ProjectWorkDTO work5 = new ProjectWorkDTO
            //{
            //    EmployeeId = 4,
            //    ProjectId = 2,
            //    ProjectRoleId = 3,
            //    WorkLoad = 20
            //};

            //projectWorkService.CreateProjectWork(work5);
            //var works5 = projectWorkService.GetAllProjectWorks();
            //foreach (var work in works5)
            //{
            //    Console.WriteLine("{0} {1} {2} {3}", work.ProjectId, work.EmployeeId, work.ProjectRoleId, work.WorkLoad);
            //}
            //Console.WriteLine();

            //projectWorkService.ChangeEmployeesProjectRole(1, 4);
            //var works6 = projectWorkService.GetAllProjectWorks();
            //foreach (var work in works6)
            //{
            //    Console.WriteLine("{0} {1} {2} {3}", work.ProjectId, work.EmployeeId, work.ProjectRoleId, work.WorkLoad);
            //}

            #endregion

            #region ParticipationHistory
            //ParticipationHistoryDTO history = new ParticipationHistoryDTO
            //{
            //    ProjectWorkId = 2,
            //    StartDate = new DateTimeOffset(2018, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
            //    EndDate = new DateTimeOffset(2018, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            //};
            //historyService.CreateHistory(history);

            //foreach (var hist in historyService.GetAllEmployeesHistoriesOnProject(2))
            //{
            //    Console.WriteLine("{0}", hist.Id);
            //}

            //Console.WriteLine();
            //historyService.ChangeHistoryEndDate(1, new DateTimeOffset(2018, 10, 15, 10, 15, 35, new TimeSpan(3, 0, 0)));

            //foreach (var hist in historyService.GetAllEmployeesHistoriesOnProject(2))
            //{
            //    Console.WriteLine("{0}", hist.Id);
            //}
            //Console.WriteLine();

            //ParticipationHistoryDTO history2 = new ParticipationHistoryDTO
            //{
            //    ProjectWorkId = 2,
            //    StartDate = new DateTimeOffset(2018, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
            //    EndDate = new DateTimeOffset(2018, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            //};

            //historyService.CreateHistory(history2);
            //Console.WriteLine("{0}", historyService.GetLastEmployeesHistory(2).Id);
            #endregion

            #region Schedule
            //ProjectDTO project1 = new ProjectDTO
            //{
            //    ProjectName = "проект 1",
            //    ProjectDescription = "проект номер один",
            //    ProjectStartDate = new DateTimeOffset(2018, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
            //    ProjectEndDate = new DateTimeOffset(2018, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0)),
            //    ProjectStatusId = 1
            //};
            //ProjectDTO project2 = new ProjectDTO
            //{
            //    ProjectName = "проект 2",
            //    ProjectDescription = "проект номер два",
            //    ProjectStartDate = new DateTimeOffset(2018, 11, 1, 10, 15, 35, new TimeSpan(3, 0, 0)),
            //    ProjectEndDate = new DateTimeOffset(2018, 12, 11, 10, 15, 35, new TimeSpan(3, 0, 0)),
            //    ProjectStatusId = 1
            //};
            //projectService.CreateProject(project1);
            //projectService.CreateProject(project2);

            //EmployeeDTO employee1 = new EmployeeDTO
            //{
            //    EmployeeName = "Кирилл",
            //    EmployeeSurname = "Петров",
            //    EmployeePatronymic = "Иванович",
            //    RoleId = 3
            //};

            //EmployeeDTO employee2 = new EmployeeDTO
            //{
            //    EmployeeName = "Зоя",
            //    EmployeeSurname = "Зайцева",
            //    EmployeePatronymic = "Андреевна",
            //    RoleId = 3
            //};
            //employeeService.CreateEmployee(employee1);
            //employeeService.CreateEmployee(employee2);

            //ProjectWorkDTO work1 = new ProjectWorkDTO
            //{
            //    EmployeeId = 1,
            //    ProjectId = 1,
            //    ProjectRoleId = 2,
            //};


            //ProjectWorkDTO work2 = new ProjectWorkDTO
            //{
            //    EmployeeId = 2,
            //    ProjectId = 1,
            //    ProjectRoleId = 3,
            //};
            //projectWorkService.CreateProjectWork(work1);
            //projectWorkService.CreateProjectWork(work2);

            //ScheduleDTO schedule = new ScheduleDTO
            //{
            //    ProjectWorkId = 1,
            //    ScheduleDayId = 2
            //};
            //scheduleServise.CreateSchedule(schedule);

            //projectWorkService.AddWorkLoad(2, 30);
            //Console.WriteLine();

            //ProjectWorkDTO work3 = new ProjectWorkDTO
            //{
            //    EmployeeId = 1,
            //    ProjectId = 1,
            //    ProjectRoleId = 3,
            //};


            //ProjectWorkDTO work4 = new ProjectWorkDTO
            //{
            //    EmployeeId = 2,
            //    ProjectId = 2,
            //    ProjectRoleId = 3,
            //};
            //projectWorkService.CreateProjectWork(work3);
            //projectWorkService.CreateProjectWork(work4);

            projectWorkService.AddWorkLoad(3, 50);

            #endregion
        }
    }
}
