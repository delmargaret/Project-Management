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
    public class ProjectWorksController : ApiController
    {
        static IUnitOfWork uow = new ContextUnitOfWork("ManagementContext");
        IProjectWorkService projectWorkService = new ProjectWorkService(uow, new Map<ProjectWork, ProjectWorkDTO>());
        ProjectWorkValidator pwvalidator = new ProjectWorkValidator();

        [HttpGet]
        public IHttpActionResult GetProjectWorks()
        {
            try
            {
                List<ProjectWorkDTO> works = projectWorkService.GetAllProjectWorks().ToList();
                string[] result = new string[works.Count];
                int i = 0;
                foreach (var work in works)
                {
                    result[i] = JsonConvert.SerializeObject(work);
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
        public IHttpActionResult GetNames(int projId)
        {
            try
            {
                var works = projectWorkService.GetNamesOnProject(projId).ToList();
                string[] result = new string[works.Count];
                int i = 0;
                foreach (var work in works)
                {
                    result[i] = JsonConvert.SerializeObject(work);
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
        public IHttpActionResult GetNamesAndLoad(int projectId)
        {
            try
            {
                var works = projectWorkService.GetNamesAndLoadOnProject(projectId).ToList();
                string[] result = new string[works.Count];
                int i = 0;
                foreach (var work in works)
                {
                    result[i] = JsonConvert.SerializeObject(work);
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
        public IHttpActionResult GetEmployeesProjects(int empId)
        {
            try
            {
                var works = projectWorkService.GetEmployeesProjects(empId).ToList();
                string[] result = new string[works.Count];
                int i = 0;
                foreach (var work in works)
                {
                    result[i] = JsonConvert.SerializeObject(work);
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
        public IHttpActionResult GetEmployeesOnProjects(int prjctId)
        {
            try
            {
                var works = projectWorkService.GetEmployeesOnProject(prjctId).ToList();
                string[] result = new string[works.Count];
                int i = 0;
                foreach (var work in works)
                {
                    result[i] = JsonConvert.SerializeObject(work);
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
        public IHttpActionResult GetById(int id)
        {
            try
            {
                ProjectWorkDTO work = projectWorkService.GetProjectWorkById(id);
                var result = JsonConvert.SerializeObject(work);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
        }

        [HttpGet]
        public IHttpActionResult CalculateEmployeesWorkload(int employeeId)
        {
            try
            {
                int load = projectWorkService.CalculateEmployeesWorkload(employeeId);
                var result = JsonConvert.SerializeObject(load);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
            catch (PercentOrScheduleException)
            {
                return BadRequest("Тип загруженности - расписание");
            }
        }

        [HttpPost]
        public IHttpActionResult CreateProjectWork([FromBody]ProjectWorkDTO work)
        {
            try
            {
                var result = work;
                var validationResult = pwvalidator.Validate(work);
                if (validationResult.Count != 0)
                {
                    return BadRequest("Объект не валиден");
                }
                projectWorkService.CreateProjectWork(work);
                return Ok(work);
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
            catch (ObjectAlreadyExistsException)
            {
                return BadRequest("Сотрудник уже добавлен на проект");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteProjectWorkById(int id)
        {
            try
            {
                projectWorkService.DeleteProjectWorkById(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Участие в проекте не найдено");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteEmployeeFromProject(int prId, int emId)
        {
            try
            {
                projectWorkService.DeleteEmployeeFromProject(prId, emId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeProject(int id, [FromBody]int projectId)
        {
            try
            {
                projectWorkService.ChangeProject(id, projectId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeEmployee(int id, [FromBody]int employeeId)
        {
            try
            {
                projectWorkService.ChangeEmployee(id, employeeId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeEmployeesProjectRole(int id, [FromBody]int projectRoleId)
        {
            try
            {
                projectWorkService.ChangeEmployeesProjectRole(id, projectRoleId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeWorkLoad(int id, [FromBody]int newWorkload)
        {
            try
            {
                projectWorkService.ChangeWorkLoad(id, newWorkload);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
        }

        [HttpPut]
        public IHttpActionResult AddWorkLoad(int id, [FromBody]int workload)
        {
            try
            {
                projectWorkService.AddWorkLoad(id, workload);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
            catch (PercentOrScheduleException)
            {
                return BadRequest("Тип загруженности - расписание");
            }
        }

        [HttpPut]
        public IHttpActionResult DeleteWorkLoad(int id)
        {
            try
            {
                projectWorkService.DeleteWorkLoad(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
            catch (PercentOrScheduleException)
            {
                return BadRequest("Тип загруженности - расписание");
            }
        }
    }
}