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
    public class EmployeeValidator 
    {

        public EmployeeValidator() { }
        public List<ValidationResult> Validate(EmployeeDTO employee)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(employee.EmployeeName))
            {
                errors.Add(new ValidationResult("Введите имя сотрудника", new List<string>() { "EmployeeName" }));
            }
            if (employee.EmployeeName.Length > 20 || employee.EmployeeName.Length < 2)
            {
                errors.Add(new ValidationResult("Недопустимая длина имени", new List<string>() { "EmployeeName" }));
            }
            if (string.IsNullOrWhiteSpace(employee.EmployeeSurname))
            {
                errors.Add(new ValidationResult("Введите фамилию сотрудника", new List<string>() { "EmployeeSurname" }));
            }
            if (employee.EmployeeSurname.Length > 20 || employee.EmployeeSurname.Length < 2)
            {
                errors.Add(new ValidationResult("Недопустимая длина фамилии", new List<string>() { "EmployeeSurname" }));
            }
            if (string.IsNullOrWhiteSpace(employee.EmployeePatronymic))
            {
                errors.Add(new ValidationResult("Введите отчество сотрудника", new List<string>() { "EmployeePatronymic" }));
            }
            if (employee.EmployeePatronymic.Length > 20 || employee.EmployeePatronymic.Length < 2)
            {
                errors.Add(new ValidationResult("Недопустимая длина отчества", new List<string>() { "EmployeePatronymic" }));
            }
            if (string.IsNullOrWhiteSpace(employee.Email))
            {
                errors.Add(new ValidationResult("Введите e-mai сотрудника", new List<string>() { "Email" }));
            }
            if (!(new EmailAddressAttribute()).IsValid(employee.Email))
            {
                errors.Add(new ValidationResult("Неверный e-mail", new List<string>() { "Email" }));
            }
            if (employee.GitLink!=null && (employee.GitLink.Length > 30 || employee.GitLink.Length < 2))
            {
                errors.Add(new ValidationResult("Недопустимая длина ссылки", new List<string>() { "GitLink" }));
            }
            if (employee.RoleId == 0)
            {
                errors.Add(new ValidationResult("Не указан идентификатор роли", new List<string>() { "RoleId" }));
            }
            if (employee.RoleId != 0 && (employee.RoleId > 3 || employee.RoleId < 1))
            {
                errors.Add(new ValidationResult("Роль не существует", new List<string>() { "RoleId" }));
            }
            if(employee.PhoneNumber != null && !Regex.IsMatch(employee.PhoneNumber, @"^(\+375|80)(29|25|44|33)(\d{3})(\d{2})(\d{2})$"))
            {
                errors.Add(new ValidationResult("Неверно введен номер", new List<string>() { "PhoneNumber" }));
            }
            return errors;
        }
    }
}
