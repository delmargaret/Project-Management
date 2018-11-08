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

namespace ProjectManagement.Controllers
{
    [RoutePrefix("api/Project")]
    public class ProjectController : ApiController
    {
        static IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
        ProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());
        // GET api/<controller>

        [Route("GetProjects")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<ProjectDTO> projects = projectService.GetAllProjects().ToList();
            string[] result = new string[projects.Count];
            int i = 0;
            foreach (var project in projects)
            {
                result[i] = JsonConvert.SerializeObject(project);
                i++;
            }
            return result;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [Route("CreateProject")]
        [HttpPost]
        public IHttpActionResult CreateProject([FromBody]ProjectDTO value)
        {
            var res = value;
            projectService.CreateProject(value);
            return Ok(value);
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