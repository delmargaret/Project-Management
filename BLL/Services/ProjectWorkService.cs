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
using Exeption;

namespace BLL.Services
{
    public class ProjectWorkService : IProjectWorkService
    {
        IUnitOfWork Database { get; set; }
        Map<ProjectWork, ProjectWorkDTO> Map = new Map<ProjectWork, ProjectWorkDTO>();

        public ProjectWorkService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void ChangeEmployee(int projectWorkId, int newEmployeeId)
        {
            var employee = Database.Employees.GetEmployeeById(newEmployeeId);
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            Database.ProjectWorks.ChangeEmployee(projectWork.Id, employee.Id);
            Database.Save();
        }

        public void ChangeEmployeesProjectRole(int projectWorkId, int newProjectRoleId)
        {
            var projectRole = Database.ProjectRoles.GetProjectRoleById(newProjectRoleId);
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            Database.ProjectWorks.ChangeEmployeesProjectRole(projectWork.Id, projectRole.Id);
            Database.Save();
        }

        public void ChangeProject(int projectWorkId, int newProjectId)
        {
            var project = Database.Projects.GetProjectById(newProjectId);
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            Database.ProjectWorks.ChangeProject(projectWork.Id, project.Id);
            Database.Save();
        }

        public void ChangeWorkLoad(int projectWorkId, int newWorkLoad)
        {
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            Database.ProjectWorks.ChangeWorkLoad(projectWork.Id, newWorkLoad);
            Database.Save();
        }

        public int GetWorkLoadType(int projectWorkId)
        {
            var work = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            var employeeId = work.EmployeeId;
            var type = Database.Employees.GetEmployeeById(employeeId).PercentOrScheduleId;
            return type;
        }

        public void AddWorkLoad(int projectWorkId, int workLoad)
        {
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            Employee employee = Database.Employees.GetEmployeeById(projectWork.EmployeeId);
            if (employee.PercentOrScheduleId == 3)
            {
                employee.PercentOrScheduleId = 1;
                employee.PercentOrSchedule = Database.WorkLoads.GetTypeById(1);
                Database.ProjectWorks.AddWorkLoad(projectWork.Id, workLoad);
                Database.Save();
                return;
            }
            if (employee.PercentOrScheduleId == 2)
            {
                throw new PercentOrScheduleException();
            }
            Database.ProjectWorks.AddWorkLoad(projectWork.Id, workLoad);
            Database.Save();
        }
        
        public void DeleteWorkLoad(int projectWorkId)
        {
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            Database.ProjectWorks.DeleteWorkLoad(projectWork.Id);
            Database.Save();
        }

        public ProjectWorkDTO CreateProjectWork(ProjectWorkDTO item)
        {
            Employee employee = Database.Employees.GetEmployeeById(item.EmployeeId);
            Project project = Database.Projects.GetProjectById(item.ProjectId);
            ProjectRole projectRole = Database.ProjectRoles.GetProjectRoleById(item.ProjectRoleId);
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

            var pw = Database.ProjectWorks.CreateProjectWork(projectWork);
            Database.Save();

            var empl = Database.Employees.GetEmployeeById(item.EmployeeId);
            if (empl.PercentOrScheduleId == 3) 
                Console.WriteLine("Добавьте процент загруженности или расписание");
            if (empl.PercentOrScheduleId == 1)
                Console.WriteLine("Добавьте процент загруженности");
            if (empl.PercentOrScheduleId == 2)
                Console.WriteLine("Добавьте расписание");
            return Map.ObjectMap(pw);
        }

        public void DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            var project = Database.Projects.GetProjectById(projectId);
            var employee = Database.Employees.GetEmployeeById(employeeId);
            Database.ProjectWorks.DeleteEmployeeFromProject(project.Id, employee.Id);
            Database.Save();
        }

        public void DeleteProjectWorkById(int id)
        {
            Database.ProjectWorks.DeleteProjectWorkById(id);
            Database.Save();
        }

        public IEnumerable<ProjectWorkDTO> GetAllProjectWorks()
        {
            var projectWorks = Database.ProjectWorks.GetAllProjectWorks();
            return Map.ListMap(projectWorks);
        }

        public IEnumerable<ProjectWorkDTO> GetEmployeesProjects(int employeeId)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            var projectWorks = Database.ProjectWorks.GetEmployeesProjects(employee.Id);
            return Map.ListMap(projectWorks);
        }

        public IEnumerable<ProjectWorkDTO> GetEmployeesOnProject(int projectId)
        {
            var project = Database.Projects.GetProjectById(projectId);
            var projectWorks = Database.ProjectWorks.GetEmployeesOnProject(project.Id);
            return Map.ListMap(projectWorks);
        }

        public int CalculateEmployeesWorkload(int employeeId)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            var projectWorks = Database.ProjectWorks.GetEmployeesProjects(employee.Id);
            if (employee.PercentOrScheduleId == 2)
            {
                throw new PercentOrScheduleException();
            }
            return Database.ProjectWorks.CalculateEmployeesWorkload(employee.Id);
        }

        public IEnumerable<(int id, string name, string role, string workload)> GetNamesAndLoadOnProject(int projectId)
        {
            var project = Database.Projects.GetProjectById(projectId);
            var projectWorks = Database.ProjectWorks.FindProjectWork(item=>item.ProjectId==projectId);
            return Database.ProjectWorks.GetNamesAndLoadOnProject(project.Id); 
        }

        public IEnumerable<(int id, string name, string role)> GetNamesOnProject(int projectId)
        {
            var project = Database.Projects.GetProjectById(projectId);
            var projectWorks = Database.ProjectWorks.GetNamesOnProject(project.Id);
            return projectWorks;
        }

        public ProjectWorkDTO GetProjectWorkById(int id)
        {
            var projectWork = Database.ProjectWorks.GetProjectWorkById(id);
            return Map.ObjectMap(projectWork);
        }
    }
}
