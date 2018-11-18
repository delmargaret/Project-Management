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
    public class ScheduleServiceTests
    {
        readonly IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
        ScheduleValidator svalidator = new ScheduleValidator();

        [TestMethod]
        public void CreateScheduleTest()
        {
            ScheduleServise scheduleServise = new ScheduleServise(uow);
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
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var prw = projectWorkService.GetProjectWorkById(pWork.Id);

            ScheduleDTO schedule = new ScheduleDTO
            {
                ProjectWorkId = prw.Id,
                ScheduleDayId = 1
            };

            var sch = scheduleServise.CreateSchedule(schedule);
            ScheduleDTO actual = scheduleServise.GetScheduleById(sch.Id);
            ScheduleDTO expected = new ScheduleDTO
            {
                Id = actual.Id,
                ProjectWorkId = prw.Id,
                ScheduleDayId = 1
            };
            Assert.IsTrue(actual.Id == expected.Id && actual.ProjectWorkId == expected.ProjectWorkId
                && actual.ScheduleDayId == expected.ScheduleDayId);

            scheduleServise.DeleteScheduleById(actual.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void GetScheduleOnProjectWorkTest()
        {
            ScheduleServise scheduleServise = new ScheduleServise(uow);
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
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var prw = projectWorkService.GetProjectWorkById(pWork.Id);

            ScheduleDTO schedule1 = new ScheduleDTO
            {
                ProjectWorkId = prw.Id,
                ScheduleDayId = 1
            };

            var sch1 = scheduleServise.CreateSchedule(schedule1);
            ScheduleDTO actual1 = scheduleServise.GetScheduleById(sch1.Id);

            ScheduleDTO schedule2 = new ScheduleDTO
            {
                ProjectWorkId = prw.Id,
                ScheduleDayId = 3
            };

            var sch2 = scheduleServise.CreateSchedule(schedule2);
            ScheduleDTO actual2 = scheduleServise.GetScheduleById(sch2.Id);

            var expected = scheduleServise.GetScheduleOnProjectWork(prw.Id).ToList();

            Assert.IsTrue(expected.Count == 2);

            scheduleServise.DeleteScheduleById(actual1.Id);
            scheduleServise.DeleteScheduleById(actual2.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void GetEmployeesFreeDaysTest()
        {
            ScheduleServise scheduleServise = new ScheduleServise(uow);
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
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var prw = projectWorkService.GetProjectWorkById(pWork.Id);

            ScheduleDTO schedule1 = new ScheduleDTO
            {
                ProjectWorkId = prw.Id,
                ScheduleDayId = 1
            };

            var sch1 = scheduleServise.CreateSchedule(schedule1);
            ScheduleDTO actual1 = scheduleServise.GetScheduleById(sch1.Id);

            ScheduleDTO schedule2 = new ScheduleDTO
            {
                ProjectWorkId = prw.Id,
                ScheduleDayId = 3
            };

            var sch2 = scheduleServise.CreateSchedule(schedule2);
            ScheduleDTO actual2 = scheduleServise.GetScheduleById(sch2.Id);

            var expected = scheduleServise.GetEmployeesFreeDays(em.Id).ToList();

            Assert.IsTrue(expected.Count == 4);

            scheduleServise.DeleteScheduleById(actual1.Id);
            scheduleServise.DeleteScheduleById(actual2.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void DeleteScheduleByProjectWorkIdTest()
        {
            ScheduleServise scheduleServise = new ScheduleServise(uow);
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
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var prw = projectWorkService.GetProjectWorkById(pWork.Id);

            ScheduleDTO schedule1 = new ScheduleDTO
            {
                ProjectWorkId = prw.Id,
                ScheduleDayId = 1
            };

            var sch1 = scheduleServise.CreateSchedule(schedule1);
            ScheduleDTO actual1 = scheduleServise.GetScheduleById(sch1.Id);

            ScheduleDTO schedule2 = new ScheduleDTO
            {
                ProjectWorkId = prw.Id,
                ScheduleDayId = 3
            };

            var sch2 = scheduleServise.CreateSchedule(schedule2);
            ScheduleDTO actual2 = scheduleServise.GetScheduleById(sch2.Id);

            scheduleServise.DeleteScheduleByProjectWorkId(prw.Id);

            Assert.ThrowsException<NotFoundException>(() => scheduleServise.GetScheduleOnProjectWork(prw.Id));

            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void ChangeScheduleDayTest()
        {
            ScheduleServise scheduleServise = new ScheduleServise(uow);
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
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var prw = projectWorkService.GetProjectWorkById(pWork.Id);

            ScheduleDTO schedule = new ScheduleDTO
            {
                ProjectWorkId = prw.Id,
                ScheduleDayId = 1
            };

            var sch = scheduleServise.CreateSchedule(schedule);
            ScheduleDTO actual1 = scheduleServise.GetScheduleById(sch.Id);
            scheduleServise.ChangeScheduleDay(actual1.Id, 5);
            ScheduleDTO actual = scheduleServise.GetScheduleById(actual1.Id);

            ScheduleDTO expected = new ScheduleDTO
            {
                Id = actual.Id,
                ProjectWorkId = prw.Id,
                ScheduleDayId = 5
            };
            Assert.IsTrue(actual.Id == expected.Id && actual.ProjectWorkId == expected.ProjectWorkId
                && actual.ScheduleDayId == expected.ScheduleDayId);

            scheduleServise.DeleteScheduleById(actual.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void CreateScheduleIfEmployeeHasWorkloadTest()
        {
            ScheduleServise scheduleServise = new ScheduleServise(uow);
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
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var prw = projectWorkService.GetProjectWorkById(pWork.Id);
            projectWorkService.AddWorkLoad(prw.Id, 80);

            ScheduleDTO schedule = new ScheduleDTO
            {
                ProjectWorkId = prw.Id,
                ScheduleDayId = 1
            };

            Assert.ThrowsException<PercentOrScheduleException>(() => scheduleServise.CreateSchedule(schedule));

            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }
    }
}
