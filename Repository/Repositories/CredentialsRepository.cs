using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.DataContext;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class CredentialsRepository : ICredentialsRepository
    {
        private ManagementContext db;

        public CredentialsRepository(ManagementContext context)
        {
            this.db = context;
        }

        public Credentials GetLastPassword(string login)
        {
            var creds = db.Credentials.Where(item => item.Login == login).ToList();
            return creds.LastOrDefault();
        }

        public IEnumerable<Credentials> GetAll()
        {
            return db.Credentials;
        }

        public Credentials Get(int id)
        {
            return db.Credentials.Find(id);
        }

        public Credentials Create(Credentials password)
        {
            var pw = db.Credentials.Add(password);
            return pw;
        }

        public void Update(Credentials password)
        {
            db.Entry(password).State = EntityState.Modified;
        }

        public IEnumerable<Credentials> Find(Func<Credentials, Boolean> predicate)
        {
            return db.Credentials.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Credentials password = db.Credentials.Find(id);
            if (password != null)
                db.Credentials.Remove(password);
        }
    }
}
