using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using DAL.Entities;
using Exeption;
using Newtonsoft.Json;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectManagement.Controllers
{
    public class ProjectRoleController : ApiController
    {
        static IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
        IProjectRoleService projectRoleService = new ProjectRoleService(uow, new Map<ProjectRole, ProjectRoleDTO>());

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