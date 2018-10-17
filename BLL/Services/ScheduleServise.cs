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
using BLL.Infrastructure;

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
            if (schedules.Count() == 0)
            {
                throw new ProjectException("Расписания не найдены");
            }
            return Map.ListMap(schedules);
        }

        public IEnumerable<ScheduleDTO> GetScheduleOnProjectWork(int? projectWorkId)
        {
            if (projectWorkId == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            var schedules = Database.Schedules.GetAllSchedules();
            if (schedules.Count() == 0)
            {
                throw new ProjectException("Расписание сотрудника");
            }
            return Map.ListMap(schedules);
        }

        public ScheduleDTO GetScheduleById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор расписания");
            }
            var schedule = Database.Schedules.GetScheduleById(id.Value);
            if (schedule == null) 
            {
                throw new ProjectException("Расписание не найдено");
            }
            return Map.ObjectMap(schedule);
        }

        public IEnumerable<ScheduleDayDTO> GetEmployeesFreeDays(int? employeeId)
        {
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            Employee employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            var freedays = Database.Schedules.GetEmployeesFreeDays(employeeId.Value);
            if (freedays.Count() == 0)
            {
                throw new ProjectException("Свободных дней нет");
            }
            return DayMap.ListMap(freedays);
        }

        public void CreateSchedule(ScheduleDTO item)
        {
            ProjectWork work = Database.ProjectWorks.GetProjectWorkById(item.ProjectWorkId);
            if (work == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            ScheduleDay day = Database.ScheduleDays.GetScheduleDayById(item.ScheduleDayId);
            if (day == null)
            {
                throw new ProjectException("День не найден");
            }
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
                throw new ProjectException("Доступен только процент загруженности");
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

        public void DeleteScheduleById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор расписания");
            }
            var scedule = Database.Schedules.GetScheduleById(id.Value);
            if (scedule == null)
            {
                throw new ProjectException("Расписание не найден");
            }
            Database.Schedules.DeleteScheduleById(id.Value);
            Database.Save();
        }

        public void DeleteScheduleByProjectWorkId(int? projectWorkId)
        {
            if (projectWorkId == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            Database.Schedules.DeleteScheduleByProjectWorkId(projectWorkId.Value);
            Database.Save();
        }

        public void ChangeScheduleDay(int? scheduleId, int? scheduleDayId)
        {
            if (scheduleId == null)
            {
                throw new ProjectException("Не установлен идентификатор расписания");
            }
            var schedule = Database.Schedules.GetScheduleById(scheduleId.Value);
            if (schedule == null)
            {
                throw new ProjectException("Расписание не найдено");
            }
            if (scheduleDayId == null)
            {
                throw new ProjectException("Не установлен идентификатор дня");
            }
            var day = Database.ScheduleDays.GetScheduleDayById(scheduleDayId.Value);
            if (day == null)
            {
                throw new ProjectException("День не найден");
            }
            Database.Schedules.ChangeScheduleDay(scheduleId.Value, scheduleDayId.Value);
            Database.Save();
        }
    }
}
