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
        ParticipationHistory GetEmployeesHistory(int projectWorkId);
        IEnumerable<ParticipationHistory> FindHistory(Func<ParticipationHistory, Boolean> predicate);
        ParticipationHistory CreateHistory(ParticipationHistory item);
        void FindSameHistory(int projWorkId, DateTimeOffset start, DateTimeOffset end);
        void UpdateHistory(ParticipationHistory item);
        void DeleteHistory(int id);
        void ChangeHistoryStartDate(int id, DateTimeOffset start);
        void ChangeHistoryEndDate(int id, DateTimeOffset end);
    }
}
