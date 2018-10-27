using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;


namespace Repository.Interfaces
{
    public interface IPasswordRepository
    {
        IEnumerable<Password> GetAll();
        Password Get(int id);
        IEnumerable<Password> Find(Func<Password, Boolean> predicate);
        Password Create(Password item);
        void Update(Password item);
        void Delete(int id);
    }
}
