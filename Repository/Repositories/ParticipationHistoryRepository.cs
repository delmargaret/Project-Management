using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;
using Exeption;

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
            if (db.ParticipationHistories.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ParticipationHistories;
        }

        public IEnumerable<ParticipationHistory> GetAllEmployeesHistoriesOnProject(int projectWorkId)
        {
            if(db.ParticipationHistories.Where(item => item.ProjectWorkId == projectWorkId).Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ParticipationHistories.Where(item => item.ProjectWorkId == projectWorkId);
        }

        public ParticipationHistory GetLastEmployeesHistory(int projectWorkId)
        {
            List<ParticipationHistory> list = new List<ParticipationHistory>();
            list = db.ParticipationHistories.Where(item => item.ProjectWorkId == projectWorkId).ToList();
            if (list.Last() == null)
            {
                throw new NotFoundException();
            }
            return list.Last();
        }

        public void ChangeHistoryStartDate(int id, DateTimeOffset start)
        {
            ParticipationHistory history = db.ParticipationHistories.Find(id);
            if (history == null)
            {
                throw new NotFoundException();
            }
                history.StartDate = start;
        }

        public void ChangeHistoryEndDate(int id, DateTimeOffset end)
        {
            ParticipationHistory history = db.ParticipationHistories.Find(id);
            if (history == null)
            {
                throw new NotFoundException();
            }
                history.EndDate = end;
        }

        public ParticipationHistory GetHistoryById(int id)
        {
            if (db.ParticipationHistories.Find(id) == null)
            {
                throw new NotFoundException();
            }
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
            if (db.ParticipationHistories.Where(predicate).ToList().Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ParticipationHistories.Where(predicate).ToList();
        }

        public void DeleteHistory(int id)
        {
            ParticipationHistory participation = db.ParticipationHistories.Find(id);
            if (participation == null)
            {
                throw new NotFoundException();
            }
                db.ParticipationHistories.Remove(participation);
        }
    }
}
