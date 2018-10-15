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
    public class ScheduleDayRepository : IScheduleDayRepository
    {
        private ManagementContext db;

        public ScheduleDayRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<ScheduleDay> GetAllScheduleDays()
        {
            return db.ScheduleDays;
        }

        public ScheduleDay GetScheduleDayById(int id)
        {
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
            return db.ScheduleDays.Where(predicate).ToList();
        }

        public void DeleteScheduleDay(int id)
        {
            ScheduleDay day = db.ScheduleDays.Find(id);
            if (day != null)
                db.ScheduleDays.Remove(day);
        }
    }
}
