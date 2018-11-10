using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAllProjects();
        IEnumerable<Project> GetAllProjectsByStatusId(int projectStatusId);
        IEnumerable<Project> GetProjectsEndingInNDays(int numberOfDays);
        Project GetProjectById(int id);
        IEnumerable<Project> FindProject(Func<Project, Boolean> predicate);
        Project CreateProject(Project item);
        void FindSameProject(string projName, string projDescr, DateTimeOffset start, DateTimeOffset end, int projStatus);
        void UpdateProject(Project item);
        void DeleteProjectById(int id);
        void ChangeProjectName(int projectId, string newProjectName);
        void ChangeProjectDescription(int projectId, string newProjectDescription);
        void ChangeProjectStartDate(int projectId, DateTimeOffset newStartDate);
        void ChangeProjectEndDate(int projectId, DateTimeOffset newEndDate);
        void ChangeProjectStatus(int projectId, int projectStatusId);
        void CloseProject(int projectId);
    }
}
