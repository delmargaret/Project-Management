using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using Exeption;
using Newtonsoft.Json;
using ProjectManagement.Filters;
using ProjectManagement.Models;
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
    public class RolesController : ApiController
    {
        IRoleService roleService;
        public RolesController(IRoleService serv)
        {
            roleService = serv;
        }

        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            try
            {
                List<RoleDTO> roles = roleService.GetRoles().ToList();
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
                return Ok();
            }
        }
    }
}