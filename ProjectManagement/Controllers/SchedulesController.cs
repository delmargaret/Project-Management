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
    public class SchedulesController : ApiController
    {
        ScheduleValidator svalidator = new ScheduleValidator();
        IScheduleService scheduleServise = new ScheduleServise(new ContextUnitOfWork("ManagementContext"));

        //IScheduleService scheduleServise;
        //public SchedulesController(IScheduleService serv)
        //{
        //    scheduleServise = serv;
        //}
        [HttpGet]
        public IHttpActionResult GetAllSchedules()
        {
            try
            {
                List<ScheduleDTO> days = scheduleServise.GetAllSchedules().ToList();
                string[] result = new string[days.Count];
                int i = 0;
                foreach (var day in days)
                {
                    result[i] = JsonConvert.SerializeObject(day);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Объекты не найдены");
            }
        }

        [HttpGet]
        public IHttpActionResult GetScheduleOnProjectWork(int projectWorkId)
        {
            try
            {
                var days = scheduleServise.GetScheduleOnProjectWork(projectWorkId).ToList();
                string[] result = new string[days.Count];
                int i = 0;
                foreach (var day in days)
                {
                    result[i] = JsonConvert.SerializeObject(day);
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
        public IHttpActionResult GetScheduleById(int id)
        {
            try
            {
                ScheduleDTO schedule = scheduleServise.GetScheduleById(id);
                var result = JsonConvert.SerializeObject(schedule);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("История не найдена");
            }
        }

        [HttpGet]
        public IHttpActionResult GetEmployeesFreeDays(int employeeId)
        {
            try
            {
                var days = scheduleServise.GetEmployeesFreeDays(employeeId).ToList();
                string[] result = new string[days.Count];
                int i = 0;
                foreach (var day in days)
                {
                    result[i] = JsonConvert.SerializeObject(day);
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
        public IHttpActionResult GetEmployeesSchedule(int empId)
        {
            try
            {
                var days = scheduleServise.GetEmployeesSchedule(empId).ToList();
                string[] result = new string[days.Count];
                int i = 0;
                foreach (var day in days)
                {
                    result[i] = JsonConvert.SerializeObject(day);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
        }

        [HttpPost]
        public IHttpActionResult CreateSchedule([FromBody]ScheduleDTO schedule)
        {
            try
            {
                var result = schedule;
                var validationResult = svalidator.Validate(schedule);
                if (validationResult.Count != 0)
                {
                    return BadRequest("Объект не валиден");
                }
                scheduleServise.CreateSchedule(schedule);
                return Ok(schedule);
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
            catch (ObjectAlreadyExistsException)
            {
                return BadRequest("Расписание уже существует");
            }
            catch (PercentOrScheduleException)
            {
                return BadRequest("Тип загруженности - проценты");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteScheduleById(int id)
        {
            try
            {
                scheduleServise.DeleteScheduleById(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Расписание не найдено");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteScheduleByProjectWorkId(int projectWorkId)
        {
            try
            {
                scheduleServise.DeleteScheduleByProjectWorkId(projectWorkId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объекты не найдены");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeScheduleDay(int id, [FromBody]int scheduleDayId)
        {
            try
            {
                scheduleServise.ChangeScheduleDay(id, scheduleDayId);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
        }
    }
}