using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IScheduleService
    {
        IEnumerable<ScheduleDTO> GetAllSchedules();
        IEnumerable<ScheduleDTO> GetScheduleOnProjectWork(int projectWorkId);
        ScheduleDTO GetScheduleById(int id);
        IEnumerable<ScheduleDayDTO> GetEmployeesFreeDays(int employeeId);
        IEnumerable<ScheduleDayDTO> GetEmployeesSchedule(int employeeId);
        ScheduleDTO CreateSchedule(ScheduleDTO item);
        void DeleteScheduleById(int id);
        void DeleteScheduleByProjectWorkId(int projectWorkId);
        void ChangeScheduleDay(int scheduleId, int scheduleDayId);
        void Dispose();
    }
}
