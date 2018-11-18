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
            EmployeeService employeeService = new EmployeeService(uow);

            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var errors = evalidator.Validate(employee);
            var emp = employeeService.CreateEmployee(employee);
            EmployeeDTO actual = employeeService.GetEmployeeById(emp.Id);
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
        public void CreateEmployeeIfItIsNotValidTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);

            EmployeeDTO actual = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya",
            };
            EmployeeDTO result = new EmployeeDTO();
            var errors = evalidator.Validate(actual);
            if (errors.Count == 0)
            {
                result = employeeService.CreateEmployee(actual);
            }
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeeById(result.Id));
        }

        [TestMethod]
        public void DeleteEmployeeByIdTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);

            var em = employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya@mail.ru",
            });
            EmployeeDTO actual = employeeService.GetEmployeeById(em.Id);
            employeeService.DeleteEmployeeById(actual.Id);
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeeById(em.Id));
        }

        [TestMethod]
        public void DeleteEmployeeByIdIfItsNullTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            Assert.ThrowsException<NotFoundException>(() => employeeService.DeleteEmployeeById(152364));
        }

        [TestMethod]
        public void DeleteEmployeeBySurnameTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);

            var katya = employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya@mail.ru",
            });
            var sasha = employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Александр",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Георгиевич",
                RoleId = 3,
                Email = "sasha@mail.ru",
            });
            EmployeeDTO saved = employeeService.GetEmployeeById(sasha.Id);
            employeeService.DeleteEmployeeBySurname("Антонович");
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeeById(katya.Id));
            employeeService.DeleteEmployeeById(saved.Id);
        }

        [TestMethod]
        public void DeleteEmployeeBySurnameIfItsNullTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            Assert.ThrowsException<NotFoundException>(() => employeeService.DeleteEmployeeBySurname("Петров"));
        }

        [TestMethod]
        public void DeleteEmployeeByEmailTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);

            var katya = employeeService.CreateEmployee(new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya@mail.ru",
            });
            employeeService.DeleteEmployeeByEmail("katya@mail.ru");
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeeById(katya.Id));
        }

        [TestMethod]
        public void DeleteEmployeeByEmailIfItsNullTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            Assert.ThrowsException<NotFoundException>(() => employeeService.DeleteEmployeeByEmail("hjskfsklsxc@gmail.ru"));
        }

        [TestMethod]
        public void GetEmployeeByIdTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO expected = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 2,
                Email = "katya@mail.ru",
            };
            var em = employeeService.CreateEmployee(expected);
            Assert.IsNotNull(employeeService.GetEmployeeById(em.Id));
            employeeService.DeleteEmployeeById(em.Id);
        }

        [TestMethod]
        public void GetEmployeeByIdIfItsNullTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeeById(3692));
        }

        [TestMethod]
        public void GetAllEmployeesTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);

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
            EmployeeService employeeService = new EmployeeService(uow);

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
            EmployeeService employeeService = new EmployeeService(uow);
            Assert.ThrowsException<NotFoundException>(() => employeeService.GetEmployeesByRoleId(9));
        }

        [TestMethod]
        public void GetEmployeesByRoleIdIfEmployeesNotFoundTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
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
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.AddGitLink(emp.Id, "abc");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.GitLink == "abc");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void DeleteGitLinkByEmployeeIdTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.AddGitLink(emp.Id, "abc");
            employeeService.DeleteGitLinkByEmployeeId(emp.Id);
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.GitLink == null);
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void AddPhoneNumberTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.AddPhoneNumber(emp.Id, "+324589617426");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.PhoneNumber == "+324589617426");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void DeletePhoneNumberByEmployeeIdTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.AddPhoneNumber(emp.Id, "+324589617426");
            employeeService.DeletePhoneNumberByEmployeeId(emp.Id);
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.PhoneNumber == null);
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeNameTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.ChangeName(emp.Id, "Светлана");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.EmployeeName == "Светлана");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeSurnameTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.ChangeSurname(emp.Id, "Кот");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.EmployeeSurname == "Кот");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangePatronymicTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.ChangePatronymic(emp.Id, "Олеговна");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.EmployeePatronymic == "Олеговна");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeEmailTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.ChangeEmail(emp.Id, "katrin@mail.ru");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.Email == "katrin@mail.ru");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeGitLinkTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
                GitLink = "link"
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.ChangeGitLink(emp.Id, "abc");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.GitLink == "abc");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangePhoneNumberTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
                PhoneNumber = "+258469812534"
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.ChangePhoneNumber(emp.Id, "80469812534");
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.PhoneNumber == "80469812534");
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeRoleTest()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            EmployeeDTO employee = new EmployeeDTO
            {
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
            };
            var em = employeeService.CreateEmployee(employee);
            EmployeeDTO emp = employeeService.GetEmployeeById(em.Id);
            employeeService.ChangeRole(emp.Id, 1);
            EmployeeDTO expected = employeeService.GetEmployeeById(emp.Id);
            Assert.IsTrue(expected.RoleId == 1);
            employeeService.DeleteEmployeeById(emp.Id);
        }

        [TestMethod]
        public void ChangeWorkLoadIfItsPercent()
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
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var pw = projectWorkService.GetProjectWorkById(pWork.Id);
            projectWorkService.AddWorkLoad(pw.Id, 30);
            var actual = projectWorkService.GetProjectWorkById(pw.Id);

            Assert.ThrowsException<PercentOrScheduleException>(() => employeeService.ChangeWorkLoad(em.Id, 2));

            projectWorkService.DeleteProjectWorkById(actual.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void ChangeWorkLoadIfItsSchedule()
        {
            EmployeeService employeeService = new EmployeeService(uow);
            ProjectService projectService = new ProjectService(uow);
            ScheduleServise scheduleServise = new ScheduleServise(uow);
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
            var pw = projectWorkService.GetProjectWorkById(pWork.Id);
            var actual = projectWorkService.GetProjectWorkById(pw.Id);
            scheduleServise.CreateSchedule(new ScheduleDTO { ProjectWorkId = actual.Id, ScheduleDayId = 2 });
            scheduleServise.CreateSchedule(new ScheduleDTO { ProjectWorkId = actual.Id, ScheduleDayId = 4 });

            Assert.ThrowsException<PercentOrScheduleException>(() => employeeService.ChangeWorkLoad(em.Id, 1));

            projectWorkService.DeleteProjectWorkById(actual.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }

        [TestMethod]
        public void ChangeWorkLoad()
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
            var pWork = projectWorkService.CreateProjectWork(projectWork);
            var pw = projectWorkService.GetProjectWorkById(pWork.Id);
            projectWorkService.AddWorkLoad(pw.Id, 30);
            var pw1 = projectWorkService.GetProjectWorkById(pw.Id);
            projectWorkService.DeleteWorkLoad(pw1.Id);
            var pw2 = projectWorkService.GetProjectWorkById(pw1.Id);

            employeeService.ChangeWorkLoad(em.Id, 2);
            var actual = employeeService.GetEmployeeById(em.Id);
            EmployeeDTO expected = new EmployeeDTO
            {
                Id = em.Id,
                EmployeeName = "Екатерина",
                EmployeeSurname = "Антонович",
                EmployeePatronymic = "Алексеевна",
                RoleId = 3,
                Email = "katya@mail.ru",
                PercentOrScheduleId = 2
            };

            Assert.IsTrue(actual.Id == expected.Id && actual.EmployeeName == expected.EmployeeName &&
                actual.EmployeeSurname == expected.EmployeeSurname && actual.EmployeePatronymic == expected.EmployeePatronymic &&
                actual.RoleId == expected.RoleId && actual.Email == expected.Email && actual.PercentOrScheduleId == expected.PercentOrScheduleId);

            projectWorkService.DeleteProjectWorkById(pw2.Id);
            employeeService.DeleteEmployeeById(em.Id);
            projectService.DeleteProjectById(pr.Id);
        }
    }
}
