using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using DAL.Entities;
using Exeption;
using Newtonsoft.Json;
using ProjectManagement.Models;
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
    public class RolesController : ApiController
    {
        //static IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
        IRoleService roleService = new RoleService(new ContextUnitOfWork("ManagementContext"));

        //IRoleService roleService;
        //public RolesController(IRoleService serv)
        //{
        //    roleService = serv;
        //}
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
            catch(NotFoundException)
            {
                return BadRequest("Роли не найдены");
            }
        }
    }
}