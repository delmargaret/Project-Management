using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace Repository.Interfaces
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee Get(int id);
        IEnumerable<Employee> Find(Func<Employee, Boolean> predicate);
        void Create(Employee item);
        void Update(Employee item);
        void Delete(int id);
    }
}
