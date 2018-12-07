using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;


namespace Repository.Interfaces
{
    public interface ICredentialsRepository
    {
        IEnumerable<Credentials> GetAll();
        Credentials Get(int id);
        IEnumerable<Credentials> Find(Func<Credentials, Boolean> predicate);
        Credentials Create(Credentials item);
        void Update(Credentials item);
        Credentials GetCredentialsByLogin(string login);
        void Delete(int id);
    }
}
