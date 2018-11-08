using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using DAL.Entities;
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
    [RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {
        static IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
        RoleService roleService = new RoleService(uow, new Map<Role, RoleDTO>());

        [Route("GetRoles")]
        [HttpGet]
        public IEnumerable<string> GetRoles()
        {
            List<RoleDTO> roles = roleService.GetRoles().ToList();
            string[] result = new string[roles.Count];
            int i = 0;
            foreach(var role in roles)
            {
                result[i] = JsonConvert.SerializeObject(role);
                i++;
            }
            return result;
        }
    }
}