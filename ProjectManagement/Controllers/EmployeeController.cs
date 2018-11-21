using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using Exeption;
using Newtonsoft.Json;
using ProjectManagement.Filters;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Validation;

namespace ProjectManagement.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [JwtAuthentication]
    public class EmployeesController : ApiController
    {
        EmailService emailService = new EmailService();
        EmployeeValidator evalidator = new EmployeeValidator();
        IEmployeeService employeeService = new EmployeeService(new ContextUnitOfWork("ManagementContext"));

        //IEmployeeService employeeService { get; set; }
        //public EmployeesController(IEmployeeService serv)
        //{
        //    employeeService = serv;
        //}
        
        [HttpGet]
        public IHttpActionResult GetAllEmployees()
        {
            try
            {
                List<EmployeeDTO> employees = employeeService.GetAllEmployees().ToList();
                string[] result = new string[employees.Count];
                int i = 0;
                foreach (var em in employees)
                {
                    result[i] = JsonConvert.SerializeObject(em);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
        }

        [HttpGet]
        public IHttpActionResult GetEmployeesNotOnProjects(int projectId)
        {
            try
            {
                var employees = employeeService.FindEmployeesNotOnProject(projectId).ToList();
                string[] result = new string[employees.Count];
                int i = 0;
                foreach (var employee in employees)
                {
                    result[i] = JsonConvert.SerializeObject(employee);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
        }

        [ActionName("sortBySurnameAsc")]
        [HttpGet]
        public IHttpActionResult GetBySurnameAsc()
        {
            try
            {
                List<EmployeeDTO> employees = employeeService.SortEmployeesBySurnameAsc().ToList();
                string[] result = new string[employees.Count];
                int i = 0;
                foreach (var em in employees)
                {
                    result[i] = JsonConvert.SerializeObject(em);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
        }

        [ActionName("sortBySurnameDesc")]
        [HttpGet]
        public IHttpActionResult GetBySurnameDesc()
        {
            try
            {
                List<EmployeeDTO> employees = employeeService.SortEmployeesBySurnameDesc().ToList();
                string[] result = new string[employees.Count];
                int i = 0;
                foreach (var em in employees)
                {
                    result[i] = JsonConvert.SerializeObject(em);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
        }

        [ActionName("sortByRoleAsc")]
        [HttpGet]
        public IHttpActionResult GetByRoleAsc()
        {
            try
            {
                List<EmployeeDTO> employees = employeeService.SortEmployeesByRoleAsc().ToList();
                string[] result = new string[employees.Count];
                int i = 0;
                foreach (var em in employees)
                {
                    result[i] = JsonConvert.SerializeObject(em);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
        }

        [ActionName("sortByRoleDesc")]
        [HttpGet]
        public IHttpActionResult GetByRoleDesc()
        {
            try
            {
                List<EmployeeDTO> employees = employeeService.SortEmployeesByRoleDesc().ToList();
                string[] result = new string[employees.Count];
                int i = 0;
                foreach (var em in employees)
                {
                    result[i] = JsonConvert.SerializeObject(em);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                EmployeeDTO employee = employeeService.GetEmployeeById(id);
                var result = JsonConvert.SerializeObject(employee);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return Ok();
            }

        }

        [HttpGet]
        public IHttpActionResult GetByEmail(string email)
        {
            try
            {
                EmployeeDTO employee = employeeService.GetEmployeeByEmail(email);
                var result = JsonConvert.SerializeObject(employee);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpGet]
        public IHttpActionResult GetBySurname(string surname)
        {
            try
            {
                List<EmployeeDTO> employees = employeeService.GetEmployeesBySurname(surname).ToList();
                string[] result = new string[employees.Count];
                int i = 0;
                foreach (var em in employees)
                {
                    result[i] = JsonConvert.SerializeObject(em);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудники не найдены");
            }
        }

        [HttpGet]
        public IHttpActionResult GetByRoleId(int roleId)
        {
            try
            {
                List<EmployeeDTO> employees = employeeService.GetEmployeesByRoleId(roleId).ToList();
                string[] result = new string[employees.Count];
                int i = 0;
                foreach (var em in employees)
                {
                    result[i] = JsonConvert.SerializeObject(em);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудники не найдены");
            }
        }

        
        [HttpPost]
        public IHttpActionResult CreateEmployee([FromBody]EmployeeDTO employee)
        {
            try
            {
                var result = employee;
                var validationResult = evalidator.Validate(employee);
                if (validationResult.Count != 0)
                {
                    return BadRequest("Объект не валиден");
                }
                employeeService.CreateEmployee(employee);
                emailService.SendMail(employee.Email);
                return Ok(employee);
            }
            catch (ObjectAlreadyExistsException)
            {
                return BadRequest("Пользователь с таким e-mail уже создан");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteById(int id)
        {
            try
            {
                employeeService.DeleteEmployeeById(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteByEmail(string email)
        {
            try
            {
                employeeService.DeleteEmployeeByEmail(email);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult AddGitLink(int id, [FromBody]string git)
        {
            try
            {
                employeeService.AddGitLink(id, git);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult DeleteGitLink(int id)
        {
            try
            {
                employeeService.DeleteGitLinkByEmployeeId(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult AddPhoneNumber(int id, [FromBody]string number)
        {
            try
            {
                employeeService.AddPhoneNumber(id, number);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult DeletePhoneNumber(int id)
        {
            try
            {
                employeeService.DeletePhoneNumberByEmployeeId(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeName(int id, [FromBody]string name)
        {
            try
            {
                employeeService.ChangeName(id, name);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeSurname(int id, [FromBody]string surname)
        {
            try
            {
                employeeService.ChangeSurname(id, surname);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangePatronymic(int id, [FromBody]string patronymic)
        {
            try
            {
                employeeService.ChangePatronymic(id, patronymic);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeEmail(int id, [FromBody]string email)
        {
            try
            {
                employeeService.ChangeEmail(id, email);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeGitLink(int id, [FromBody]string git)
        {
            try
            {
                employeeService.ChangeGitLink(id, git);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangePhoneNumber(int id, [FromBody]string number)
        {
            try
            {
                employeeService.ChangePhoneNumber(id, number);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Сотрудник не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeRole(int id, [FromBody]int roleId)
        {
            try
            {
                employeeService.ChangeRole(id, roleId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeWorkLoad(int id, [FromBody]int loadType)
        {
            try
            {
                employeeService.ChangeWorkLoad(id, loadType);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
        }
    }
}