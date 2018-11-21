using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Credentials
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordString { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
