using BLL.DTO;
using BLL.Mapping;
using BLL.Services;
using DAL.Entities;
using Newtonsoft.Json;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Validation;

namespace ProjectManagement.Controllers
{
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        EmployeeValidator evalidator = new EmployeeValidator();
        static IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
        EmployeeService employeeService = new EmployeeService(uow, new Map<Employee, EmployeeDTO>());

        [Route("GetEmployees")]
        [HttpGet]
        public IEnumerable<string> GetAllEmployees()
        {
            List<EmployeeDTO> employees = employeeService.GetAllEmployees().ToList();
            string[] result = new string[employees.Count];
            int i = 0;
            foreach (var em in employees)
            {
                result[i] = JsonConvert.SerializeObject(em);
                i++;
            }
            return result;
        }

        [Route("GetEmployeeById/{id}")]
        public string GetById(int id)
        {
            EmployeeDTO employee = employeeService.GetEmployeeById(id);
            return JsonConvert.SerializeObject(employee);
        }

        [Route("GetEmployeeByEmail/{email}")]
        public string GetByEmail(string email)
        {
            EmployeeDTO employee = employeeService.GetEmployeeByEmail(email);
            return JsonConvert.SerializeObject(employee);
        }

        [Route("GetEmployeeBySurname/{surname}")]
        public IEnumerable<string> GetBySurname(string surname)
        {
            List<EmployeeDTO> employees = employeeService.GetEmployeesBySurname(surname).ToList();
            string[] result = new string[employees.Count];
            int i = 0;
            foreach (var em in employees)
            {
                result[i] = JsonConvert.SerializeObject(em);
                i++;
            }
            return result;
        }

        [Route("GetEmployeeBySurname/{surname}")]
        public IEnumerable<string> GetByRoleId(int roleId)
        {
            List<EmployeeDTO> employees = employeeService.GetEmployeesByRoleId(roleId).ToList();
            string[] result = new string[employees.Count];
            int i = 0;
            foreach (var em in employees)
            {
                result[i] = JsonConvert.SerializeObject(em);
                i++;
            }
            return result;
        }

        [Route("CreateEmployee")]
        [HttpPost]
        public IHttpActionResult CreateEmployee([FromBody]EmployeeDTO value)
        {
            var result = value;
            employeeService.CreateEmployee(value);
            return Ok(value);
        }

        [Route("DeleteEmployeeById/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteById(int id)
        {
            employeeService.DeleteEmployeeById(id);
            return Ok();
        }

        [Route("DeleteEmployeeByEmail/{email}")]
        [HttpDelete]
        public IHttpActionResult DeleteByEmail(string email)
        {
            employeeService.DeleteEmployeeByEmail(email);
            return Ok();
        }

        [Route("AddGitLink/{id}")]
        [HttpPut]
        public IHttpActionResult AddGitLink(int id, [FromBody]string git)
        {
            employeeService.AddGitLink(id, git);
            return Ok();
        }

        [Route("DeleteGitLink/{id}")]
        [HttpPut]
        public IHttpActionResult DeleteGitLink(int id)
        {
            employeeService.DeleteGitLinkByEmployeeId(id);
            return Ok();
        }

        [Route("AddPhoneNumber/{id}")]
        [HttpPut]
        public IHttpActionResult AddPhoneNumber(int id, [FromBody]string number)
        {
            employeeService.AddPhoneNumber(id, number);
            return Ok();
        }

        [Route("DeletePhoneNumber/{id}")]
        [HttpPut]
        public IHttpActionResult DeletePhoneNumber(int id)
        {
            employeeService.DeletePhoneNumberByEmployeeId(id);
            return Ok();
        }

        [Route("ChangeName/{id}")]
        [HttpPut]
        public IHttpActionResult ChangeName(int id, [FromBody]string name)
        {
            employeeService.ChangeName(id, name);
            return Ok();
        }

        [Route("ChangeSurname/{id}")]
        [HttpPut]
        public IHttpActionResult ChangeSurname(int id, [FromBody]string surname)
        {
            employeeService.ChangeSurname(id, surname);
            return Ok();
        }

        [Route("ChangePatronymic/{id}")]
        [HttpPut]
        public IHttpActionResult ChangePatronymic(int id, [FromBody]string patronymic)
        {
            employeeService.ChangePatronymic(id, patronymic);
            return Ok();
        }

        [Route("ChangeEmail/{id}")]
        [HttpPut]
        public IHttpActionResult ChangeEmail(int id, [FromBody]string email)
        {
            employeeService.ChangeEmail(id, email);
            return Ok();
        }

        [Route("ChangeGitLink/{id}")]
        [HttpPut]
        public IHttpActionResult ChangeGitLink(int id, [FromBody]string git)
        {
            employeeService.ChangeGitLink(id, git);
            return Ok();
        }

        [Route("ChangePhoneNumber/{id}")]
        [HttpPut]
        public IHttpActionResult ChangePhoneNumber(int id, [FromBody]string number)
        {
            employeeService.ChangePhoneNumber(id, number);
            return Ok();
        }

        [Route("ChangeRole/{id}")]
        [HttpPut]
        public IHttpActionResult ChangeRole(int id, [FromBody]int roleId)
        {
            employeeService.ChangeRole(id, roleId);
            return Ok();
        }

        [Route("ChangeWorkLoadType/{id}")]
        [HttpPut]
        public IHttpActionResult ChangeWorkLoad(int id, [FromBody]int loadType)
        {
            employeeService.ChangeWorkLoad(id, loadType);
            return Ok();
        }
    }
}