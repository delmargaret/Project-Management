using System;
using System.Linq;
using BLL.DTO;
using BLL.Mapping;
using BLL.Services;
using DAL.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Interfaces;
using Repository.Repositories;

namespace ProjectManagement.Tests
{
    [TestClass]
    public class ProjectRoleServiceTests
    {
        readonly IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");

        [TestMethod]
        public void CreateProjectRoleTest()
        {
            ProjectRoleService projectRoleService = new ProjectRoleService(uow, new Map<ProjectRole, ProjectRoleDTO>());

            var projectRole = projectRoleService.CreateProjectRole("abc");
            ProjectRoleDTO actual = projectRoleService.GetProjectRoleById(projectRole.Id);
            ProjectRoleDTO expected = new ProjectRoleDTO
            {
                Id = actual.Id,
                ProjectRoleName = "abc"
            };
            Assert.IsTrue(actual.Id == expected.Id && actual.ProjectRoleName == expected.ProjectRoleName);

            projectRoleService.DeleteProjectRoleById(actual.Id);
        }

        [TestMethod]
        public void GetProjectRolesTest()
        {
            ProjectRoleService projectRoleService = new ProjectRoleService(uow, new Map<ProjectRole, ProjectRoleDTO>());

            var projects = projectRoleService.GetProjectRoles().ToList();
            Assert.AreEqual(projects.Count, 7);
        }

        [TestMethod]
        public void ChangeProjectRoleNameTest()
        {
            ProjectRoleService projectRoleService = new ProjectRoleService(uow, new Map<ProjectRole, ProjectRoleDTO>());

            var projectRole = projectRoleService.CreateProjectRole("abc");
            ProjectRoleDTO actual = projectRoleService.GetProjectRoleById(projectRole.Id);
            projectRoleService.ChangeProjectRoleName(actual.Id, "cba");
            ProjectRoleDTO expected = projectRoleService.GetProjectRoleById(projectRole.Id);
            Assert.IsTrue(expected.ProjectRoleName == "cba");

            projectRoleService.DeleteProjectRoleById(actual.Id);
        }
    }
}
