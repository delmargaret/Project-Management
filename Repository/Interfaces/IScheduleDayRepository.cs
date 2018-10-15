using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IScheduleDayRepository
    {
        IEnumerable<ScheduleDay> GetAllScheduleDays();
        ScheduleDay GetScheduleDayById(int id);
        IEnumerable<ScheduleDay> FindScheduleDay(Func<ScheduleDay, Boolean> predicate);
        void CreateScheduleDay(ScheduleDay item);
        void UpdateScheduleDay(ScheduleDay item);
        void DeleteScheduleDay(int id);
    }
}
