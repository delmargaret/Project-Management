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
            Database.ProjectWorks.ChangeEmployee(projectWorkId, newEmployeeId);
            Database.Save();
        }

        public void ChangeEmployeesProjectRole(int projectWorkId, int newProjectRoleId)
        {
            Database.ProjectWorks.ChangeEmployeesProjectRole(projectWorkId, newProjectRoleId);
            Database.Save();
        }

        public void ChangeProject(int projectWorkId, int newProjectId)
        {
            Database.ProjectWorks.ChangeProject(projectWorkId, newProjectId);
            Database.Save();
        }

        public void ChangeWorkLoad(int projectWorkId, int newWorkLoad)
        {
            Database.ProjectWorks.ChangeWorkLoad(projectWorkId, newWorkLoad);
            Database.Save();
        }

        public int GetWorkLoadType(int projectWorkId)
        {
            var work = Database.ProjectWorks.GetProjectWorkById(projectWorkId);
            var employeeId = work.EmployeeId;
            var type = Database.Employees.GetEmployeeById(employeeId).PercentOrScheduleId;
            return type;
        }

        public string GetWorkload(int employeeId)
        {
            return Database.ProjectWorks.GetWorkload(employeeId);
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
            Database.ProjectWorks.DeleteWorkLoad(projectWorkId);
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
            return Map.ObjectMap(pw);
        }

        public void DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            Database.ProjectWorks.DeleteEmployeeFromProject(projectId, employeeId);
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

        public IEnumerable<(int id, int projectId, string projectNname, string role, string workload, string history)> GetEmployeesProjects(int employeeId)
        {
            return Database.ProjectWorks.GetEmployeesProjectsAndLoad(employeeId);
        }

        public IEnumerable<ProjectWorkDTO> GetEmployeesOnProject(int projectId)
        {
            var projectWorks = Database.ProjectWorks.GetEmployeesOnProject(projectId);
            return Map.ListMap(projectWorks);
        }

        public int CalculateEmployeesWorkload(int employeeId)
        {
            var employee = Database.Employees.GetEmployeeById(employeeId);
            if (employee.PercentOrScheduleId == 2)
            {
                throw new PercentOrScheduleException();
            }
            return Database.ProjectWorks.CalculateEmployeesWorkload(employee.Id);
        }

        public IEnumerable<(int id, int employeeId, string name, string role, string workload, string history)> GetNamesAndLoadOnProject(int projectId)
        {
            return Database.ProjectWorks.GetNamesAndLoadOnProject(projectId);
        }

        public IEnumerable<(int id, string name, string role)> GetNamesOnProject(int projectId)
        {
            return Database.ProjectWorks.GetNamesOnProject(projectId);
        }

        public ProjectWorkDTO GetProjectWorkById(int id)
        {
            var projectWork = Database.ProjectWorks.GetProjectWorkById(id);
            return Map.ObjectMap(projectWork);
        }
    }
}
