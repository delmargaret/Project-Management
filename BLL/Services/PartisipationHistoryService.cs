using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PartisipationHistoryService : IParticipationHistoryService
    {
        IUnitOfWork Database { get; set; }

        public PartisipationHistoryService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void CreateHistory(ParticipationHistoryDTO historyDTO)
        {
            ProjectWork work = Database.ProjectWorks.GetProjectWorkById(historyDTO.ProjectWorkId);

            if (work == null)
                Console.WriteLine("история участия в проекте не создана");
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
                Console.WriteLine("не установлено id истории");
            var history = Database.ParticipationHistories.GetHistoryById(id.Value);
            if (history == null)
                Console.WriteLine("истории не существует");
            Database.ParticipationHistories.DeleteHistory(id.Value);
            Database.Save();
        }

        public ParticipationHistoryDTO GetHistoryById(int? id)
        {
            var history = Database.ParticipationHistories.GetHistoryById(id.Value);
            if (history == null)
                Console.WriteLine("история не найдена");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ParticipationHistory, ParticipationHistoryDTO>()).CreateMapper();
            return mapper.Map<ParticipationHistory, ParticipationHistoryDTO>(Database.ParticipationHistories.GetHistoryById(id.Value));
        }

        public ParticipationHistoryDTO GetLastEmployeesHistory(int? projectWorkId)
        {
            var history = Database.ParticipationHistories.GetLastEmployeesHistory(projectWorkId.Value);
            if (history == null)
                Console.WriteLine("история не найдена");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ParticipationHistory, ParticipationHistoryDTO>()).CreateMapper();
            return mapper.Map<ParticipationHistory, ParticipationHistoryDTO>(Database.ParticipationHistories.GetLastEmployeesHistory(projectWorkId.Value));
        }

        public IEnumerable<ParticipationHistoryDTO> GetAllHistories()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ParticipationHistory, ParticipationHistoryDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ParticipationHistory>, List<ParticipationHistoryDTO>>(Database.ParticipationHistories.GetAllHistories());
        }

        public IEnumerable<ParticipationHistoryDTO> GetAllEmployeesHistoriesOnProject(int? projectWorkId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ParticipationHistory, ParticipationHistoryDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ParticipationHistory>, List<ParticipationHistoryDTO>>(Database.ParticipationHistories.GetAllEmployeesHistoriesOnProject(projectWorkId.Value));
        }

        public void ChangeHistoryStartDate(int? id, DateTimeOffset start)
        {
            if (id == null)
                Console.WriteLine("не установлено id истории");
            var history = Database.ParticipationHistories.GetHistoryById(id.Value);
            if (history == null)
                Console.WriteLine("истории не существует");
            Database.ParticipationHistories.ChangeHistoryStartDate(id.Value, start);
            Database.Save();
        }

        public void ChangeHistoryEndDate(int? id, DateTimeOffset end)
        {
            if (id == null)
                Console.WriteLine("не установлено id истории");
            var history = Database.ParticipationHistories.GetHistoryById(id.Value);
            if (history == null)
                Console.WriteLine("истории не существует");
            Database.ParticipationHistories.ChangeHistoryEndDate(id.Value, end);
            Database.Save();
        }
    }
}
