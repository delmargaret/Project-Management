using BLL.Interfaces;
using BLL.Services;
using Exeption;
using Newtonsoft.Json;
using ProjectManagement.Filters;
using ProjectManagement.Models;
using ProjectManagement.Util;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectManagement.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TokenController : ApiController
    {
        PasswordService passwordService = new PasswordService();

        IEmployeeService employeeService = new EmployeeService(new ContextUnitOfWork("ManagementContext"));

        ICredentialsService credentialsService = new CredentialsService(new ContextUnitOfWork("ManagementContext"));

        //public TokenController(ICredentialsService serv, IEmployeeService empserv)
        //{
        //    credentialsService = serv;
        //    employeeService = empserv;
        //}

        [JwtAuthentication]
        [HttpGet]
        public IHttpActionResult GetEmployee()
        {
            try
            {
                var email = RequestContext.Principal.Identity.Name;
                if (email != null)
                {
                    var employee = employeeService.GetEmployeeByEmail(email);
                    return Ok(JsonConvert.SerializeObject(employee));
                }
                else throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            catch (NotFoundException) { return Ok(); }
        }

        [JwtAuthentication]
        [HttpGet]
        public int GetRoleId()
        {
            try
            {
                var email = RequestContext.Principal.Identity.Name;
                if (email != null)
                {
                    var role = employeeService.GetEmployeeByEmail(email).RoleId;
                    return role;
                }
                else throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            catch (NotFoundException) { return 0; }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Authenticate([FromBody]UserModel model)
        {
            try
            {
                if (CheckUser(model.Login, model.Password))
                {
                    var token = JWT.GenerateToken(model.Login, 50);
                    return Ok(JsonConvert.SerializeObject(token));
                }
                else throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            catch (HttpResponseException)
            {
                return Ok();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Registrate([FromBody]UserModel model)
        {
            try
            {
                var employee = employeeService.GetEmployeeByEmail(model.Login);
                credentialsService.Registrate(employee.Id, model.Password);
                return Ok();
            }
            catch (HttpResponseException)
            {
                return Ok();
            }
            catch (NotFoundException)
            {
                return Ok();
            }
        }

        [JwtAuthentication]
        [HttpPost]
        public IHttpActionResult ChangePassword([FromBody]UserModel model)
        {
            try
            {
                credentialsService.ChangePassword(model.Login, model.Password);
                var token = JWT.GenerateToken(model.Login, 50);
                return Ok(JsonConvert.SerializeObject(token));
            }
            catch (HttpResponseException)
            {
                return Ok();
            }
            catch (Exception)
            {
                return Ok();
            }
        }
        private bool CheckUser(string username, string password)
        {
            return credentialsService.Autenticate(username, password);
        }
    }
}