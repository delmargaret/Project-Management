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
        IEnumerable<ProjectRole> GetAll();
        ProjectRole Get(int id);
        IEnumerable<ProjectRole> Find(Func<ProjectRole, Boolean> predicate);
        void Create(ProjectRole item);
        void Update(ProjectRole item);
        void Delete(int id);
    }
}
