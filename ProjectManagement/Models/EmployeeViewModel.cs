using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManagement.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeePatronymic { get; set; }
        public string Email { get; set; }
        public string GitLink { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}