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
    public class ProjectServiceTests
    {
        readonly IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
        ProjectValidator pvalidator = new ProjectValidator();

        [TestMethod]
        public void CreateProjectTest()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            var errors = pvalidator.Validate(project);
            var pr = projectService.CreateProject(project);
            ProjectDTO actual = projectService.GetProjectById(pr.Id);

            ProjectDTO expected = new ProjectDTO
            {
                Id = actual.Id,
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectStatusId = 1
            };

            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.ProjectName == expected.ProjectName &&
                actual.ProjectDescription == expected.ProjectDescription && actual.ProjectStartDate == expected.ProjectStartDate
                && actual.ProjectEndDate == expected.ProjectEndDate && actual.ProjectStatusId == expected.ProjectStatusId);
            projectService.DeleteProjectById(actual.Id);
        }

        [TestMethod]
        public void CreateProjectIfItsNotValidTest()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2020, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            ProjectDTO result = new ProjectDTO();
            var errors = pvalidator.Validate(project);
            if (errors.Count == 0)
            {
                result = projectService.CreateProject(project);
            }
            Assert.ThrowsException<NotFoundException>(() => projectService.GetProjectById(result.Id));
        }

        [TestMethod]
        public void GetAllProjectsByStatusIdTest()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());

            ProjectDTO project1 = new ProjectDTO
            {
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0)),
            };

            var errors1 = pvalidator.Validate(project1);
            var pr1 = projectService.CreateProject(project1);
            ProjectDTO actual1 = projectService.GetProjectById(pr1.Id);

            ProjectDTO project2 = new ProjectDTO
            {
                ProjectName = "project2",
                ProjectDescription = "this is the second project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0)),
            };

            var errors2 = pvalidator.Validate(project2);
            var pr2 = projectService.CreateProject(project2);
            ProjectDTO actual2 = projectService.GetProjectById(pr2.Id);

            ProjectDTO project3 = new ProjectDTO
            {
                ProjectName = "project3",
                ProjectDescription = "this is the third project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0)),
            };

            var errors3 = pvalidator.Validate(project3);
            var pr3 = projectService.CreateProject(project3);
            ProjectDTO actual3 = projectService.GetProjectById(pr3.Id);
            projectService.CloseProject(actual3.Id);

            var result = projectService.GetAllProjectsByStatusId(1).ToList();
            Assert.AreEqual(result.Count, 2);

            projectService.DeleteProjectById(actual1.Id);
            projectService.DeleteProjectById(actual2.Id);
            projectService.DeleteProjectById(actual3.Id);
        }

        [TestMethod]
        public void GetAllProjectsTest()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());

            ProjectDTO project1 = new ProjectDTO
            {
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0)),
            };

            var errors1 = pvalidator.Validate(project1);
            var pr1 = projectService.CreateProject(project1);
            ProjectDTO actual1 = projectService.GetProjectById(pr1.Id);

            ProjectDTO project2 = new ProjectDTO
            {
                ProjectName = "project2",
                ProjectDescription = "this is the second project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0)),
            };

            var errors2 = pvalidator.Validate(project2);
            var pr2 = projectService.CreateProject(project2);
            ProjectDTO actual2 = projectService.GetProjectById(pr2.Id);

            ProjectDTO project3 = new ProjectDTO
            {
                ProjectName = "project3",
                ProjectDescription = "this is the third project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0)),
            };

            var errors3 = pvalidator.Validate(project3);
            var pr3 = projectService.CreateProject(project3);
            ProjectDTO actual3 = projectService.GetProjectById(pr3.Id);
            projectService.CloseProject(actual3.Id);

            var result = projectService.GetAllProjects().ToList();
            Assert.AreEqual(result.Count, 3);

            projectService.DeleteProjectById(actual1.Id);
            projectService.DeleteProjectById(actual2.Id);
            projectService.DeleteProjectById(actual3.Id);
        }

        [TestMethod]
        public void GetProjectsEndingInNDaysTest()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());

            ProjectDTO project1 = new ProjectDTO
            {
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2018, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2018, 12, 15, 11, 15, 35, new TimeSpan(3, 0, 0)),
            };

            var errors1 = pvalidator.Validate(project1);
            var pr1 = projectService.CreateProject(project1);
            ProjectDTO actual1 = projectService.GetProjectById(pr1.Id);

            ProjectDTO project2 = new ProjectDTO
            {
                ProjectName = "project2",
                ProjectDescription = "this is the second project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0)),
            };

            var errors2 = pvalidator.Validate(project2);
            var pr2 = projectService.CreateProject(project2);
            ProjectDTO actual2 = projectService.GetProjectById(pr2.Id);

            ProjectDTO project3 = new ProjectDTO
            {
                ProjectName = "project3",
                ProjectDescription = "this is the third project",
                ProjectStartDate = new DateTimeOffset(2018, 9, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2018, 11, 29, 11, 15, 35, new TimeSpan(3, 0, 0)),
            };

            var errors3 = pvalidator.Validate(project3);
            var pr3 = projectService.CreateProject(project3);
            ProjectDTO actual3 = projectService.GetProjectById(pr3.Id);

            var result = projectService.GetProjectsEndingInNDays(123).ToList();
            Assert.AreEqual(result.Count, 2);

            projectService.DeleteProjectById(actual1.Id);
            projectService.DeleteProjectById(actual2.Id);
            projectService.DeleteProjectById(actual3.Id);
        }

        [TestMethod]
        public void ChangeProjectDescriptionTest()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            var errors = pvalidator.Validate(project);
            var pr = projectService.CreateProject(project);
            ProjectDTO actual = projectService.GetProjectById(pr.Id);

            projectService.ChangeProjectDescription(actual.Id, "abcgjksnsnsjsjxmxkks sakak");
            ProjectDTO actual2 = projectService.GetProjectById(actual.Id);
            Assert.AreEqual(actual2.ProjectDescription, "abcgjksnsnsjsjxmxkks sakak");
            projectService.DeleteProjectById(actual.Id);
        }

        [TestMethod]
        public void ChangeProjectNameTest()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            var errors = pvalidator.Validate(project);
            var pr = projectService.CreateProject(project);
            ProjectDTO actual = projectService.GetProjectById(pr.Id);

            projectService.ChangeProjectName(actual.Id, "pr1");
            ProjectDTO actual2 = projectService.GetProjectById(actual.Id);
            Assert.AreEqual(actual2.ProjectName, "pr1");
            projectService.DeleteProjectById(actual.Id);
        }

        [TestMethod]
        public void ChangeProjectStartDateTest()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            var errors = pvalidator.Validate(project);
            var pr = projectService.CreateProject(project);
            ProjectDTO actual = projectService.GetProjectById(pr.Id);

            projectService.ChangeProjectStartDate(actual.Id, new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)));
            ProjectDTO actual2 = projectService.GetProjectById(actual.Id);
            Assert.AreEqual(actual2.ProjectStartDate, new DateTimeOffset(2021, 10, 6, 10, 15, 35, new TimeSpan(3, 0, 0)));
            projectService.DeleteProjectById(actual.Id);
        }

        [TestMethod]
        public void ChangeProjectEndDateTest()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            var errors = pvalidator.Validate(project);
            var pr = projectService.CreateProject(project);
            ProjectDTO actual = projectService.GetProjectById(pr.Id);

            projectService.ChangeProjectEndDate(actual.Id, new DateTimeOffset(2021, 12, 21, 10, 15, 35, new TimeSpan(3, 0, 0)));
            ProjectDTO actual2 = projectService.GetProjectById(actual.Id);
            Assert.AreEqual(actual2.ProjectEndDate, new DateTimeOffset(2021, 12, 21, 10, 15, 35, new TimeSpan(3, 0, 0)));
            projectService.DeleteProjectById(actual.Id);
        }

        [TestMethod]
        public void ChangeProjectStatusTest()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());

            ProjectDTO project = new ProjectDTO
            {
                ProjectName = "project1",
                ProjectDescription = "this is the first project",
                ProjectStartDate = new DateTimeOffset(2021, 10, 16, 10, 15, 35, new TimeSpan(3, 0, 0)),
                ProjectEndDate = new DateTimeOffset(2021, 11, 21, 11, 15, 35, new TimeSpan(3, 0, 0))
            };

            var errors = pvalidator.Validate(project);
            var pr = projectService.CreateProject(project);
            ProjectDTO actual = projectService.GetProjectById(pr.Id);

            projectService.ChangeProjectStatus(actual.Id, 2);
            ProjectDTO actual2 = projectService.GetProjectById(actual.Id);
            Assert.AreEqual(actual2.ProjectStatusId, 2);
            projectService.DeleteProjectById(actual.Id);
        }

        [TestMethod]
        public void GetProjectByIdIfItsNotFound()
        {
            ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());
            Assert.ThrowsException<NotFoundException>(() => projectService.GetProjectById(123548));
        }
    }
}
