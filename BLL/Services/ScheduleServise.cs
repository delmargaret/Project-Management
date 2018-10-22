using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interfaces;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using BLL.Mapping;
using Exeption;

namespace BLL.Services
{
    public class ScheduleServise : IScheduleService
    {
        IUnitOfWork Database { get; set; }
        Map<Schedule, ScheduleDTO> Map { get; set; }
        Map<ScheduleDay, ScheduleDayDTO> DayMap { get; set; }

        public ScheduleServise(IUnitOfWork uow, Map<Schedule, ScheduleDTO> map, Map<ScheduleDay, ScheduleDayDTO> dayMap)
        {
            Database = uow;
            Map = map;
            DayMap = dayMap;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<ScheduleDTO> GetAllSchedules()
        {
            var schedules = Database.Schedules.GetAllSchedules();
            return Map.ListMap(schedules);
        }

        public IEnumerable<ScheduleDTO> GetScheduleOnProjectWork(int projectWorkId)
        {
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            var schedules = Database.Schedules.GetScheduleOnProjectWork(projectWork.Id);
            return Map.ListMap(schedules);
        }

        public ScheduleDTO GetScheduleById(int id)
        {
            var schedule = Database.Schedules.GetScheduleById(id);
            return Map.ObjectMap(schedule);
        }

        public IEnumerable<ScheduleDayDTO> GetEmployeesFreeDays(int employeeId)
        {
            Employee employee = Database.Employees.GetEmployeeById(employeeId);
            var freedays = Database.Schedules.GetEmployeesFreeDays(employee.Id);
            return DayMap.ListMap(freedays);
        }

        public void CreateSchedule(ScheduleDTO item)
        {
            ProjectWork work = Database.ProjectWorks.GetProjectWorkById(item.ProjectWorkId);
            ScheduleDay day = Database.ScheduleDays.GetScheduleDayById(item.ScheduleDayId);
            Employee employee = Database.Employees.GetEmployeeById(work.EmployeeId);
            if (employee.PercentOrScheduleId == 3)
            {
                employee.PercentOrScheduleId = 2;
                employee.PercentOrSchedule = Database.WorkLoads.GetTypeById(2);
                Schedule sch = new Schedule
                {
                    ProjectWorkId = item.ProjectWorkId,
                    ProjectWork = work,
                    ScheduleDayId = item.ScheduleDayId,
                    ScheduleDay = day
                };
                Database.Schedules.CreateSchedule(sch);
                Database.Save();
                return;
            }
            if (employee.PercentOrScheduleId == 1)
            {
                throw new PercentOrScheduleException();
            }
            if (employee.PercentOrScheduleId == 2)
            {
                Schedule schedule = new Schedule
                {
                    ProjectWorkId = item.ProjectWorkId,
                    ProjectWork = work,
                    ScheduleDayId = item.ScheduleDayId,
                    ScheduleDay = day
                };

                Database.Schedules.CreateSchedule(schedule);
                Database.Save();
                return;
            }
        }

        public void DeleteScheduleById(int id)
        {
            var scedule = Database.Schedules.GetScheduleById(id);
            Database.Schedules.DeleteScheduleById(scedule.Id);
            Database.Save();
        }

        public void DeleteScheduleByProjectWorkId(int projectWorkId)
        {
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            Database.Schedules.DeleteScheduleByProjectWorkId(projectWork.Id);
            Database.Save();
        }

        public void ChangeScheduleDay(int scheduleId, int scheduleDayId)
        {
            var schedule = Database.Schedules.GetScheduleById(scheduleId);
            var day = Database.ScheduleDays.GetScheduleDayById(scheduleDayId);
            Database.Schedules.ChangeScheduleDay(schedule.Id, day.Id);
            Database.Save();
        }
    }
}
