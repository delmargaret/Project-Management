using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IParticipationHistoryRepository
    {
        IEnumerable<ParticipationHistory> GetAllHistories();
        IEnumerable<ParticipationHistory> GetAllEmployeesHistoriesOnProject(int projectWorkId);
        ParticipationHistory GetHistoryById(int id);
        ParticipationHistory GetLastEmployeesHistory(int projectWorkId);
        IEnumerable<ParticipationHistory> FindHistory(Func<ParticipationHistory, Boolean> predicate);
        void CreateHistory(ParticipationHistory item);
        void UpdateHistory(ParticipationHistory item);
        void DeleteHistory(int id);
        void ChangeHistoryStartDate(int id, DateTimeOffset start);
        void ChangeHistoryEndDate(int id, DateTimeOffset end);
    }
}
