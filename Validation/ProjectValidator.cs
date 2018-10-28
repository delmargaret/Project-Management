using BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation
{
    public class ProjectValidator
    {
        public ProjectValidator() { }
        public List<ValidationResult> Validate(ProjectDTO project)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(project.ProjectName))
            {
                errors.Add(new ValidationResult("Введите название проекта", new List<string>() { "ProjectName" }));
            }
            if (project.ProjectName.Length > 20 || project.ProjectName.Length < 2)
            {
                errors.Add(new ValidationResult("Недопустимая длина названия проекта", new List<string>() { "ProjectName" }));
            }
            if (string.IsNullOrWhiteSpace(project.ProjectDescription))
            {
                errors.Add(new ValidationResult("Введите описание проекта", new List<string>() { "ProjectDescription" }));
            }
            if (project.ProjectDescription.Length > 1024 || project.ProjectDescription.Length < 10)
            {
                errors.Add(new ValidationResult("Недопустимая длина описания проекта", new List<string>() { "ProjectDescription" }));
            }
            if (project.ProjectStartDate == null)
            {
                errors.Add(new ValidationResult("Не указана дата начала проекта", new List<string>() { "ProjectStartDate" }));
            }
            if (project.ProjectEndDate == null)
            {
                errors.Add(new ValidationResult("Не указана дата окончания проекта", new List<string>() { "ProjectEndDate" }));
            }
            if (project.ProjectEndDate < DateTimeOffset.Now)
            {
                errors.Add(new ValidationResult("Неверная дата", new List<string>() { "ProjectEndDate" }));
            }
            if (project.ProjectEndDate < project.ProjectStartDate)
            {
                errors.Add(new ValidationResult("Неверная дата", new List<string>() { "ProjectStartDate", "ProjectEndDate" }));
            }
            if (project.ProjectStatusId != 0 && (project.ProjectStatusId > 2 || project.ProjectStatusId < 1))
            {
                errors.Add(new ValidationResult("Статуса проекта не существует", new List<string>() { "ProjectStatusId" }));
            }

            return errors;
        }
    }
}
