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
    public class ScheduleRepository : IScheduleRepository
    {
        private ManagementContext db;

        public ScheduleRepository(ManagementContext context)
        {
            this.db = context;
        }

        public void ChangeScheduleDay(int scheduleId, int scheduleDayId)
        {
            Schedule schedule = db.Schedules.Find(scheduleId);
            if (schedule != null)
            {
                schedule.ScheduleDayId = scheduleDayId;
                schedule.ScheduleDay = db.ScheduleDays.Find(scheduleDayId);
            }
        }

        public void CreateSchedule(Schedule item)
        {
            db.Schedules.Add(item);
        }

        public void DeleteScheduleById(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule != null)
                db.Schedules.Remove(schedule);
        }

        public void DeleteScheduleByProjectWorkId(int projectWorkId)
        {
            var days = db.Schedules.Where(item => item.ProjectWorkId == projectWorkId);
            foreach(var day in days)
            {
                db.Schedules.Remove(day);
            }
        }

        public IEnumerable<Schedule> FindSchedule(Func<Schedule, bool> predicate)
        {
            return db.Schedules.Where(predicate).ToList();
        }

        public IEnumerable<Schedule> GetAllSchedules()
        {
            return db.Schedules;
        }

        public Schedule GetScheduleById(int id)
        {
            return db.Schedules.Find(id);
        }

        public IEnumerable<Schedule> GetScheduleOnProjectWork(int projectWorkId)
        {
            return db.Schedules.Where(item => item.ProjectWorkId == projectWorkId);
        }

        public void UpdateSchedule(Schedule item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
