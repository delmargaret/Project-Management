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
                Console.WriteLine("расписания не найдены");
                return null;
            }
            return Map.ListMap(schedules);
        }

        public IEnumerable<ScheduleDTO> GetScheduleOnProjectWork(int? projectWorkId)
        {
            if (projectWorkId == null)
            {
                Console.WriteLine("не указан id проектной работы");
                return null;
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
            {
                Console.WriteLine("проектная работа не найдена");
                return null;
            }
            var schedules = Database.Schedules.GetAllSchedules();
            if (schedules.Count() == 0)
            {
                Console.WriteLine("расписание сотрудника не найдено");
                return null;
            }
            return Map.ListMap(schedules);
        }

        public ScheduleDTO GetScheduleById(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("не указан id расписания");
                return null;
            }
            var schedule = Database.Schedules.GetScheduleById(id.Value);
            if (schedule == null) 
            {
                Console.WriteLine("расписание не найдено");
                return null;
            }
            return Map.ObjectMap(schedule);
        }

        public IEnumerable<ScheduleDayDTO> GetEmployeesFreeDays(int? employeeId)
        {
            if (employeeId == null)
            {
                Console.WriteLine("не указан id сотрудника");
                return null;
            }
            Employee employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                Console.WriteLine("сотрудник не найден");
                return null;
            }
            var freedays = Database.Schedules.GetEmployeesFreeDays(employeeId.Value);
            if (freedays.Count() == 0)
            {
                Console.WriteLine("свободных дней нет");
                return null;
            }
            return DayMap.ListMap(freedays);
        }

        public void CreateSchedule(ScheduleDTO item)
        {
            ProjectWork work = Database.ProjectWorks.GetProjectWorkById(item.ProjectWorkId);
            if (work == null)
            {
                Console.WriteLine("проектной работы не существует");
                return;
            }
            ScheduleDay day = Database.ScheduleDays.GetScheduleDayById(item.ScheduleDayId);
            if (day == null)
            {
                Console.WriteLine("день не найден");
                return;
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
                Console.WriteLine("добавьте процент загруженности");
                return;
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
                Console.WriteLine("не установлено id расписания");
                return;
            }
            var scedule = Database.Schedules.GetScheduleById(id.Value);
            if (scedule == null)
            {
                Console.WriteLine("расписание не существует");
                return;
            }
            Database.Schedules.DeleteScheduleById(id.Value);
            Database.Save();
        }

        public void DeleteScheduleByProjectWorkId(int? projectWorkId)
        {
            if (projectWorkId == null)
            {
                Console.WriteLine("не установлено id проектной работы");
                return;
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
            {
                Console.WriteLine("проектной работы не существует");
                return;
            }
            Database.Schedules.DeleteScheduleByProjectWorkId(projectWorkId.Value);
            Database.Save();
        }

        public void ChangeScheduleDay(int? scheduleId, int? scheduleDayId)
        {
            if (scheduleId == null)
            {
                Console.WriteLine("не установлено id расписания");
                return;
            }
            var schedule = Database.Schedules.GetScheduleById(scheduleId.Value);
            if (schedule == null)
            {
                Console.WriteLine("расписания не существует");
                return;
            }
            if (scheduleDayId == null)
            {
                Console.WriteLine("не установлено id дня");
                return;
            }
            var day = Database.ScheduleDays.GetScheduleDayById(scheduleDayId.Value);
            if (day == null)
            {
                Console.WriteLine("день не найден");
                return;
            }
            Database.Schedules.ChangeScheduleDay(scheduleId.Value, scheduleDayId.Value);
            Database.Save();
        }
    }
}
