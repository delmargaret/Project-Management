using BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation
{
    public class ProjectWorkValidator
    {
        public ProjectWorkValidator() { }
        public List<ValidationResult> Validate(ProjectWorkDTO work)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (work.ProjectId == 0)
            {
                errors.Add(new ValidationResult("Не указан идентификатор проекта", new List<string>() { "ProjectId" }));
            }
            if (work.EmployeeId == 0)
            {
                errors.Add(new ValidationResult("Не указан идентификатор сотрудника", new List<string>() { "EmployeeId" }));
            }
            if (work.ProjectRoleId == 0)
            {
                errors.Add(new ValidationResult("Не указан идентификатор роли в проекте", new List<string>() { "ProjectRoleId" }));
            }

            return errors;
        }
    }
}
