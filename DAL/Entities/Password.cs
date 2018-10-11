using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Password
    {
        public int Id { get; set; }
        public string PasswordString { get; set; }
        public Employee Employee { get; set; }
    }
}
