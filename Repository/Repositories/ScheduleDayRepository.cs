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
    public class ScheduleDayRepository : IScheduleDayRepository
    {
        private ManagementContext db;

        public ScheduleDayRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<ScheduleDay> GetAllScheduleDays()
        {
            if (db.ScheduleDays.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ScheduleDays;
        }

        public ScheduleDay GetScheduleDayById(int id)
        {
            if (db.ScheduleDays.Find(id) == null)
            {
                throw new NotFoundException();
            }
            return db.ScheduleDays.Find(id);
        }

        public void CreateScheduleDay(ScheduleDay day)
        {
            db.ScheduleDays.Add(day);
        }

        public void UpdateScheduleDay(ScheduleDay day)
        {
            db.Entry(day).State = EntityState.Modified;
        }

        public IEnumerable<ScheduleDay> FindScheduleDay(Func<ScheduleDay, Boolean> predicate)
        {
            if (db.ScheduleDays.Where(predicate).ToList().Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.ScheduleDays.Where(predicate).ToList();
        }

        public void DeleteScheduleDay(int id)
        {
            ScheduleDay day = db.ScheduleDays.Find(id);
            if (day == null)
            {
                throw new NotFoundException();
            }
                db.ScheduleDays.Remove(day);
        }
    }
}
