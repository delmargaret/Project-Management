using BLL.Interfaces;
using BLL.Services;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ProjectManagement.Filters
{
    public class JwtAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        IEmployeeService employeeService = new EmployeeService(new ContextUnitOfWork("ManagementContext"));
        private string[] roleIdList;
        public JwtAuthorizationAttribute(params string[] roles)
        {
            roleIdList = roles;
        }
        public bool AllowMultiple => false;

        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            IPrincipal principal = actionContext.RequestContext.Principal;

            var role = employeeService.GetEmployeeByEmail(principal.Identity.Name).RoleId.ToString();

            if (principal == null || !roleIdList.Contains(role))
            {
                return Task.FromResult(
                      actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized));
            }
            else
            {
                return continuation();
            }
        }
    }
}