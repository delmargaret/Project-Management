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
using BLL.Infrastructure;

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
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(newEmployeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            if (projectWorkId == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            Database.ProjectWorks.ChangeEmployee(projectWorkId.Value, newEmployeeId.Value);
            Database.Save();
        }

        public void ChangeEmployeesProjectRole(int? projectWorkId, int? newProjectRoleId)
        {
            if (newProjectRoleId == null)
            {
                throw new ProjectException("Не установлен идентификатор роли в проекте");
            }
            var projectRole = Database.ProjectRoles.GetProjectRoleById(newProjectRoleId.Value);
            if (projectRole == null)
            {
                throw new ProjectException("Роль в проекте не найдена");
            }
            if (projectWorkId == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            Database.ProjectWorks.ChangeEmployeesProjectRole(projectWorkId.Value, newProjectRoleId.Value);
            Database.Save();
        }

        public void ChangeProject(int? projectWorkId, int? newProjectId)
        {
            if (newProjectId == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(newProjectId.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            if (projectWorkId == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            Database.ProjectWorks.ChangeProject(projectWorkId.Value, newProjectId.Value);
            Database.Save();
        }

        public void ChangeWorkLoad(int? projectWorkId, int? newWorkLoad)
        {
            if (newWorkLoad == null)
            {
                throw new ProjectException("Не установлено значение загруженности сотрудника");
            }
            if (projectWorkId == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            Database.ProjectWorks.ChangeWorkLoad(projectWorkId.Value, newWorkLoad.Value);
            Database.Save();
        }

        public void AddWorkLoad(int? projectWorkId, int? workLoad)
        {
            if (workLoad == null)
            {
                throw new ProjectException("Не установлено значение загруженности сотрудника");
            }
            if (projectWorkId == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
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
                throw new ProjectException("Доступно только расписание");
            }

            Database.ProjectWorks.AddWorkLoad(projectWorkId.Value, workLoad.Value);
            Database.Save();
        }

        public void DeleteWorkLoad(int? projectWorkId)
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
            Database.ProjectWorks.DeleteWorkLoad(projectWorkId.Value);
            Database.Save();
        }

        public void CreateProjectWork(ProjectWorkDTO item)
        {
            Employee employee = Database.Employees.GetEmployeeById(item.EmployeeId);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Project project = Database.Projects.GetProjectById(item.ProjectId);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            ProjectRole projectRole = Database.ProjectRoles.GetProjectRoleById(item.ProjectRoleId);
            if (projectRole == null)
            {
                throw new ProjectException("Роль в проекте не найдена");
            }
            ProjectWork temp = null;
            var works = Database.ProjectWorks.GetAllProjectWorks();
            foreach(var work in works)
            {
                if (work.EmployeeId == item.EmployeeId && work.ProjectId == item.ProjectId && work.ProjectRoleId == item.ProjectRoleId)
                {
                    temp = Database.ProjectWorks.GetProjectWorkById(work.Id);
                    throw new ProjectException("Сотрудник уже добавлен в проект");
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
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            Database.ProjectWorks.DeleteEmployeeFromProject(projectId.Value, employeeId.Value);
            Database.Save();
        }

        public void DeleteProjectWorkById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(id.Value);
            if (projectWork == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            Database.ProjectWorks.DeleteProjectWorkById(id.Value);
            Database.Save();
        }

        public IEnumerable<ProjectWorkDTO> GetAllProjectWorks()
        {
            var projectWorks = Database.ProjectWorks.GetAllProjectWorks();
            if (projectWorks.Count() == 0)
            {
                throw new ProjectException("Участия в проекте не найдены");
            }
            return Map.ListMap(projectWorks);
        }

        public IEnumerable<ProjectWorkDTO> GetEmployeesProjects(int? employeeId)
        {
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            var projectWorks = Database.ProjectWorks.GetEmployeesProjects(employeeId.Value);
            if (projectWorks.Count() == 0)
            {
                throw new ProjectException("Проекты не найдены");
            }
            return Map.ListMap(projectWorks);
        }

        public int CalculateEmployeesWorkload(int? employeeId)
        {
            if (employeeId == null)
            {
                throw new ProjectException("Не установлен идентификатор сотрудника");
            }
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
            {
                throw new ProjectException("Сотрудник не найден");
            }
            var projectWorks = Database.ProjectWorks.GetEmployeesProjects(employeeId.Value);
            if (projectWorks.Count() == 0)
            {
                throw new ProjectException("Проекты не найдены");
            }
            if (employee.PercentOrScheduleId == 2)
            {
                throw new ProjectException("Доступно только расписание");
            }
            return Database.ProjectWorks.CalculateEmployeesWorkload(employeeId.Value);
        }

        public IEnumerable<(string name, string role, string workload)> GetNamesAndLoadOnProject(int? projectId)
        {
            if (projectId == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                throw new ProjectException("Проект не найден");
            }
            var projectWorks = Database.ProjectWorks.FindProjectWork(item=>item.ProjectId==projectId.Value);
            if (projectWorks.Count() == 0)
            {
                throw new ProjectException("Сотрудники на проекте не найдены");
            }
            return Database.ProjectWorks.GetNamesAndLoadOnProject(projectId.Value); ;
        }

        public IEnumerable<(string name, string role)> GetNamesOnProject(int? projectId)
        {
            if (projectId == null)
            {
                throw new ProjectException("Не установлен идентификатор проекта");
            }
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
            {
                throw new ProjectException("Проек не найден");
            }
            var projectWorks = Database.ProjectWorks.GetNamesOnProject(projectId.Value);
            if (projectWorks.Count() == 0)
            {
                throw new ProjectException("Сотрудники на проекте не найдены");
            }
            return Database.ProjectWorks.GetNamesOnProject(projectId.Value);
        }

        public ProjectWorkDTO GetProjectWorkById(int? id)
        {
            if (id == null)
            {
                throw new ProjectException("Не установлен идентификатор участия в проекте");
            }
            var projectWork = Database.ProjectWorks.GetProjectWorkById(id.Value);
            if (projectWork == null)
            {
                throw new ProjectException("Участие в проекте не найдено");
            }
            return Map.ObjectMap(projectWork);
        }
    }
}
