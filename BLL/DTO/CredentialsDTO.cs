using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class CredentialsDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordString { get; set; }
        public int EmployeeId { get; set; }
    }
}
