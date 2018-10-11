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
        IEnumerable<ProjectWork> GetAll();
        ProjectWork Get(int id);
        IEnumerable<ProjectWork> Find(Func<ProjectWork, Boolean> predicate);
        void Create(ProjectWork item);
        void Update(ProjectWork item);
        void Delete(int id);
    }
}
