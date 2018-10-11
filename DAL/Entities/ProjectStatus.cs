using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProjectStatus
    {
        public int Id { get; set; }
        public string ProjectStatusName { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}
