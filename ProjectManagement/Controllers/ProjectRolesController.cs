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

namespace ProjectManagement.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [JwtAuthentication]
    public class ProjectRolesController : ApiController
    {

        IProjectRoleService projectRoleService;
        public ProjectRolesController(IProjectRoleService serv)
        {
            projectRoleService = serv;
        }
        [HttpGet]
        public IHttpActionResult GetProjectRoles()
        {
            try
            {
                List<ProjectRoleDTO> roles = projectRoleService.GetProjectRoles().ToList();
                string[] result = new string[roles.Count];
                int i = 0;
                foreach (var role in roles)
                {
                    result[i] = JsonConvert.SerializeObject(role);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Роли не найдены");
            }
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                ProjectRoleDTO role = projectRoleService.GetProjectRoleById(id);
                var result = JsonConvert.SerializeObject(role);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Роль не найдена");
            }
        }
    }
}