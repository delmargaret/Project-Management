using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ParticipationHistory
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int ProjectWorkId { get; set; }
        public ProjectWork ProjectWork { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTimeOffset StartDate { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTimeOffset EndDate { get; set; }
    }
}
