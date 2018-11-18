using System;
using System.Linq;
using BLL.DTO;
using BLL.Mapping;
using BLL.Services;
using DAL.Entities;
using Exeption;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Interfaces;
using Repository.Repositories;
using Validation;

namespace ProjectManagement.Tests
{
    [TestClass]
    public class ProjectWorkServiceTests
    {
        ProjectWorkValidator pwvalidator = new ProjectWorkValidator();
        readonly IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");

        [TestMethod]
        public void CreateProjectWorkTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork = employeeService.CreateEmployee(employee);
            var em = employeeService.GetEmployeeById(employeeOnWork.Id);

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj = projectService.CreateProject(project);
            var pr = projectService.GetProjectById(proj.Id);

            ProjectWorkDTO projectWork = new ProjectWorkDTO
            {
                EmployeeId = em.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 3
            };
            var errors = pwvalidator.Validate(projectWork);
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var actual = projectWorkService.GetProjectWorkById(pWork.Id);

            ProjectWorkDTO expected = new ProjectWorkDTO
            {
                Id = actual.Id,
                EmployeeId = em.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 3
            };
            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.EmployeeId == expected.EmployeeId &&
                actual.ProjectId == expected.ProjectId && actual.ProjectRoleId == expected.ProjectRoleId);

            projectWorkService.DeleteProjectWorkById(actual.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void DeleteEmployeeFromProjectTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            EmployeeDTO employee2 = new EmployeeDTO
            {
                EmployeeName = "Иван",
                EmployeeSurname = "Иванов",
                EmployeePatronymic = "Иванович",
                RoleId = 3,
                Email = "ivan@mail.ru",
            };
            var employeeOnWork2 = employeeService.CreateEmployee(employee2);
            var em2 = employeeService.GetEmployeeById(employeeOnWork2.Id);

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj = projectService.CreateProject(project);
            var pr = projectService.GetProjectById(proj.Id);

            ProjectWorkDTO projectWork1 = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 3
            };
            var errors1 = pwvalidator.Validate(projectWork1);
            var pWork1 = projectWorkService.CreateProjectWork(projectWork1);
            var actual1 = projectWorkService.GetProjectWorkById(pWork1.Id);

            ProjectWorkDTO projectWork2 = new ProjectWorkDTO
            {
                EmployeeId = em2.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 5
            };
            var errors2 = pwvalidator.Validate(projectWork2);
            var pWork2 = projectWorkService.CreateProjectWork(projectWork2);
            var actual2 = projectWorkService.GetProjectWorkById(pWork2.Id);

            projectWorkService.DeleteEmployeeFromProject(pr.Id, em1.Id);
            var result = projectWorkService.GetNamesOnProject(pr.Id).ToList();
            Assert.AreEqual(result.Count, 1);

            projectWorkService.DeleteProjectWorkById(actual2.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            employeeService.DeleteEmployeeById(em2.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void DeleteProjectWorkByIdIfItsNotFound()
        {
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            Assert.ThrowsException<NotFoundException>(() => projectWorkService.DeleteProjectWorkById(1235));
        }

        [TestMethod]
        public void GetAllProjectWorksTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            EmployeeDTO employee2 = new EmployeeDTO
            {
                EmployeeName = "Иван",
                EmployeeSurname = "Иванов",
                EmployeePatronymic = "Иванович",
                RoleId = 3,
                Email = "ivan@mail.ru",
            };
            var employeeOnWork2 = employeeService.CreateEmployee(employee2);
            var em2 = employeeService.GetEmployeeById(employeeOnWork2.Id);

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj = projectService.CreateProject(project);
            var pr = projectService.GetProjectById(proj.Id);

            ProjectWorkDTO projectWork1 = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 3
            };
            var errors1 = pwvalidator.Validate(projectWork1);
            var pWork1 = projectWorkService.CreateProjectWork(projectWork1);
            var actual1 = projectWorkService.GetProjectWorkById(pWork1.Id);

            ProjectWorkDTO projectWork2 = new ProjectWorkDTO
            {
                EmployeeId = em2.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 5
            };
            var errors2 = pwvalidator.Validate(projectWork2);
            var pWork2 = projectWorkService.CreateProjectWork(projectWork2);
            var actual2 = projectWorkService.GetProjectWorkById(pWork2.Id);

            var result = projectWorkService.GetAllProjectWorks().ToList();
            Assert.AreEqual(result.Count, 2);

            projectWorkService.DeleteProjectWorkById(actual1.Id);
            projectWorkService.DeleteProjectWorkById(actual2.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            employeeService.DeleteEmployeeById(em2.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void CreateProjectWorkIfItsAlreadyExistsTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork = employeeService.CreateEmployee(employee);
            var em = employeeService.GetEmployeeById(employeeOnWork.Id);

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj = projectService.CreateProject(project);
            var pr = projectService.GetProjectById(proj.Id);

            ProjectWorkDTO projectWork = new ProjectWorkDTO
            {
                EmployeeId = em.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 3
            };
            var errors = pwvalidator.Validate(projectWork);
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var actual = projectWorkService.GetProjectWorkById(pWork.Id);
            Assert.ThrowsException<ObjectAlreadyExistsException>(() => projectWorkService.CreateProjectWork(projectWork));

            projectWorkService.DeleteProjectWorkById(actual.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void GetEmployeesProjectsTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            ProjectDTO project1 = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj1 = projectService.CreateProject(project1);
            var pr1 = projectService.GetProjectById(proj1.Id);

            ProjectDTO project2 = new ProjectDTO
            {
                ProjectName = "проект 2",
                ProjectDescription = "проект номер два",
                ProjectStartDate = new DateTimeOffset(2021, 2, 14, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 8, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj2 = projectService.CreateProject(project2);
            var pr2 = projectService.GetProjectById(proj2.Id);

            ProjectWorkDTO projectWork1 = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr1.Id,
                ProjectRoleId = 3
            };
            var errors1 = pwvalidator.Validate(projectWork1);
            var pWork1 = projectWorkService.CreateProjectWork(projectWork1);
            var actual1 = projectWorkService.GetProjectWorkById(pWork1.Id);

            ProjectWorkDTO projectWork2 = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr2.Id,
                ProjectRoleId = 5
            };
            var errors2 = pwvalidator.Validate(projectWork2);
            var pWork2 = projectWorkService.CreateProjectWork(projectWork2);
            var actual2 = projectWorkService.GetProjectWorkById(pWork2.Id);

            var result = projectWorkService.GetEmployeesProjects(em1.Id).ToList();
            Assert.AreEqual(result.Count, 2);

            projectWorkService.DeleteProjectWorkById(actual1.Id);
            projectWorkService.DeleteProjectWorkById(actual2.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            projectService.DeleteProjectById(pr1.Id);
            projectService.DeleteProjectById(pr2.Id);
        }

        [TestMethod]
        public void CalculateEmployeesWorkloadTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            ProjectDTO project1 = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj1 = projectService.CreateProject(project1);
            var pr1 = projectService.GetProjectById(proj1.Id);

            ProjectDTO project2 = new ProjectDTO
            {
                ProjectName = "проект 2",
                ProjectDescription = "проект номер два",
                ProjectStartDate = new DateTimeOffset(2021, 2, 14, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 8, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj2 = projectService.CreateProject(project2);
            var pr2 = projectService.GetProjectById(proj2.Id);

            ProjectWorkDTO projectWork1 = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr1.Id,
                ProjectRoleId = 3
            };
            var errors1 = pwvalidator.Validate(projectWork1);
            var pWork1 = projectWorkService.CreateProjectWork(projectWork1);
            var actual1 = projectWorkService.GetProjectWorkById(pWork1.Id);
            projectWorkService.AddWorkLoad(actual1.Id, 20);

            ProjectWorkDTO projectWork2 = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr2.Id,
                ProjectRoleId = 5
            };
            var errors2 = pwvalidator.Validate(projectWork2);
            var pWork2 = projectWorkService.CreateProjectWork(projectWork2);
            var actual2 = projectWorkService.GetProjectWorkById(pWork2.Id);
            projectWorkService.AddWorkLoad(actual2.Id, 50);

            var result = projectWorkService.CalculateEmployeesWorkload(em1.Id);
            Assert.AreEqual(result, 70);

            projectWorkService.DeleteProjectWorkById(actual1.Id);
            projectWorkService.DeleteProjectWorkById(actual2.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            projectService.DeleteProjectById(pr1.Id);
            projectService.DeleteProjectById(pr2.Id);
        }

        [TestMethod]
        public void GetNamesAndLoadOnProjectTest()
        {
            ScheduleServise scheduleServise = new ScheduleServise(uow);
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            EmployeeDTO employee2 = new EmployeeDTO
            {
                EmployeeName = "Иван",
                EmployeeSurname = "Иванов",
                EmployeePatronymic = "Иванович",
                RoleId = 3,
                Email = "ivan@mail.ru",
            };
            var employeeOnWork2 = employeeService.CreateEmployee(employee2);
            var em2 = employeeService.GetEmployeeById(employeeOnWork2.Id);

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj = projectService.CreateProject(project);
            var pr = projectService.GetProjectById(proj.Id);

            ProjectWorkDTO projectWork1 = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 3
            };
            var errors1 = pwvalidator.Validate(projectWork1);
            var pWork1 = projectWorkService.CreateProjectWork(projectWork1);
            var actual1 = projectWorkService.GetProjectWorkById(pWork1.Id);
            projectWorkService.AddWorkLoad(actual1.Id, 50);

            ProjectWorkDTO projectWork2 = new ProjectWorkDTO
            {
                EmployeeId = em2.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 5
            };
            var errors2 = pwvalidator.Validate(projectWork2);
            var pWork2 = projectWorkService.CreateProjectWork(projectWork2);
            var actual2 = projectWorkService.GetProjectWorkById(pWork2.Id);
            scheduleServise.CreateSchedule(new ScheduleDTO { ProjectWorkId = actual2.Id, ScheduleDayId = 2 });
            scheduleServise.CreateSchedule(new ScheduleDTO { ProjectWorkId = actual2.Id, ScheduleDayId = 4 });

            var result = projectWorkService.GetNamesAndLoadOnProject(pr.Id).ToList();

            Assert.IsTrue(result.First().name == "Антонович Екатерина Алексеевна" && result.Last().name=="Иванов Иван Иванович" 
                && result.First().workload=="50%" && result.Last().workload=="Вторник Четверг ");

            projectWorkService.DeleteProjectWorkById(actual1.Id);
            projectWorkService.DeleteProjectWorkById(actual2.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            employeeService.DeleteEmployeeById(em2.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void GetNamesOnProjectTest()
        {
            ScheduleServise scheduleServise = new ScheduleServise(uow);
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            EmployeeDTO employee2 = new EmployeeDTO
            {
                EmployeeName = "Иван",
                EmployeeSurname = "Иванов",
                EmployeePatronymic = "Иванович",
                RoleId = 3,
                Email = "ivan@mail.ru",
            };
            var employeeOnWork2 = employeeService.CreateEmployee(employee2);
            var em2 = employeeService.GetEmployeeById(employeeOnWork2.Id);

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj = projectService.CreateProject(project);
            var pr = projectService.GetProjectById(proj.Id);

            ProjectWorkDTO projectWork1 = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 3
            };
            var errors1 = pwvalidator.Validate(projectWork1);
            var pWork1 = projectWorkService.CreateProjectWork(projectWork1);
            var actual1 = projectWorkService.GetProjectWorkById(pWork1.Id);
            projectWorkService.AddWorkLoad(actual1.Id, 50);

            ProjectWorkDTO projectWork2 = new ProjectWorkDTO
            {
                EmployeeId = em2.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 5
            };
            var errors2 = pwvalidator.Validate(projectWork2);
            var pWork2 = projectWorkService.CreateProjectWork(projectWork2);
            var actual2 = projectWorkService.GetProjectWorkById(pWork2.Id);
            scheduleServise.CreateSchedule(new ScheduleDTO { ProjectWorkId = actual2.Id, ScheduleDayId = 2 });
            scheduleServise.CreateSchedule(new ScheduleDTO { ProjectWorkId = actual2.Id, ScheduleDayId = 4 });

            var result = projectWorkService.GetNamesOnProject(pr.Id).ToList();

            Assert.IsTrue(result.First().name == "Антонович Екатерина Алексеевна" && result.Last().name == "Иванов Иван Иванович");

            projectWorkService.DeleteProjectWorkById(actual1.Id);
            projectWorkService.DeleteProjectWorkById(actual2.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            employeeService.DeleteEmployeeById(em2.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void ChangeEmployeeTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            EmployeeDTO employee2 = new EmployeeDTO
            {
                EmployeeName = "Иван",
                EmployeeSurname = "Иванов",
                EmployeePatronymic = "Иванович",
                RoleId = 3,
                Email = "ivan@mail.ru",
            };
            var employeeOnWork2 = employeeService.CreateEmployee(employee2);
            var em2 = employeeService.GetEmployeeById(employeeOnWork2.Id);

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj = projectService.CreateProject(project);
            var pr = projectService.GetProjectById(proj.Id);

            ProjectWorkDTO projectWork = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 3
            };
            var errors = pwvalidator.Validate(projectWork);
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            projectWorkService.ChangeEmployee(pWork.Id, em2.Id);
            var actual = projectWorkService.GetProjectWorkById(pWork.Id);


            ProjectWorkDTO expected = new ProjectWorkDTO
            {
                Id = actual.Id,
                EmployeeId = em2.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 3
            };
            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.EmployeeId == expected.EmployeeId &&
                actual.ProjectId == expected.ProjectId && actual.ProjectRoleId == expected.ProjectRoleId);

            projectWorkService.DeleteProjectWorkById(actual.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            employeeService.DeleteEmployeeById(em2.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void ChangeEmployeesProjectRoleTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj = projectService.CreateProject(project);
            var pr = projectService.GetProjectById(proj.Id);

            ProjectWorkDTO projectWork = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 3
            };
            var errors = pwvalidator.Validate(projectWork);
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            projectWorkService.ChangeEmployeesProjectRole(pWork.Id, 6);
            var actual = projectWorkService.GetProjectWorkById(pWork.Id);


            ProjectWorkDTO expected = new ProjectWorkDTO
            {
                Id = actual.Id,
                EmployeeId = em1.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 6
            };
            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.EmployeeId == expected.EmployeeId &&
                actual.ProjectId == expected.ProjectId && actual.ProjectRoleId == expected.ProjectRoleId);

            projectWorkService.DeleteProjectWorkById(actual.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void ChangeProjectTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            ProjectDTO project1 = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj1 = projectService.CreateProject(project1);
            var pr1 = projectService.GetProjectById(proj1.Id);

            ProjectDTO project2 = new ProjectDTO
            {
                ProjectName = "проект 2",
                ProjectDescription = "проект номер два",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj2 = projectService.CreateProject(project2);
            var pr2 = projectService.GetProjectById(proj2.Id);

            ProjectWorkDTO projectWork = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr1.Id,
                ProjectRoleId = 3
            };
            var errors = pwvalidator.Validate(projectWork);
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            projectWorkService.ChangeProject(pWork.Id, pr2.Id);
            var actual = projectWorkService.GetProjectWorkById(pWork.Id);

            ProjectWorkDTO expected = new ProjectWorkDTO
            {
                Id = actual.Id,
                EmployeeId = em1.Id,
                ProjectId = pr2.Id,
                ProjectRoleId = 3
            };
            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.EmployeeId == expected.EmployeeId &&
                actual.ProjectId == expected.ProjectId && actual.ProjectRoleId == expected.ProjectRoleId);

            projectWorkService.DeleteProjectWorkById(actual.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            projectService.DeleteProjectById(pr1.Id);
            projectService.DeleteProjectById(pr2.Id);
        }

        [TestMethod]
        public void ChangeWorkLoadTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            ProjectDTO project1 = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj1 = projectService.CreateProject(project1);
            var pr1 = projectService.GetProjectById(proj1.Id);

            ProjectWorkDTO projectWork = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr1.Id,
                ProjectRoleId = 3
            };
            var errors = pwvalidator.Validate(projectWork);
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var actual1 = projectWorkService.GetProjectWorkById(pWork.Id);
            projectWorkService.AddWorkLoad(actual1.Id, 20);
            var actual2 = projectWorkService.GetProjectWorkById(actual1.Id);
            projectWorkService.ChangeWorkLoad(pWork.Id, 60);
            var actual = projectWorkService.GetProjectWorkById(actual2.Id);

            ProjectWorkDTO expected = new ProjectWorkDTO
            {
                Id = actual.Id,
                EmployeeId = em1.Id,
                ProjectId = pr1.Id,
                ProjectRoleId = 3,
                WorkLoad = 60
            };
            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.EmployeeId == expected.EmployeeId &&
                actual.ProjectId == expected.ProjectId && actual.ProjectRoleId == expected.ProjectRoleId && 
                actual.WorkLoad == expected.WorkLoad);

            projectWorkService.DeleteProjectWorkById(actual.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            projectService.DeleteProjectById(pr1.Id);
        }

        [TestMethod]
        public void DeleteWorkLoadTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ProjectWorkService projectWorkService = new ProjectWorkService(uow);
            EmployeeDTO employee1 = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var employeeOnWork1 = employeeService.CreateEmployee(employee1);
            var em1 = employeeService.GetEmployeeById(employeeOnWork1.Id);

            ProjectDTO project1 = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 10, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
            };
            var proj1 = projectService.CreateProject(project1);
            var pr1 = projectService.GetProjectById(proj1.Id);

            ProjectWorkDTO projectWork = new ProjectWorkDTO
            {
                EmployeeId = em1.Id,
                ProjectId = pr1.Id,
                ProjectRoleId = 3
            };
            var errors = pwvalidator.Validate(projectWork);
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var actual1 = projectWorkService.GetProjectWorkById(pWork.Id);
            projectWorkService.AddWorkLoad(actual1.Id, 20);
            var actual2 = projectWorkService.GetProjectWorkById(actual1.Id);
            projectWorkService.DeleteWorkLoad(pWork.Id);
            var actual = projectWorkService.GetProjectWorkById(actual2.Id);

            ProjectWorkDTO expected = new ProjectWorkDTO
            {
                Id = actual.Id,
                EmployeeId = em1.Id,
                ProjectId = pr1.Id,
                ProjectRoleId = 3,
                WorkLoad = null
            };
            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.EmployeeId == expected.EmployeeId &&
                actual.ProjectId == expected.ProjectId && actual.ProjectRoleId == expected.ProjectRoleId &&
                actual.WorkLoad == expected.WorkLoad);

            projectWorkService.DeleteProjectWorkById(actual.Id);
            employeeService.DeleteEmployeeById(em1.Id);
            projectService.DeleteProjectById(pr1.Id);
        }
    }
}
