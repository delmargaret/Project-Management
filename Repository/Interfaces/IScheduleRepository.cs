using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IScheduleRepository
    {
        IEnumerable<Schedule> GetAllSchedules();
        IEnumerable<Schedule> GetScheduleOnProjectWork(int projectWorkId);
        Schedule GetScheduleById(int id);
        IEnumerable<Schedule> FindSchedule(Func<Schedule, Boolean> predicate);
        IEnumerable<ScheduleDay> GetEmployeesFreeDays(int employeeId);
        IEnumerable<ScheduleDay> GetEmployeesSchedule(int employeeId);
        Schedule CreateSchedule(Schedule item);
        void FindSameSchedule(int projWorkId, int dayId);
        void UpdateSchedule(Schedule item);
        void DeleteScheduleById(int id);
        void DeleteScheduleByProjectWorkId(int projectWorkId);
        void ChangeScheduleDay(int scheduleId, int scheduleDayId);
    }
}
