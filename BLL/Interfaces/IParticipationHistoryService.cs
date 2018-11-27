using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IParticipationHistoryService
    {
        ParticipationHistoryDTO CreateHistory(ParticipationHistoryDTO history);
        void DeleteHistoryById(int id);
        ParticipationHistoryDTO GetHistoryById(int id);
        ParticipationHistoryDTO GetEmployeesHistory(int projectWorkId);
        IEnumerable<ParticipationHistoryDTO> GetAllHistories();
        IEnumerable<ParticipationHistoryDTO> GetAllEmployeesHistoriesOnProject(int projectWorkId);
        void ChangeHistoryStartDate(int id, DateTimeOffset start);
        void ChangeHistoryEndDate(int id, DateTimeOffset end);
        void Dispose();
    }
}
