using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectWorkService
    {
        IEnumerable<ProjectWorkDTO> GetAllProjectWorks();
        IEnumerable<(string name, string role)> GetNamesOnProject(int? projectId);
        IEnumerable<ProjectWorkDTO> GetNamesAndLoadOnProject(int? projectId);
        IEnumerable<ProjectWorkDTO> GetEmployeesProjects(int? employeeId);
        ProjectWorkDTO GetProjectWorkById(int? id);
        void CreateProjectWork(ProjectWorkDTO item);
        void DeleteProjectWorkById(int? id);
        void DeleteEmployeeFromProject(int? projectId, int? employeeId);
        void ChangeProject(int? projectWorkId, int? newProjectId);
        void ChangeEmployee(int? projectWorkId, int? newEmployeeId);
        void ChangeEmployeesProjectRole(int? projectWorkId, int? newProjectRoleId);
        void ChangeWorkLoad(int? projectWorkId, int? newWorkLoad);
        void Dispose();
    }
}
