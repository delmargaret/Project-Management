using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Недопустимая длина имени")]
        public string EmployeeName { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Недопустимая длина фамилии")]
        public string EmployeeSurname { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Недопустимая длина отчества")]
        public string EmployeePatronymic { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [EmailAddress(ErrorMessage = "Неправильно введен e-mail")]
        public string Email { get; set; }
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Неверная ссылка")]
        public string GitLink { get; set; }
        [RegularExpression(@"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$", ErrorMessage = "Неверно введен номер")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        [Range(1, 3, ErrorMessage ="Роли не существует")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int PercentOrScheduleId { get; set; }
        public PercentOrSchedule PercentOrSchedule { get; set; }
    }
}
