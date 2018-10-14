using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using Repository.Interfaces;

namespace BLL.Services
{
    public class ProjectWorkService : IProjectWorkService
    {
        IUnitOfWork Database { get; set; }

        public ProjectWorkService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void ChangeEmployee(int? projectWorkId, int? newEmployeeId)
        {
            if (newEmployeeId == null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(newEmployeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует");
            if (projectWorkId == null)
                Console.WriteLine("не установлено id проектной работы");
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
                Console.WriteLine("проектной работы не существует");
            Database.ProjectWorks.ChangeEmployee(projectWorkId.Value, newEmployeeId.Value);
            Database.Save();
        }

        public void ChangeEmployeesProjectRole(int? projectWorkId, int? newProjectRoleId)
        {
            if (newProjectRoleId == null)
                Console.WriteLine("не установлено id роли");
            var projectRole = Database.ProjectRoles.GetProjectRoleById(newProjectRoleId.Value);
            if (projectRole == null)
                Console.WriteLine("роль не существует");
            if (projectWorkId == null)
                Console.WriteLine("не установлено id проектной работы");
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
                Console.WriteLine("проектной работы не существует");
            Database.ProjectWorks.ChangeEmployeesProjectRole(projectWorkId.Value, newProjectRoleId.Value);
            Database.Save();
        }

        public void ChangeProject(int? projectWorkId, int? newProjectId)
        {
            if (newProjectId == null)
                Console.WriteLine("не установлено id проекта");
            var project = Database.Projects.GetProjectById(newProjectId.Value);
            if (project == null)
                Console.WriteLine("проекта не существует");
            if (projectWorkId == null)
                Console.WriteLine("не установлено id проектной работы");
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
                Console.WriteLine("проектной работы не существует");
            Database.ProjectWorks.ChangeProject(projectWorkId.Value, newProjectId.Value);
            Database.Save();
        }

        public void ChangeWorkLoad(int? projectWorkId, int? newWorkLoad)
        {
            if (newWorkLoad == null)
                Console.WriteLine("не установлено значение загруженности сотрудника");
            if (projectWorkId == null)
                Console.WriteLine("не установлено id проектной работы");
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectWorkId.Value);
            if (projectWork == null)
                Console.WriteLine("проектной работы не существует");
            Database.ProjectWorks.ChangeWorkLoad(projectWorkId.Value, newWorkLoad.Value);
            Database.Save();
        }

        public void CreateProjectWork(ProjectWorkDTO item)
        {
            Employee employee = Database.Employees.GetEmployeeById(item.EmployeeId);
            if (employee == null)
                Console.WriteLine("сотрудник не найден");
            Project project = Database.Projects.GetProjectById(item.ProjectId);
            if (project == null)
                Console.WriteLine("проект не найден");
            ProjectRole projectRole = Database.ProjectRoles.GetProjectRoleById(item.ProjectRoleId);
            if (projectRole == null)
                Console.WriteLine("роль не найдена");
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
                    WorkLoad = item.WorkLoad
                };

                Database.ProjectWorks.CreateProjectWork(projectWork);
                Database.Save();
            }


        }

        public void DeleteEmployeeFromProject(int? projectId, int? employeeId)
        {
            if (projectId == null)
                Console.WriteLine("не установлено id проектной работы");
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectId.Value);
            if (projectWork == null)
                Console.WriteLine("проектной работы не существует");
            if (employeeId == null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудника не существует");
            Database.ProjectWorks.DeleteEmployeeFromProject(projectId.Value, employeeId.Value);
            Database.Save();
        }

        public void DeleteProjectWorkById(int? id)
        {
            if (id == null)
                Console.WriteLine("не установлено id проектной работы");
            var projectWork = Database.ProjectWorks.GetProjectWorkById(id.Value);
            if (projectWork == null)
                Console.WriteLine("проектной работы не существует");
            Database.ProjectWorks.DeleteProjectWorkById(id.Value);
            Database.Save();
        }

        public IEnumerable<ProjectWorkDTO> GetAllProjectWorks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectWork, ProjectWorkDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ProjectWork>, List<ProjectWorkDTO>>(Database.ProjectWorks.GetAllProjectWorks());
        }

        public IEnumerable<ProjectWorkDTO> GetEmployeesProjects(int? employeeId)
        {
            if (employeeId==null)
                Console.WriteLine("не установлено id сотрудника");
            var employee = Database.Employees.GetEmployeeById(employeeId.Value);
            if (employee == null)
                Console.WriteLine("сотрудник не найден");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectWork, ProjectWorkDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ProjectWork>, List<ProjectWorkDTO>>(Database.ProjectWorks.GetEmployeesProjects(employeeId.Value));
        }

        public IEnumerable<ProjectWorkDTO> GetNamesAndLoadOnProject(int? projectId)
        {
            if (projectId == null)
                Console.WriteLine("не установлено id проекта");
            var project = Database.Projects.GetProjectById(projectId.Value);
            if (project == null)
                Console.WriteLine("проект не найден");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectWork, ProjectWorkDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ProjectWork>, List<ProjectWorkDTO>>(Database.ProjectWorks.GetNamesAndLoadOnProject(projectId.Value));
        }

        public IEnumerable<(string name, string role)> GetNamesOnProject(int? projectId)
        {
            if (projectId == null)
                Console.WriteLine("не установлено id проектной работы");
            var projectWork = Database.ProjectWorks.GetProjectWorkById(projectId.Value);
            if (projectWork == null)
                Console.WriteLine("проектная работа не найден");
            return Database.ProjectWorks.GetNamesOnProject(projectId.Value);
        }

        public ProjectWorkDTO GetProjectWorkById(int? id)
        {
            if (id == null)
                Console.WriteLine("не установлено id проектной работы");
            var projectWork = Database.ProjectWorks.GetProjectWorkById(id.Value);
            if (projectWork == null)
                Console.WriteLine("проектная работа не найден");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectWork, ProjectWorkDTO>()).CreateMapper();
            return mapper.Map<ProjectWork, ProjectWorkDTO>(Database.ProjectWorks.GetProjectWorkById(id.Value));
        }
    }
}
