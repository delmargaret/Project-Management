using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeePatronymic { get; set; }
        public string Email { get; set; }
        public string GitLink { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
        public ICollection<ProjectWork> Projects { get; set; }
    }
}
