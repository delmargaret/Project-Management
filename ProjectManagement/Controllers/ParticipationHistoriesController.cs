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
    public class ParticipationHistoriesController : ApiController
    {
        IParticipationHistoryService participationHistoryService = new ParticipationHistoryService(new ContextUnitOfWork("ManagementContext"));
        ParticipationHistoryValidator phvalidator = new ParticipationHistoryValidator();

        //IParticipationHistoryService participationHistoryService;
        //public ParticipationHistoriesController(IParticipationHistoryService serv)
        //{
        //    participationHistoryService = serv;
        //}
        [HttpGet]
        public IHttpActionResult GetAllHistories()
        {
            try
            {
                List<ParticipationHistoryDTO> histories = participationHistoryService.GetAllHistories().ToList();
                string[] result = new string[histories.Count];
                int i = 0;
                foreach (var history in histories)
                {
                    result[i] = JsonConvert.SerializeObject(history);
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
        public IHttpActionResult GetAllEmployeesHistoriesOnProject(int projWorkId)
        {
            try
            {
                var histories = participationHistoryService.GetAllEmployeesHistoriesOnProject(projWorkId).ToList();
                string[] result = new string[histories.Count];
                int i = 0;
                foreach (var history in histories)
                {
                    result[i] = JsonConvert.SerializeObject(history);
                    i++;
                }
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Истории не найдены");
            }
        }

        [HttpGet]
        public IHttpActionResult GetHistoryById(int id)
        {
            try
            {
                ParticipationHistoryDTO history = participationHistoryService.GetHistoryById(id);
                var result = JsonConvert.SerializeObject(history);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("История не найдена");
            }
        }

        [HttpGet]
        public IHttpActionResult GetLastEmployeesHistory(int projectWorkId)
        {
            try
            {
                ParticipationHistoryDTO history = participationHistoryService.GetLastEmployeesHistory(projectWorkId);
                var result = JsonConvert.SerializeObject(history);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
        }

        [HttpPost]
        public IHttpActionResult CreateHistory([FromBody]ParticipationHistoryDTO history)
        {
            try
            {
                var result = history;
                var validationResult = phvalidator.Validate(history);
                if (validationResult.Count != 0)
                {
                    return BadRequest("Объект не валиден");
                }
                participationHistoryService.CreateHistory(history);
                return Ok(history);
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
            catch (ObjectAlreadyExistsException)
            {
                return BadRequest("История уже существует");
            }
            catch (InvalidDateException)
            {
                return BadRequest("Неверные даты");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteHistoryById(int id)
        {
            try
            {
                participationHistoryService.DeleteHistoryById(id);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("История не найдена");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeHistoryStartDate(int id, [FromBody]DateTimeOffset start)
        {
            try
            {
                participationHistoryService.ChangeHistoryStartDate(id, start);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
            catch (InvalidDateException)
            {
                return BadRequest("Неверные даты");
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeHistoryEndDate(int id, [FromBody]DateTimeOffset end)
        {
            try
            {
                participationHistoryService.ChangeHistoryEndDate(id, end);
                return Ok();
            }
            catch (NotFoundException)
            {
                return BadRequest("Объект не найден");
            }
            catch (InvalidDateException)
            {
                return BadRequest("Неверные даты");
            }
        }
    }
}