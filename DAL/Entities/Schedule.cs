using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int ProjectWorkId { get; set; }
        public ProjectWork ProjectWork { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1,6,ErrorMessage ="Дня не существует")]
        public int ScheduleDayId { get; set; }
        public ScheduleDay ScheduleDay { get; set; }
    }
}
