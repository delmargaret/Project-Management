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
    public class PasswordRepository : IPasswordRepository
    {
        private ManagementContext db;

        public PasswordRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<Password> GetAll()
        {
            return db.Passwords;
        }

        public Password Get(int id)
        {
            return db.Passwords.Find(id);
        }

        public void Create(Password password)
        {
            db.Passwords.Add(password);
        }

        public void Update(Password password)
        {
            db.Entry(password).State = EntityState.Modified;
        }

        public IEnumerable<Password> Find(Func<Password, Boolean> predicate)
        {
            return db.Passwords.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Password password = db.Passwords.Find(id);
            if (password != null)
                db.Passwords.Remove(password);
        }
    }
}
