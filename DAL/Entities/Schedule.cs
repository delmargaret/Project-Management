using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public int ProjectWorkId { get; set; }
        public ProjectWork ProjectWork { get; set; }
        public int ScheduleDayId { get; set; }
        public ScheduleDay ScheduleDay { get; set; }
    }
}
