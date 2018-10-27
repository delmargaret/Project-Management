using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IProjectStatusRepository
    {
        IEnumerable<ProjectStatus> GetAllProjectStatuses();
        ProjectStatus GetProjectStatusById(int id);
        IEnumerable<ProjectStatus> FindProjectStatus(Func<ProjectStatus, Boolean> predicate);
        ProjectStatus CreateProjectStatus(ProjectStatus item);
        void UpdateProjectStatus(ProjectStatus item);
        void DeleteProjectStatus(int id);
    }
}