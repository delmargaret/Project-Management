using BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation
{
    public class ScheduleValidator
    {
        public ScheduleValidator() { }
        public List<ValidationResult> Validate(ScheduleDTO schedule)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (schedule.ProjectWorkId == 0)
            {
                errors.Add(new ValidationResult("Не указан идентификатор участия в проекте", new List<string>() { "ProjectWorkId" }));
            }
            if (schedule.ScheduleDayId == 0)
            {
                errors.Add(new ValidationResult("Не указан идентификатор дня недели", new List<string>() { "ScheduleDayId" }));
            }
            if (schedule.ScheduleDayId != 0 && (schedule.ScheduleDayId > 6 || schedule.ScheduleDayId < 1))
            {
                errors.Add(new ValidationResult("День не существует", new List<string>() { "ScheduleDayId" }));
            }

            return errors;
        }
    }
}
