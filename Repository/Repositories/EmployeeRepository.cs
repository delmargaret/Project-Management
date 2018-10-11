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
    public class EmployeeRepository : IEmployeeRepository
    {
        private ManagementContext db;

        public EmployeeRepository(ManagementContext context)
        {
            this.db = context;
        }

        public IEnumerable<Employee> GetAll()
        {
            return db.Employees;
        }

        public Employee Get(int id)
        {
            return db.Employees.Find(id);
        }

        public void Create(Employee employee)
        {
            db.Employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
        }

        public IEnumerable<Employee> Find(Func<Employee, Boolean> predicate)
        {
            return db.Employees.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
                db.Employees.Remove(employee);
        }
    }
}
