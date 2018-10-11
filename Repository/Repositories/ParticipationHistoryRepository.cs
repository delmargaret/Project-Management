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

        public IEnumerable<ParticipationHistory> GetAll()
        {
            return db.ParticipationHistories;
        }

        public ParticipationHistory Get(int id)
        {
            return db.ParticipationHistories.Find(id);
        }

        public void Create(ParticipationHistory participationHistory)
        {
            db.ParticipationHistories.Add(participationHistory);
        }

        public void Update(ParticipationHistory participationHistory)
        {
            db.Entry(participationHistory).State = EntityState.Modified;
        }

        public IEnumerable<ParticipationHistory> Find(Func<ParticipationHistory, Boolean> predicate)
        {
            return db.ParticipationHistories.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            ParticipationHistory participation = db.ParticipationHistories.Find(id);
            if (participation != null)
                db.ParticipationHistories.Remove(participation);
        }
    }
}
