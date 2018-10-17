using BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Validation
{
    public class ValidateObject 
    {

        public ValidateObject() { }
        public List<ValidationResult> ValidateEmployee(EmployeeDTO employee)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(employee.EmployeeName))
            {
                throw new ValidationException("Введите имя сотрудника");
            }
            if (employee.EmployeeName.Length > 20 || employee.EmployeeName.Length < 2)
            {
                throw new ValidationException("Недопустимая длина имени");
            }
            if (string.IsNullOrWhiteSpace(employee.EmployeeSurname))
            {
                throw new ValidationException("Введите фамилию сотрудника");
            }
            if (employee.EmployeeSurname.Length > 20 || employee.EmployeeSurname.Length < 2)
            {
                throw new ValidationException("Недопустимая длина фамилии");
            }
            if (string.IsNullOrWhiteSpace(employee.EmployeePatronymic))
            {
                throw new ValidationException("Введите отчество сотрудника");
            }
            if (employee.EmployeePatronymic.Length > 20 || employee.EmployeePatronymic.Length < 2)
            {
                throw new ValidationException("Недопустимая длина отчества");
            }
            if (string.IsNullOrWhiteSpace(employee.Email))
            {
                throw new ValidationException("Введите e-mai сотрудника");
            }
            if (!(new System.ComponentModel.DataAnnotations.EmailAddressAttribute()).IsValid(employee.Email))
            {
                errors.Add(new ValidationResult("Неверный e-mail"));
            }
            if (employee.GitLink!=null && (employee.GitLink.Length > 30 || employee.GitLink.Length < 2))
            {
                throw new ValidationException("Недопустимая длина ссылки");
            }
            if (string.IsNullOrWhiteSpace(employee.RoleId.ToString()))
            {
                throw new ValidationException("Не указан идентификатор роли");
            }
            if (employee.RoleId > 3 || employee.RoleId < 1)
            {
                errors.Add(new ValidationResult("Роль не существует"));
            }
            if(employee.PhoneNumber != null && !Regex.IsMatch(employee.PhoneNumber, @"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$"))
            {
                throw new ValidationException("Неверно введен номер");
            }
            return errors;
        }
    }
}
