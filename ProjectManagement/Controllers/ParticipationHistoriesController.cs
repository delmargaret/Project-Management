using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using Exeption;
using Newtonsoft.Json;
using ProjectManagement.Filters;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Validation;

namespace ProjectManagement.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [JwtAuthentication]
    public class ParticipationHistoriesController : ApiController
    {
        ParticipationHistoryValidator phvalidator = new ParticipationHistoryValidator();

        IParticipationHistoryService participationHistoryService;
        public ParticipationHistoriesController(IParticipationHistoryService serv)
        {
            participationHistoryService = serv;
        }

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
                return Ok();
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
                return Ok();
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
                return Ok();
            }
        }

        [HttpGet]
        public IHttpActionResult GetEmployeesHistory(int projectWorkId)
        {
            try
            {
                ParticipationHistoryDTO history = participationHistoryService.GetEmployeesHistory(projectWorkId);
                var result = JsonConvert.SerializeObject(history);
                return Ok(result);
            }
            catch (NotFoundException)
            {
                return Ok();
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
                return Ok();
            }
            catch (ObjectAlreadyExistsException)
            {
                return Ok();
            }
            catch (InvalidDateException)
            {
                return Ok();
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteHistoryById(int id)
        {
            try
            {
                participationHistoryService.DeleteHistoryById(id);
                return Ok(id);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeHistoryStartDate(int id, [FromBody]DateTimeOffset start)
        {
            try
            {
                participationHistoryService.ChangeHistoryStartDate(id, start);
                return Ok(id);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
            catch (InvalidDateException)
            {
                return Ok();
            }
        }

        [HttpPut]
        public IHttpActionResult ChangeHistoryEndDate(int id, [FromBody]DateTimeOffset end)
        {
            try
            {
                participationHistoryService.ChangeHistoryEndDate(id, end);
                return Ok(id);
            }
            catch (NotFoundException)
            {
                return Ok();
            }
            catch (InvalidDateException)
            {
                return Ok();
            }
        }
    }
}