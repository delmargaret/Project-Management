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
    public class ParticipationHistoryTests
    {
        ParticipationHistoryValidator phvalidator = new ParticipationHistoryValidator();
        readonly IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");

        [TestMethod]
        public void CreateHistoryTest()
        {
            ParticipationHistoryService participationHistoryService = new ParticipationHistoryService(uow);
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

            ParticipationHistoryDTO participationHistory = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors = phvalidator.Validate(participationHistory);
            var history = participationHistoryService.CreateHistory(participationHistory);
            var actual = participationHistoryService.GetHistoryById(history.Id);

            ParticipationHistoryDTO expected = new ParticipationHistoryDTO
            {
                Id = actual.Id,
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.ProjectWorkId == expected.ProjectWorkId &&
                actual.StartDate == expected.StartDate && actual.EndDate == expected.EndDate);

            participationHistoryService.DeleteHistoryById(actual.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void GetLastEmployeesHistoryTest()
        {
            ParticipationHistoryService participationHistoryService = new ParticipationHistoryService(uow);
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
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
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

            ParticipationHistoryDTO participationHistory1 = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 04, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors = phvalidator.Validate(participationHistory1);
            var history1 = participationHistoryService.CreateHistory(participationHistory1);
            var actual1 = participationHistoryService.GetHistoryById(history1.Id);

            ParticipationHistoryDTO participationHistory2 = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 11, 05, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors2 = phvalidator.Validate(participationHistory2);
            var history2 = participationHistoryService.CreateHistory(participationHistory2);
            var actual2 = participationHistoryService.GetHistoryById(history2.Id);

            var actual = participationHistoryService.GetLastEmployeesHistory(prw.Id);
            ParticipationHistoryDTO expected = new ParticipationHistoryDTO
            {
                Id = actual2.Id,
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 11, 05, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            Assert.IsTrue(errors.Count == 0 && errors2.Count == 0 && actual.Id == expected.Id && actual.ProjectWorkId == expected.ProjectWorkId &&
    actual.StartDate == expected.StartDate && actual.EndDate == expected.EndDate);

            participationHistoryService.DeleteHistoryById(actual1.Id);
            participationHistoryService.DeleteHistoryById(actual2.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void GetAllHistoriesTest()
        {
            ParticipationHistoryService participationHistoryService = new ParticipationHistoryService(uow);
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

            EmployeeDTO employee2 = new EmployeeDTO
            {
                EmployeeName = "Иван",
                EmployeeSurname = "Иванов",
                EmployeePatronymic = "Иванович",
                RoleId = 2,
                Email = "vanya@mail.ru",
            };
            var employeeOnWork2 = employeeService.CreateEmployee(employee2);
            var em2 = employeeService.GetEmployeeById(employeeOnWork2.Id);

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
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

            ProjectWorkDTO projectWork2 = new ProjectWorkDTO
            {
                EmployeeId = em2.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 2
            };
            var pWork2 = projectWorkService.CreateProjectWork(projectWork2);
            var prw2 = projectWorkService.GetProjectWorkById(pWork2.Id);

            ParticipationHistoryDTO participationHistory1 = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 04, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors = phvalidator.Validate(participationHistory1);
            var history1 = participationHistoryService.CreateHistory(participationHistory1);
            var actual1 = participationHistoryService.GetHistoryById(history1.Id);

            ParticipationHistoryDTO participationHistory2 = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 11, 05, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors2 = phvalidator.Validate(participationHistory2);
            var history2 = participationHistoryService.CreateHistory(participationHistory2);
            var actual2 = participationHistoryService.GetHistoryById(history2.Id);

            ParticipationHistoryDTO participationHistory3 = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw2.Id,
                StartDate = new DateTimeOffset(2021, 11, 05, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors3 = phvalidator.Validate(participationHistory3);
            var history3 = participationHistoryService.CreateHistory(participationHistory3);
            var actual3 = participationHistoryService.GetHistoryById(history3.Id);

            var actual = participationHistoryService.GetAllHistories().ToList();
            Assert.AreEqual(actual.Count, 3);

            participationHistoryService.DeleteHistoryById(actual1.Id);
            participationHistoryService.DeleteHistoryById(actual2.Id);
            participationHistoryService.DeleteHistoryById(actual3.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            projectWorkService.DeleteProjectWorkById(prw2.Id);
            employeeService.DeleteEmployeeById(em.Id);
            employeeService.DeleteEmployeeById(em2.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void GetAllEmployeesHistoriesOnProjectTest()
        {
            ParticipationHistoryService participationHistoryService = new ParticipationHistoryService(uow);
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

            EmployeeDTO employee2 = new EmployeeDTO
            {
                EmployeeName = "Иван",
                EmployeeSurname = "Иванов",
                EmployeePatronymic = "Иванович",
                RoleId = 2,
                Email = "vanya@mail.ru",
            };
            var employeeOnWork2 = employeeService.CreateEmployee(employee2);
            var em2 = employeeService.GetEmployeeById(employeeOnWork2.Id);

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "проект 1",
                ProjectDescription = "проект номер один",
                ProjectStartDate = new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
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

            ProjectWorkDTO projectWork2 = new ProjectWorkDTO
            {
                EmployeeId = em2.Id,
                ProjectId = pr.Id,
                ProjectRoleId = 2
            };
            var pWork2 = projectWorkService.CreateProjectWork(projectWork2);
            var prw2 = projectWorkService.GetProjectWorkById(pWork2.Id);

            ParticipationHistoryDTO participationHistory1 = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 04, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors = phvalidator.Validate(participationHistory1);
            var history1 = participationHistoryService.CreateHistory(participationHistory1);
            var actual1 = participationHistoryService.GetHistoryById(history1.Id);

            ParticipationHistoryDTO participationHistory2 = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 11, 05, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors2 = phvalidator.Validate(participationHistory2);
            var history2 = participationHistoryService.CreateHistory(participationHistory2);
            var actual2 = participationHistoryService.GetHistoryById(history2.Id);

            ParticipationHistoryDTO participationHistory3 = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw2.Id,
                StartDate = new DateTimeOffset(2021, 11, 05, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors3 = phvalidator.Validate(participationHistory3);
            var history3 = participationHistoryService.CreateHistory(participationHistory3);
            var actual3 = participationHistoryService.GetHistoryById(history3.Id);

            var actual = participationHistoryService.GetAllEmployeesHistoriesOnProject(prw.Id).ToList();
            Assert.AreEqual(actual.Count, 2);

            participationHistoryService.DeleteHistoryById(actual1.Id);
            participationHistoryService.DeleteHistoryById(actual2.Id);
            participationHistoryService.DeleteHistoryById(actual3.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            projectWorkService.DeleteProjectWorkById(prw2.Id);
            employeeService.DeleteEmployeeById(em.Id);
            employeeService.DeleteEmployeeById(em2.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void ChangeHistoryStartDateTest()
        {
            ParticipationHistoryService participationHistoryService = new ParticipationHistoryService(uow);
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
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
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

            ParticipationHistoryDTO participationHistory1 = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors = phvalidator.Validate(participationHistory1);
            var history1 = participationHistoryService.CreateHistory(participationHistory1);
            var actual1 = participationHistoryService.GetHistoryById(history1.Id);

            participationHistoryService.ChangeHistoryStartDate(actual1.Id, new DateTimeOffset(2021, 11, 05, 10, 15, 35, new TimeSpan(3, 0, 0)));
            var actual = participationHistoryService.GetHistoryById(actual1.Id);

            ParticipationHistoryDTO expected = new ParticipationHistoryDTO
            {
                Id = actual1.Id,
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 11, 05, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.ProjectWorkId == expected.ProjectWorkId &&
    actual.StartDate == expected.StartDate && actual.EndDate == expected.EndDate);

            participationHistoryService.DeleteHistoryById(actual1.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void ChangeHistoryStartDateIfItsNotValidTest()
        {
            ParticipationHistoryService participationHistoryService = new ParticipationHistoryService(uow);
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
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
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

            ParticipationHistoryDTO participationHistory = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            var errors = phvalidator.Validate(participationHistory);
            var history = participationHistoryService.CreateHistory(participationHistory);
            var actual = participationHistoryService.GetHistoryById(history.Id);

            Assert.ThrowsException<InvalidDateException>(() => participationHistoryService.ChangeHistoryStartDate(actual.Id, new DateTimeOffset(2021, 12, 05, 10, 15, 35, new TimeSpan(3, 0, 0))));

            participationHistoryService.DeleteHistoryById(actual.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void ChangeHistoryEndDateTest()
        {
            ParticipationHistoryService participationHistoryService = new ParticipationHistoryService(uow);
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
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
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

            ParticipationHistoryDTO participationHistory1 = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };
            var errors = phvalidator.Validate(participationHistory1);
            var history1 = participationHistoryService.CreateHistory(participationHistory1);
            var actual1 = participationHistoryService.GetHistoryById(history1.Id);

            participationHistoryService.ChangeHistoryEndDate(actual1.Id, new DateTimeOffset(2021, 12, 10, 11, 15, 35, new TimeSpan(3, 0, 0)));
            var actual = participationHistoryService.GetHistoryById(actual1.Id);

            ParticipationHistoryDTO expected = new ParticipationHistoryDTO
            {
                Id = actual1.Id,
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 12, 10, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.ProjectWorkId == expected.ProjectWorkId &&
    actual.StartDate == expected.StartDate && actual.EndDate == expected.EndDate);

            participationHistoryService.DeleteHistoryById(actual1.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void ChangeHistoryEndDateIfItsNotValidTest()
        {
            ParticipationHistoryService participationHistoryService = new ParticipationHistoryService(uow);
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
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 10, 15, 35, new TimeSpan(3, 0, 0))
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

            ParticipationHistoryDTO participationHistory = new ParticipationHistoryDTO
            {
                ProjectWorkId = prw.Id,
                StartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                EndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            var errors = phvalidator.Validate(participationHistory);
            var history = participationHistoryService.CreateHistory(participationHistory);
            var actual = participationHistoryService.GetHistoryById(history.Id);

            Assert.ThrowsException<InvalidDateException>(() => participationHistoryService.ChangeHistoryEndDate(actual.Id, new DateTimeOffset(2021, 9, 05, 10, 15, 35, new TimeSpan(3, 0, 0))));

            participationHistoryService.DeleteHistoryById(actual.Id);
            projectWorkService.DeleteProjectWorkById(prw.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }
    }
}
