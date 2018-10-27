using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IProjectRoleRepository
    {
        IEnumerable<ProjectRole> GetAllProjectRoles();
        ProjectRole GetProjectRoleById(int id);
        IEnumerable<ProjectRole> FindProjectRole(Func<ProjectRole, Boolean> predicate);
        ProjectRole CreateProjectRole(ProjectRole item);
        void UpdateProjectRole(ProjectRole item);
        void DeleteProjectRole(int id);
        void ChangeProjectRoleName(int projectRoleId, string projectRoleName);
    }
}
