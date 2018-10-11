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
        IEnumerable<Project> GetAll();
        Project Get(int id);
        IEnumerable<Project> Find(Func<Project, Boolean> predicate);
        void Create(Project item);
        void Update(Project item);
        void Delete(int id);
    }
}
