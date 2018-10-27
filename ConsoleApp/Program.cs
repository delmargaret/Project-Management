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
using System.ComponentModel.DataAnnotations;
using BLL.Mapping;
using Validation;
using Exeption;

namespace ConsoleApp
{

    class Program
    {
        static void Main(string[] args)
        {
            EmployeeValidator evalidator = new EmployeeValidator();
            ParticipationHistoryValidator phvalidator = new ParticipationHistoryValidator();
            IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            RoleService roleService = new RoleService(uow, new Map<Role, RoleDTO>());
            ProjectRoleService projectRoleService = new ProjectRoleService(uow, new Map<ProjectRole, ProjectRoleDTO>());
            ParticipationHistoryService historyService = new ParticipationHistoryService(uow, new Map<ParticipationHistory, ParticipationHistoryDTO>());
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());
            ProjectWorkService projectWorkService = new ProjectWorkService(uow, new Map<ProjectWork, ProjectWorkDTO>());
            ScheduleServise scheduleServise = new ScheduleServise(uow, new Map<Schedule, ScheduleDTO>(), new Map<ScheduleDay, ScheduleDayDTO>());
            try
            {
                //#region Employee
                //EmployeeDTO employee1 = new EmployeeDTO
                //{
                //    EmployeeName = "Иван",
                //    EmployeeSurname = "Иванов",
                //    EmployeePatronymic = "Иванович",
                //    RoleId = 1,
                //    Email = "i.van@mail.ru",
                //};
                //employeeService.CreateEmployee(employee1);

                //EmployeeDTO employee2 = new EmployeeDTO
                //{
                //    EmployeeName = "Ольга",
                //    EmployeeSurname = "Петрова",
                //    EmployeePatronymic = "Николаевна",
                //    RoleId = 3,
                //    Email = "Olya@mail.ru",
                //};
                //employeeService.CreateEmployee(employee2);

                //EmployeeDTO employee3 = new EmployeeDTO
                //{
                //    Id = 23,
                //    EmployeeName = "Елизавета",
                //    EmployeeSurname = "Кот",
                //    EmployeePatronymic = "Андреевна",
                //    RoleId = 3,
                //    Email = "Lisa@mail.ru",
                //};
                //employeeService.CreateEmployee(employee3);
                //var employees = employeeService.GetAllEmployees();
                //foreach (var employee in employees)
                //{
                //    Console.WriteLine("{0} {1}  Роль: {2}", employee.Id, employee.EmployeeSurname, employee.RoleId);
                //}

                //EmployeeDTO employee4 = new EmployeeDTO
                //{
                //    EmployeeName = "Андрей",
                //    EmployeeSurname = "Зайцев",
                //    EmployeePatronymic = "Анатольевич",
                //    Email = "Andr",
                //};
                //var errors = evalidator.Validate(employee4);
                //if (errors.Count() != 0)
                //{
                //    foreach (var error in errors)
                //    {
                //        if (error.MemberNames.Count() != 0)
                //        {
                //            foreach (var membername in error.MemberNames)
                //            {
                //                Console.WriteLine(membername.ToString() + ": " + error.ErrorMessage);
                //            }
                //        }
                //        else
                //        {
                //            Console.WriteLine(error.ErrorMessage);
                //        }
                //    }
                //}
                //else
                //{
                //    employeeService.CreateEmployee(employee4);
                //    var employeess = employeeService.GetAllEmployees();
                //    foreach (var employee in employeess)
                //    {
                //        Console.WriteLine("{0} {1}  Роль: {2}", employee.EmployeeName, employee.EmployeeSurname, employee.RoleId);
                //    }
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

                //Console.WriteLine("введите id сотрудника для изменения email");
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
                //#endregion

                //#region Project
                //ProjectDTO project1 = new ProjectDTO
                //{
                //    ProjectName = "проект 1",
                //    ProjectDescription = "проект номер один",
                //    ProjectStartDate = new DateTimeOffset(2018, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                //    ProjectEndDate = new DateTimeOffset(2018, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
                //};
                //ProjectDTO project2 = new ProjectDTO
                //{
                //    ProjectName = "проект 2",
                //    ProjectDescription = "проект номер два",
                //    ProjectStartDate = new DateTimeOffset(2018, 11, 1, 10, 15, 35, new TimeSpan(3, 0, 0)),
                //    ProjectEndDate = new DateTimeOffset(2018, 12, 11, 10, 15, 35, new TimeSpan(3, 0, 0))
                //};
                //projectService.CreateProject(project1);
                //projectService.CreateProject(project2);
                //var projects = projectService.GetAllProjects();
                //foreach (var proj in projects)
                //{
                //    Console.WriteLine("{0}", proj.ProjectName);
                //}
                //Console.WriteLine();

                //var projects2 = projectService.GetAllProjectsByStatusId(4);
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
                //Console.WriteLine();
                //var pr = projectService.GetProjectsEndingInNDays(6);
                //foreach (var proj in pr)
                //{
                //    Console.WriteLine("{0}", proj.ProjectName);
                //}
                //#endregion

                //#region ProjectWork
                //ProjectWorkDTO work1 = new ProjectWorkDTO
                //{
                //    EmployeeId = 3,
                //    ProjectId = 1,
                //    ProjectRoleId = 2,
                //};

                //ProjectWorkDTO work2 = new ProjectWorkDTO
                //{
                //    EmployeeId = 3,
                //    ProjectId = 1,
                //    ProjectRoleId = 4,
                //};

                //ProjectWorkDTO work3 = new ProjectWorkDTO
                //{
                //    EmployeeId = 2,
                //    ProjectId = 1,
                //    ProjectRoleId = 1,
                //};

                //ProjectWorkDTO work4 = new ProjectWorkDTO
                //{
                //    EmployeeId = 2,
                //    ProjectId = 2,
                //    ProjectRoleId = 3,
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

                //var works2 = projectWorkService.GetEmployeesProjects(2);
                //foreach (var work in works2)
                //{
                //    Console.WriteLine("{0} {1} {2} ", work.ProjectId, work.ProjectRoleId, work.WorkLoad);
                //}
                //Console.WriteLine();

                //var works3 = projectWorkService.GetNamesAndLoadOnProject(1);
                //foreach (var work in works3)
                //{
                //    Console.WriteLine("{0} {1} {2} ", work.name, work.role, work.workload);
                //}
                //Console.WriteLine();

                //var works4 = projectWorkService.GetNamesOnProject(2);
                //foreach (var work in works4)
                //{
                //    Console.WriteLine("{0} {1}", work.name, work.role);
                //}
                //Console.WriteLine();


                //projectWorkService.ChangeEmployeesProjectRole(1, 4);
                //var works6 = projectWorkService.GetAllProjectWorks();
                //foreach (var work in works6)
                //{
                //    Console.WriteLine("{0} {1} {2} {3}", work.ProjectId, work.EmployeeId, work.ProjectRoleId, work.WorkLoad);
                //}

                //projectWorkService.AddWorkLoad(3, 30);
                //projectWorkService.AddWorkLoad(4, 50);
                //Console.WriteLine("Load: {0}%", projectWorkService.CalculateEmployeesWorkload(2));

                //#endregion

                //#region ParticipationHistory
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
                //#endregion

                //#region Schedule

                //ScheduleDTO schedule = new ScheduleDTO
                //{
                //    ProjectWorkId = 1,
                //    ScheduleDayId = 2
                //};
                //scheduleServise.CreateSchedule(schedule);

                //projectWorkService.AddWorkLoad(2, 30);
                //Console.WriteLine();

                //foreach (var work in projectWorkService.GetNamesAndLoadOnProject(1))
                //{
                //    Console.WriteLine("{0} {1} {2} ", work.name, work.role, work.workload);
                //}
                //Console.WriteLine();
                //#endregion
            }

            catch (NotFoundException)
            {
                Console.WriteLine("объект не найден");
            }
            catch (ObjectAlreadyExistsException)
            {
                Console.WriteLine("объект уже создан");
            }
            catch (InvalidDateException)
            {
                Console.WriteLine("неверная дата");
            }
            catch (PercentOrScheduleException)
            {
                Console.WriteLine("неверный тип загруженности");
            }
        }
    }
}
