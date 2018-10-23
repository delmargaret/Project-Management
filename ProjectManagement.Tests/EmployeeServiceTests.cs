using System;
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
    public class EmployeeServiceTests
    {
        readonly IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");

        [TestMethod]
        public void CreateEmployeeTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());

            employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            });
            EmployeeDTO actual = employeeService.GetEmployeeByEmail("katya@mail.ru");
            EmployeeDTO expected = new EmployeeDTO
            {
                Id = actual.Id,
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
                PercentOrScheduleId = 3,
                GitLink = null,
                PhoneNumber = null
            };
            Assert.IsTrue(actual.Id == expected.Id && actual.EmployeeName == expected.EmployeeName && 
                actual.EmployeeSurname == expected.EmployeeSurname && actual.EmployeePatronymic == expected.EmployeePatronymic
                && actual.RoleId == expected.RoleId && actual.Email == expected.Email && 
                actual.PercentOrScheduleId == expected.PercentOrScheduleId && (actual.PhoneNumber == expected.PhoneNumber || (string.IsNullOrEmpty(actual.PhoneNumber) && string.IsNullOrEmpty(expected.PhoneNumber))) 
                && (actual.GitLink == expected.GitLink || (string.IsNullOrEmpty(actual.GitLink)&& string.IsNullOrEmpty(expected.GitLink))));
        }
    }
}
