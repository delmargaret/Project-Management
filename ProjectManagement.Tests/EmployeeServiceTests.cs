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
    public class EmployeeServiceTests
    {
        EmployeeValidator evalidator = new EmployeeValidator();
        readonly IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");

        [TestMethod]
        public void CreateEmployeeTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());

            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var errors = evalidator.Validate(employee);
            employeeService.CreateEmployee(employee);
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
            Assert.IsTrue(errors.Count == 0 && actual.Id == expected.Id && actual.EmployeeName == expected.EmployeeName && 
                actual.EmployeeSurname == expected.EmployeeSurname && actual.EmployeePatronymic == expected.EmployeePatronymic
                && actual.RoleId == expected.RoleId && actual.Email == expected.Email && 
                actual.PercentOrScheduleId == expected.PercentOrScheduleId && (actual.PhoneNumber == expected.PhoneNumber || (string.IsNullOrEmpty(actual.PhoneNumber) && string.IsNullOrEmpty(expected.PhoneNumber))) 
                && (actual.GitLink == expected.GitLink || (string.IsNullOrEmpty(actual.GitLink)&& string.IsNullOrEmpty(expected.GitLink))));
            employeeService.DeleteEmployeeById(actual.Id);
        }

        [TestMethod]
        public void CreateEmployeeIfItIsNonValidTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());

            EmployeeDTO actual = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya",
            };
            var errors = evalidator.Validate(actual);
            if (errors.Count == 0)
            {
                employeeService.CreateEmployee(actual);
            }
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeeByEmail("katya"));
        }

        [TestMethod]
        public void DeleteEmployeeByIdTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());

            employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya@mail.ru",
            });
            EmployeeDTO actual = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.DeleteEmployeeById(actual.Id);
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeeByEmail("katya@mail.ru"));
        }

        [TestMethod]
        public void DeleteEmployeeByIdIfItsNullTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            Assert.ThrowsException<NotFoundException>(() => employeeService.DeleteEmployeeById(152364));
        }

        [TestMethod]
        public void DeleteEmployeeBySurnameTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());

            employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya@mail.ru",
            });
            employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Александр",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Георгиевич",
                RoleId = 3,
                Email = "sasha@mail.ru",
            });
            EmployeeDTO saved = employeeService.GetEmployeeByEmail("sasha@mail.ru");
            employeeService.DeleteEmployeeBySurname("Антонович");
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeeByEmail("katya@mail.ru"));
            employeeService.DeleteEmployeeById(saved.Id);
        }

        [TestMethod]
        public void DeleteEmployeeBySurnameIfItsNullTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            Assert.ThrowsException<NotFoundException>(() => employeeService.DeleteEmployeeBySurname("Петров"));
        }

        [TestMethod]
        public void DeleteEmployeeByEmailTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());

            employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya@mail.ru",
            });
            employeeService.DeleteEmployeeByEmail("katya@mail.ru");
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeeByEmail("katya@mail.ru"));
        }

        [TestMethod]
        public void DeleteEmployeeByEmailIfItsNullTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            Assert.ThrowsException<NotFoundException>(() => employeeService.DeleteEmployeeByEmail("hjskfsklsxc@gmail.ru"));
        }

        [TestMethod]
        public void GetEmployeeByIdTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO expected = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya@mail.ru",
            };
            employeeService.CreateEmployee(expected);
            EmployeeDTO actual = employeeService.GetEmployeeByEmail("katya@mail.ru");
            Assert.IsNotNull(employeeService.GetEmployeeById(actual.Id));
            employeeService.DeleteEmployeeById(actual.Id);
        }

        [TestMethod]
        public void GetEmployeeByIdIfItsNullTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeeById(3692));
        }

        [TestMethod]
        public void GetAllEmployeesTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());

            employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya@mail.ru",
            });
            employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Александр",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Георгиевич",
                RoleId = 3,
                Email = "sasha@mail.ru",
            });
            var list = employeeService.GetAllEmployees().ToList();
            Assert.AreEqual(list.Count(), 2);
            employeeService.DeleteEmployeeByEmail("katya@mail.ru");
            employeeService.DeleteEmployeeByEmail("sasha@mail.ru");
        }

        [TestMethod]
        public void GetEmployeesByRoleIdTest()
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
            employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Александр",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Георгиевич",
                RoleId = 3,
                Email = "sasha@mail.ru",
            });
            var list = employeeService.GetEmployeesByRoleId(3).ToList();
            Assert.AreEqual(list.Count(), 2);
            employeeService.DeleteEmployeeByEmail("katya@mail.ru");
            employeeService.DeleteEmployeeByEmail("sasha@mail.ru");
        }

        [TestMethod]
        public void GetEmployeesByRoleIdIfRoleNotFoundTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeesByRoleId(9));
        }

        [TestMethod]
        public void GetEmployeesByRoleIdIfTheyNotFoundTest()
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
            employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Александр",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Георгиевич",
                RoleId = 3,
                Email = "sasha@mail.ru",
            });
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeesByRoleId(1));
            employeeService.DeleteEmployeeByEmail("katya@mail.ru");
            employeeService.DeleteEmployeeByEmail("sasha@mail.ru");
        }

        [TestMethod]
        public void AddGitLinkTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.AddGitLink(emp.Id, "abc");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.GitLink == "abc");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void DeleteGitLinkByEmployeeIdTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.AddGitLink(emp.Id, "abc");
            employeeService.DeleteGitLinkByEmployeeId(emp.Id);
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.GitLink == null);
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void AddPhoneNumberTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.AddPhoneNumber(emp.Id, "+324589617426");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.PhoneNumber == "+324589617426");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void DeletePhoneNumberByEmployeeIdTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.AddPhoneNumber(emp.Id, "+324589617426");
            employeeService.DeletePhoneNumberByEmployeeId(emp.Id);
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.PhoneNumber == null);
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeNameTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.ChangeName(emp.Id, "Светлана");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.EmployeeName == "Светлана");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeSurnameTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.ChangeSurname(emp.Id, "Кот");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.EmployeeSurname == "Кот");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangePatronymicTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.ChangePatronymic(emp.Id, "Олеговна");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.EmployeePatronymic == "Олеговна");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeEmailTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.ChangeEmail(emp.Id, "katrin@mail.ru");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.Email == "katrin@mail.ru");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeGitLinkTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
                GitLink = "link"
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.ChangeGitLink(emp.Id, "abc");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.GitLink == "abc");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangePhoneNumberTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
                PhoneNumber = "+258469812534"
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.ChangePhoneNumber(emp.Id, "80469812534");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.PhoneNumber == "80469812534");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeRoleTest()
        {
            EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeByEmail("katya@mail.ru");
            employeeService.ChangeRole(emp.Id, 1);
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.RoleId == 1);
            employeeService.DeleteEmployeeById(emp.Id);
        }
    }
}
