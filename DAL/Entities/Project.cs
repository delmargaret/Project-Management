using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Недопустимая длина названия")]
        public string ProjectName { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(1024, MinimumLength = 2, ErrorMessage = "Недопустимая длина")]
        public string ProjectDescription { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTimeOffset ProjectStartDate { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTimeOffset ProjectEndDate { get; set; }
        [Range(1,2, ErrorMessage ="статус отсутствует")]
        public int ProjectStatusId { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
    }
}
