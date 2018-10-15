using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ProjectWork
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int ProjectRoleId { get; set; }
        public ProjectRole ProjectRole { get; set; }
        public int? WorkLoad { get; set; }
    }
}
