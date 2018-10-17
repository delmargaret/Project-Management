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
using BLL.Infrastructure;

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

            if (work == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            if (historyDTO.StartDate>historyDTO.EndDate)
            {
                throw new ProjectException("Неверные даты");
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

        public void DeleteHistoryById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор истории участия в проекте");
            }
            var history = Database.ParticipationHistories.GetHistoryById(id.Value);
            if (history == null)
            {
                throw new ProjectException("История участия в проекте не найдена");
            }
            Database.ParticipationHistories.DeleteHistory(id.Value);
            Database.Save();
        }

        public ParticipationHistoryDTO GetHistoryById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор истории участия в проекте");
            }
            var history = Database.ParticipationHistories.GetHistoryById(id.Value);
            if (history == null)
            {
                throw new ProjectException("История участия в проекте не найдена");
            }
            return Map.ObjectMap(history);
        }

        public ParticipationHistoryDTO GetLastEmployeesHistory(int? projectWorkId)
        {
            if (projectWorkId == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var work = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (work == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            var history = Database.ParticipationHistories.GetLastEmployeesHistory(projectWorkId.Value);
            if (history == null)
            {
                throw new ProjectException("История участия в проекте не найдена");
            }
            return Map.ObjectMap(history);
        }

        public IEnumerable<ParticipationHistoryDTO> GetAllHistories()
        {
            var histories = Database.ParticipationHistories.GetAllHistories();
            if (histories.Count() == 0)
            {
                throw new ProjectException("Истории участия в проекте не найдены");
            }
            return Map.ListMap(histories);
        }

        public IEnumerable<ParticipationHistoryDTO> GetAllEmployeesHistoriesOnProject(int? projectWorkId)
        {
            if (projectWorkId == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            var histories = Database.ParticipationHistories.GetAllHistories();
            if (histories.Count() == 0)
            {
                throw new ProjectException("Истории участия в проекте не найдены");
            }
            return Map.ListMap(histories);
        }

        public void ChangeHistoryStartDate(int? id, DateTimeOffset start)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор истории участия в проекте");
            }
            var history = Database.ParticipationHistories.GetHistoryById(id.Value);
            if (history == null)
            {
                throw new ProjectException("История участия в проекте не найдена");
            }
            if (history.EndDate < start)
            {
                throw new ProjectException("Неверная дата начала участия в проекте");
            }
            Database.ParticipationHistories.ChangeHistoryStartDate(id.Value, start);
            Database.Save();
        }

        public void ChangeHistoryEndDate(int? id, DateTimeOffset end)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор истории участия в проекте");
            }
            var history = Database.ParticipationHistories.GetHistoryById(id.Value);
            if (history == null)
            {
                throw new ProjectException("История участия в проекте не найдена");
            }
            if (history.StartDate > end)
            {
                throw new ProjectException("Неверная дата конца участия в проекте");
            }
            Database.ParticipationHistories.ChangeHistoryEndDate(id.Value, end);
            Database.Save();
        }
    }
}
