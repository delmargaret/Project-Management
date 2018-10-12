using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ParticipationHistoryRepository : IParticipationHistoryRepository
    {
        private ManagementContext db;

        public ParticipationHistoryRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<ParticipationHistory> GetAllHistories()
        {
            return db.ParticipationHistories;
        }

        public IEnumerable<ParticipationHistory> GetAllEmployeesHistoriesOnProject(int projectWorkId)
        {
            return db.ParticipationHistories.Where(item => item.ProjectWorkId == projectWorkId);
        }

        public ParticipationHistory GetLastEmployeesHistory(int projectWorkId)
        {
            return db.ParticipationHistories.Where(item => item.ProjectWorkId == projectWorkId).Last();
        }

        public void ChangeHistoryStartDate(int id, DateTimeOffset start)
        {
            ParticipationHistory history = db.ParticipationHistories.Find(id);
            if (history != null)
                history.StartDate = start;
        }

        public void ChangeHistoryEndDate(int id, DateTimeOffset end)
        {
            ParticipationHistory history = db.ParticipationHistories.Find(id);
            if (history != null)
                history.EndDate = end;
        }

        public ParticipationHistory GetHistoryById(int id)
        {
            return db.ParticipationHistories.Find(id);
        }

        public void CreateHistory(ParticipationHistory participationHistory)
        {
            db.ParticipationHistories.Add(participationHistory);
        }

        public void UpdateHistory(ParticipationHistory participationHistory)
        {
            db.Entry(participationHistory).State = EntityState.Modified;
        }

        public IEnumerable<ParticipationHistory> FindHistory(Func<ParticipationHistory, Boolean> predicate)
        {
            return db.ParticipationHistories.Where(predicate).ToList();
        }

        public void DeleteHistory(int id)
        {
            ParticipationHistory participation = db.ParticipationHistories.Find(id);
            if (participation != null)
                db.ParticipationHistories.Remove(participation);
        }
    }
}
