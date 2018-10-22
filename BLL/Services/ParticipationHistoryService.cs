using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapping;
using DAL.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exeption;

namespace BLL.Services
{
    public class ParticipationHistoryService : IParticipationHistoryService
    {
        IUnitOfWork Database { get; set; }
        Map<ParticipationHistory, ParticipationHistoryDTO> Map { get; set; }

        public ParticipationHistoryService(IUnitOfWork uow, Map<ParticipationHistory, ParticipationHistoryDTO> map)
        {
            Database = uow;
            Map = map;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void CreateHistory(ParticipationHistoryDTO historyDTO)
        {
            ProjectWork work = Database.ProjectWorks.GetProjectWorkById(historyDTO.ProjectWorkId);

            if (historyDTO.StartDate > historyDTO.EndDate)
            {
                throw new InvalidDateException();
            }
            ParticipationHistory history = new ParticipationHistory
            {
                ProjectWorkId = historyDTO.ProjectWorkId,
                ProjectWork=work,
                StartDate = historyDTO.StartDate,
                EndDate=historyDTO.EndDate
            };

            Database.ParticipationHistories.CreateHistory(history);
            Database.Save();
        }

        public void DeleteHistoryById(int id)
        {
            var history = Database.ParticipationHistories.GetHistoryById(id);
            Database.ParticipationHistories.DeleteHistory(history.Id);
            Database.Save();
        }

        public ParticipationHistoryDTO GetHistoryById(int id)
        {
            var history = Database.ParticipationHistories.GetHistoryById(id);
            return Map.ObjectMap(history);
        }

        public ParticipationHistoryDTO GetLastEmployeesHistory(int projectWorkId)
        {
            var work = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            var history = Database.ParticipationHistories.GetLastEmployeesHistory(work.Id);
            return Map.ObjectMap(history);
        }

        public IEnumerable<ParticipationHistoryDTO> GetAllHistories()
        {
            var histories = Database.ParticipationHistories.GetAllHistories();
            return Map.ListMap(histories);
        }

        public IEnumerable<ParticipationHistoryDTO> GetAllEmployeesHistoriesOnProject(int projectWorkId)
        {
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            var histories = Database.ParticipationHistories.GetAllEmployeesHistoriesOnProject(projectWork.Id);
            return Map.ListMap(histories);
        }

        public void ChangeHistoryStartDate(int id, DateTimeOffset start)
        {
            var history = Database.ParticipationHistories.GetHistoryById(id);
            if (history.EndDate < start)
            {
                throw new InvalidDateException();
            }
            Database.ParticipationHistories.ChangeHistoryStartDate(history.Id, start);
            Database.Save();
        }

        public void ChangeHistoryEndDate(int id, DateTimeOffset end)
        {
            var history = Database.ParticipationHistories.GetHistoryById(id);
            if (history.StartDate > end)
            {
                throw new InvalidDateException();
            }
            Database.ParticipationHistories.ChangeHistoryEndDate(history.Id, end);
            Database.Save();
        }
    }
}
