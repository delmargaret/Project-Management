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
    public class ScheduleRepository : IScheduleRepository
    {
        private ManagementContext db;

        public ScheduleRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<ScheduleDay> GetEmployeesFreeDays(int employeeId)
        {
            List<ScheduleDay> list = new List<ScheduleDay>();
            var works = db.ProjectWorks.Where(item => item.EmployeeId == employeeId);
            foreach(var work in works)
            {
                var schedule = db.Schedules.Where(item => item.ProjectWorkId == work.Id);
                foreach(var day in schedule)
                {
                    list.Add(day.ScheduleDay);
                }
            }
            if (list.Count() == 0)
            {
                throw new NotFoundException();
            }
            return list;
        }

        public void ChangeScheduleDay(int scheduleId, int scheduleDayId)
        {
            Schedule schedule = db.Schedules.Find(scheduleId);
            if (schedule == null)
            {
                throw new NotFoundException();
            }
            schedule.ScheduleDayId = scheduleDayId;
            schedule.ScheduleDay = db.ScheduleDays.Find(scheduleDayId);
        }

        public Schedule CreateSchedule(Schedule item)
        {
            var sch = db.Schedules.Add(item);
            return sch;
        }

        public void DeleteScheduleById(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                throw new NotFoundException();
            }
                db.Schedules.Remove(schedule);
        }

        public void DeleteScheduleByProjectWorkId(int projectWorkId)
        {
            var days = db.Schedules.Where(item => item.ProjectWorkId == projectWorkId);
            if (days.Count() == 0)
            {
                throw new NotFoundException();
            }
            foreach(var day in days)
            {
                db.Schedules.Remove(day);
            }
        }

        public IEnumerable<Schedule> FindSchedule(Func<Schedule, bool> predicate)
        {
            if (db.Schedules.Where(predicate).ToList().Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Schedules.Where(predicate).ToList();
        }

        public IEnumerable<Schedule> GetAllSchedules()
        {
            if (db.Schedules.Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Schedules;
        }

        public Schedule GetScheduleById(int id)
        {
            if (db.Schedules.Find(id) == null)
            {
                throw new NotFoundException();
            }
            return db.Schedules.Find(id);
        }

        public IEnumerable<Schedule> GetScheduleOnProjectWork(int projectWorkId)
        {
            if(db.Schedules.Where(item => item.ProjectWorkId == projectWorkId).Count() == 0)
            {
                throw new NotFoundException();
            }
            return db.Schedules.Where(item => item.ProjectWorkId == projectWorkId);
        }

        public void UpdateSchedule(Schedule item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
