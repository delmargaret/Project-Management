using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProjectWork
    {
        public int Id { get; set; }
        public Project Project { get; set; }
        public Employee Employee { get; set; }
        public ProjectRole ProjectRole { get; set; }
        public int WorkLoad { get; set; }
    }
}
