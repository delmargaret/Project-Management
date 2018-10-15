using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IProjectWorkRepository
    {
        IEnumerable<ProjectWork> GetAllProjectWorks();
        IEnumerable<(string name, string role)> GetNamesOnProject(int projectId);
        IEnumerable<(string name, string role, string workload)> GetNamesAndLoadOnProject(int projectId); 
        IEnumerable<ProjectWork> GetEmployeesProjects(int employeeId);
        ProjectWork GetProjectWorkById(int id);
        IEnumerable<ProjectWork> FindProjectWork(Func<ProjectWork, Boolean> predicate);
        ProjectWork FindSameProjectWork(int projectId, int employeeId, int projectRoleId);
        int CalculateEmployeesWorkload(int employeeId);
        void CreateProjectWork(ProjectWork item);
        void UpdateProjectWork(ProjectWork item);
        void DeleteProjectWorkById(int id);
        void DeleteEmployeeFromProject(int projectId, int employeeId);
        void ChangeProject(int projectWorkId, int newProjectId);
        void ChangeEmployee(int projectWorkId, int newEmployeeId);
        void ChangeEmployeesProjectRole(int projectWorkId, int newProjectRoleId);
        void ChangeWorkLoad(int projectWorkId, int newWorkLoad);
        void AddWorkLoad(int projectWorkId, int workLoad);
        void DeleteWorkLoad(int projectWorkId);
    }
}
