using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Mapping;
using DAL.Entities;
using Repository.Interfaces;

namespace BLL.Services
{
    public class ProjectWorkService : IProjectWorkService
    {
        IUnitOfWork Database { get; set; }
        Map<ProjectWork, ProjectWorkDTO> Map { get; set; }

        public ProjectWorkService(IUnitOfWork uow, Map<ProjectWork, ProjectWorkDTO> map)
        {
            Database = uow;
            Map = map;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void ChangeEmployee(int? projectWorkId, int? newEmployeeId)
        {
            if (newEmployeeId == null)
            {
                Console.WriteLine("не установлено id сотрудника");
                return;
            }
            var employee = Database.Employees.GetEmployeeById(newEmployeeId.Value);
            if (employee == null)
            {
                Console.WriteLine("сотрудника не существует");
                return;
            }
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
            Database.ProjectWorks.ChangeEmployee(projectWorkId.Value, newEmployeeId.Value);
            Database.Save();
        }

        public void ChangeEmployeesProjectRole(int? projectWorkId, int? newProjectRoleId)
        {
            if (newProjectRoleId == null)
            {
                Console.WriteLine("не установлено id роли");
                return;
            }
            var projectRole = Database.ProjectRoles.GetProjectRoleById(newProjectRoleId.Value);
            if (projectRole == null)
            {
                Console.WriteLine("роль не существует");
                return;
            }
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
            Database.ProjectWorks.ChangeEmployeesProjectRole(projectWorkId.Value, newProjectRoleId.Value);
            Database.Save();
        }

        public void ChangeProject(int? projectWorkId, int? newProjectId)
        {
            if (newProjectId == null)
            {
                Console.WriteLine("не установлено id проекта");
                return;
            }
            var project = Database.Projects.GetProjectById(newProjectId.Value);
            if (project == null)
            {
                Console.WriteLine("проекта не существует");
                return;
            }
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
            Database.ProjectWorks.ChangeProject(projectWorkId.Value, newProjectId.Value);
            Database.Save();
        }

        public void ChangeWorkLoad(int? projectWorkId, int? newWorkLoad)
        {
            if (newWorkLoad == null)
            {
                Console.WriteLine("не установлено значение загруженности сотрудника");
                return;
            }
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
            Database.ProjectWorks.ChangeWorkLoad(projectWorkId.Value, newWorkLoad.Value);
            Database.Save();
        }

        public void AddWorkLoad(int? projectWorkId, int? workLoad)
        {
            if (workLoad == null)
            {
                Console.WriteLine("не установлено значение загруженности сотрудника");
                return;
            }
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
            Employee employee = Database.Employees.GetEmployeeById(projectWork.EmployeeId);
            if (employee.PercentOrScheduleId == 3)
            {
                employee.PercentOrScheduleId = 1;
                employee.PercentOrSchedule = Database.WorkLoads.GetTypeById(1);
                Database.ProjectWorks.AddWorkLoad(projectWorkId.Value, workLoad.Value);
                Database.Save();
                return;
            }
            if (employee.PercentOrScheduleId == 2)
            {
                Console.WriteLine("добавьте расписание");
                return;
            }

            Database.ProjectWorks.AddWorkLoad(projectWorkId.Value, workLoad.Value);
            Database.Save();
        }

        public void DeleteWorkLoad(int? projectWorkId)
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
            Database.ProjectWorks.DeleteWorkLoad(projectWorkId.Value);
            Database.Save();
        }

        public void CreateProjectWork(ProjectWorkDTO item)
        {
            Employee employee = Database.Employees.GetEmployeeById(item.EmployeeId);
            if (employee == null)
            {
                Console.WriteLine("сотрудник не найден");
                return;
            }
            Project project = Database.Projects.GetProjectById(item.ProjectId);
            if (project == null)
            {
                Console.WriteLine("проект не найден");
                return;
            }
            ProjectRole projectRole = Database.ProjectRoles.GetProjectRoleById(item.ProjectRoleId);
            if (projectRole == null)
            {
                Console.WriteLine("роль не найдена");
                return;
            }
            ProjectWork temp = null;
            var works = Database.ProjectWorks.GetAllProjectWorks();
            foreach(var work in works)
            {
                if (work.EmployeeId == item.EmployeeId && work.ProjectId == item.ProjectId && work.ProjectRoleId == item.ProjectRoleId)
                {
                    temp = Database.ProjectWorks.GetProjectWorkById(work.Id);
                    Console.WriteLine("Сотрудник уже добавлен на проект");
                    break;
                }
            }
            if (temp == null)
            {
                ProjectWork projectWork = new ProjectWork
                {
                    EmployeeId = item.EmployeeId,
                    Employee = employee,
                    ProjectId = item.ProjectId,
                    Project = project,
                    ProjectRoleId = item.ProjectRoleId,
                    ProjectRole = projectRole,
                    WorkLoad = null
                };

                Database.ProjectWorks.CreateProjectWork(projectWork);
                Database.Save();
            }
            var empl = Database.Employees.GetEmployeeById(item.EmployeeId);
            if (empl.PercentOrScheduleId == 3) 
                Console.WriteLine("Добавьте процент загруженности или расписание");
            if (empl.PercentOrScheduleId == 1)
                Console.WriteLine("Добавьте процент загруженности");
            if (empl.PercentOrScheduleId == 2)
                Console.WriteLine("Добавьте расписание");
        }

        public void DeleteEmployeeFromProject(int? projectId, int? employeeId)
        {
            if (projectId == null)
            {
                Console.WriteLine("не установлено id проектной работы");
                return;
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectId.Value);
            if (projectWork == null)
            {
                Console.WriteLine("проектной работы не существует");
                return;
            }
            if (employeeId == null)
            {
                Console.WriteLine("не установлено id сотрудника");
                return;
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                Console.WriteLine("сотрудника не существует");
                return;
            }
            Database.ProjectWorks.DeleteEmployeeFromProject(projectId.Value, employeeId.Value);
            Database.Save();
        }

        public void DeleteProjectWorkById(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("не установлено id проектной работы");
                return;
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(id.Value);
            if (projectWork == null)
            {
                Console.WriteLine("проектной работы не существует");
                return;
            }
            Database.ProjectWorks.DeleteProjectWorkById(id.Value);
            Database.Save();
        }

        public IEnumerable<ProjectWorkDTO> GetAllProjectWorks()
        {
            var projectWorks = Database.ProjectWorks.GetAllProjectWorks();
            if (projectWorks.Count() == 0)
            {
                Console.WriteLine("проектные работы не найдены");
                return null;
            }
            return Map.ListMap(projectWorks);
        }

        public IEnumerable<ProjectWorkDTO> GetEmployeesProjects(int? employeeId)
        {
            if (employeeId == null)
            {
                Console.WriteLine("не установлено id сотрудника");
                return null;
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                Console.WriteLine("сотрудник не найден");
                return null;
            }
            var projectWorks = Database.ProjectWorks.GetEmployeesProjects(employeeId.Value);
            if (projectWorks.Count() == 0)
            {
                Console.WriteLine("у сотрудника нет проектов");
                return null;
            }
            return Map.ListMap(projectWorks);
        }

        public int CalculateEmployeesWorkload(int? employeeId)
        {
            if (employeeId == null)
            {
                Console.WriteLine("не установлено id сотрудника");
                return 0;
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                Console.WriteLine("сотрудник не найден");
                return 0;
            }
            var projectWorks = Database.ProjectWorks.GetEmployeesProjects(employeeId.Value);
            if (projectWorks.Count() == 0)
            {
                Console.WriteLine("у сотрудника нет проектов");
                return 0;
            }
            if (employee.PercentOrScheduleId == 2)
            {
                Console.WriteLine("сотрудник работает по расписанию");
                return 0;
            }
            return Database.ProjectWorks.CalculateEmployeesWorkload(employeeId.Value);
        }

        public IEnumerable<(string name, string role, string workload)> GetNamesAndLoadOnProject(int? projectId)
        {
            if (projectId == null)
            {
                Console.WriteLine("не установлено id проекта");
                return null;
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                Console.WriteLine("проект не найден");
                return null; 
            }
            var projectWorks = Database.ProjectWorks.FindProjectWork(item=>item.ProjectId==projectId.Value);
            if (projectWorks.Count() == 0)
            {
                Console.WriteLine("на проекте нет сотрудников");
                return null;
            }
            return Database.ProjectWorks.GetNamesAndLoadOnProject(projectId.Value); ;
        }

        public IEnumerable<(string name, string role)> GetNamesOnProject(int? projectId)
        {
            if (projectId == null)
            {
                Console.WriteLine("не установлено id проектной работы");
                return null;
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectId.Value);
            if (projectWork == null)
            {
                Console.WriteLine("проектная работа не найдена");
                return null;
            }
            var projectWorks = Database.ProjectWorks.GetNamesOnProject(projectId.Value);
            if (projectWorks.Count() == 0)
            {
                Console.WriteLine("на проекте нет сотрудников");
                return null;
            }
            return Database.ProjectWorks.GetNamesOnProject(projectId.Value);
        }

        public ProjectWorkDTO GetProjectWorkById(int? id)
        {
            if (id == null)
            {
                Console.WriteLine("не установлено id проектной работы");
                return null;
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(id.Value);
            if (projectWork == null)
            {
                Console.WriteLine("проектная работа не найден");
                return null;
            }
            return Map.ObjectMap(projectWork);
        }
    }
}
