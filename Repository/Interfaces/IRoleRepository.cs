using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetAll();
        Role Get(int id);
        IEnumerable<Role> Find(Func<Role, Boolean> predicate);
        void Create(Role item);
        void Update(Role item);
        void Delete(int id);
    }
}
