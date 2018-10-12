using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public DateTimeOffset ProjectStartDate { get; set; }
        public DateTimeOffset ProjectEndDate { get; set; }
        public int ProjectStatusId { get; set; }
    }
}
