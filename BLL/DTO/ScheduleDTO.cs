using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public int ProjectWorkId { get; set; }
        public int ScheduleDayId { get; set; }
    }
}
