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
using Validation;

namespace ProjectManagement.Controllers
{
    public class ProjectController : ApiController
    {
        static IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
        IProjectService projectService = new ProjectService(uow, new Map<Project, ProjectDTO>());
        ProjectValidator pvalidator = new ProjectValidator();
        // GET api/<controller>

        [HttpGet]
        public IHttpActionResult GetProjects()
        {
            try
            {
                List<ProjectDTO> projects = projectService.GetAllProjects().ToList();
                string[] result = new string[projects.Count];
                int i = 0;
                foreach (var project in projects)
                {
                    result[i] = JsonConvert.SerializeObject(project);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Проекты не найдены");
            }
        }

        [HttpGet]
        public IHttpActionResult GetByStatusId(int statusId)
        {
            try
            {
                List<ProjectDTO> projects = projectService.GetAllProjectsByStatusId(statusId).ToList();
                string[] result = new string[projects.Count];
                int i = 0;
                foreach (var project in projects)
                {
                    result[i] = JsonConvert.SerializeObject(project);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Проекты не найдены");
            }
        }

        [HttpGet]
        public IHttpActionResult GetEndingInNDays(int numberOfDays)
        {
            try
            {
                List<ProjectDTO> projects = projectService.GetProjectsEndingInNDays(numberOfDays).ToList();
                string[] result = new string[projects.Count];
                int i = 0;
                foreach (var project in projects)
                {
                    result[i] = JsonConvert.SerializeObject(project);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Проекты не найдены");
            }
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                ProjectDTO project = projectService.GetProjectById(id);
                var result = JsonConvert.SerializeObject(project);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Проект не найден");
            }
        }

        [HttpPost]
        public IHttpActionResult CreateProject([FromBody]ProjectDTO project)
        {
            try
            {
                var result = project;
                var validationResult = pvalidator.Validate(project);
                if (validationResult.Count != 0)
                {
                    return BadRequest("Объект не валиден");
                }
                projectService.CreateProject(project);
                return Ok(project);
            }
            catch (ObjectAlreadyExistsException)
            {
                return BadRequest("Проект уже добавлен");
            }
            catch (InvalidDateException)
            {
                return BadRequest("Неверные даты");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteById(int id)
        {
            try
            {
                projectService.DeleteProjectById(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Проект не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeName(int id, [FromBody]string projName)
        {
            try
            {
                projectService.ChangeProjectName(id, projName);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Проект не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeDescription(int id, [FromBody]string projDescription)
        {
            try
            {
                projectService.ChangeProjectDescription(id, projDescription);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Проект не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeStartDate(int id, [FromBody]DateTimeOffset start)
        {
            try
            {
                projectService.ChangeProjectStartDate(id, start);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Проект не найден");
            }
            catch (InvalidDateException)
            {
                return BadRequest("Неверные даты");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeEndDate(int id, [FromBody]DateTimeOffset end)
        {
            try
            {
                projectService.ChangeProjectEndDate(id, end);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Проект не найден");
            }
            catch (InvalidDateException)
            {
                return BadRequest("Неверные даты");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeProjectStatus(int id, [FromBody]int projStatusId)
        {
            try
            {
                projectService.ChangeProjectStatus(id, projStatusId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Проект не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult CloseProject(int id)
        {
            try
            {
                projectService.CloseProject(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Проект не найден");
            }
        }
    }
}