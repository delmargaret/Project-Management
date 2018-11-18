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
    public class ProjectsController : ApiController
    {
        IProjectService projectService = new ProjectService(new ContextUnitOfWork("ManagementContext"));
        ProjectValidator pvalidator = new ProjectValidator();

        //IProjectService projectService;
        //public ProjectsController(IProjectService serv)
        //{
        //    projectService = serv;
        //}
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
                return Ok();
            }
        }

        [ActionName("sortByNameAsc")]
        [HttpGet]
        public IHttpActionResult GetByNameAsc()
        {
            try
            {
                List<ProjectDTO> projects = projectService.SortByNameAsc().ToList();
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
                return Ok();
            }
        }

        [ActionName("sortByNameDesc")]
        [HttpGet]
        public IHttpActionResult GetByNameDesc()
        {
            try
            {
                List<ProjectDTO> projects = projectService.SortByNameDesc().ToList();
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
                return Ok();
            }
        }

        [ActionName("sortByStartDateAsc")]
        [HttpGet]
        public IHttpActionResult GetByStartDateAsc()
        {
            try
            {
                List<ProjectDTO> projects = projectService.SortByStartDateAsc().ToList();
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
                return Ok();
            }
        }

        [ActionName("sortByStartDateDesc")]
        [HttpGet]
        public IHttpActionResult GetByStartDateDesc()
        {
            try
            {
                List<ProjectDTO> projects = projectService.SortByStartDateDesc().ToList();
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
                return Ok();
            }
        }

        [ActionName("sortByEndDateAsc")]
        [HttpGet]
        public IHttpActionResult GetByEndDateAsc()
        {
            try
            {
                List<ProjectDTO> projects = projectService.SortByEndDateAsc().ToList();
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
                return Ok();
            }
        }

        [ActionName("sortByEndDateDesc")]
        [HttpGet]
        public IHttpActionResult GetByEndDateDesc()
        {
            try
            {
                List<ProjectDTO> projects = projectService.SortByEndDateDesc().ToList();
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
                return Ok();
            }
        }

        [ActionName("sortByStatusAsc")]
        [HttpGet]
        public IHttpActionResult GetByStatusAsc()
        {
            try
            {
                List<ProjectDTO> projects = projectService.SortByStatusAsc().ToList();
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
                return Ok();
            }
        }

        [ActionName("sortByStatusDesc")]
        [HttpGet]
        public IHttpActionResult GetByStatusDesc()
        {
            try
            {
                List<ProjectDTO> projects = projectService.SortByStatusDesc().ToList();
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
                return Ok();
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
                return Ok();
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
                return Ok();
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