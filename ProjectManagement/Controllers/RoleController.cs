using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using DAL.Entities;
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
    [RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {
        IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");

        [Route("GetRoles")]
        [HttpGet]
        public IEnumerable<RoleViewModel> GetRoles()
        {
            RoleService roleService = new RoleService(uow, new Map<Role, RoleDTO>());
            IEnumerable<RoleDTO> roleDTOs = roleService.GetRoles();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<RoleDTO, RoleViewModel>()).CreateMapper();
            var roles = mapper.Map<IEnumerable<RoleDTO>, List<RoleViewModel>>(roleDTOs);
            return roles;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}