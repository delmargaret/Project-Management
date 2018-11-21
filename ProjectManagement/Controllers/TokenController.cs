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
        CredentialsService credentialsService = new CredentialsService(new ContextUnitOfWork("ManagementContext"));
        EmployeeService employeeService = new EmployeeService(new ContextUnitOfWork("ManagementContext"));
        PasswordService passwordService = new PasswordService();

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
            if (CheckUser(model.Login, model.Password))
            {
                var token = JWT.GenerateToken(model.Login, 20);
                return Ok(JsonConvert.SerializeObject(token));
            }
            else throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Registrate([FromBody]UserModel model)
        {
            var employeeId = employeeService.GetEmployeeByEmail(model.Login).Id;
            credentialsService.Registrate(employeeId, model.Password);
            var token = JWT.GenerateToken(model.Login, 20);
            return Ok(JsonConvert.SerializeObject(token));
        }

        private bool CheckUser(string username, string password)
        {
            return credentialsService.Autenticate(username, password);
        }
    }
}