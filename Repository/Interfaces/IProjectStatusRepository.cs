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
        IEnumerable<ProjectStatus> GetAll();
        ProjectStatus Get(int id);
        IEnumerable<ProjectStatus> Find(Func<ProjectStatus, Boolean> predicate);
        void Create(ProjectStatus item);
        void Update(ProjectStatus item);
        void Delete(int id);
    }
}
