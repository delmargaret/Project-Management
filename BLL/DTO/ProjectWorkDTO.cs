using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ProjectWorkDTO
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public int ProjectRoleId { get; set; }
        public int? WorkLoad { get; set; }
    }
}
