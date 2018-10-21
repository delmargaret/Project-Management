using BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation
{
    public class ParticipationHistoryVlidator
    {
        public ParticipationHistoryVlidator() { }
        public List<ValidationResult> Validate(ParticipationHistoryDTO history)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (history.ProjectWorkId==0)
            {
                errors.Add(new ValidationResult("Не указан идентификатор участия в проекте", new List<string>() { "ProjectWorkId" }));
            }
            if (history.StartDate == null)
            {
                errors.Add(new ValidationResult("Не указана дата начала участия в проекте", new List<string>() { "StartDate" }));
            }
            if (history.EndDate == null)
            {
                errors.Add(new ValidationResult("Не указана дата окончания участия в проекте", new List<string>() { "EndDate" }));
            }
            if (history.EndDate < DateTimeOffset.Now)
            {
                errors.Add(new ValidationResult("Неверная дата", new List<string>() { "EndDate" }));
            }
            if (history.EndDate < history.StartDate)
            {
                errors.Add(new ValidationResult("Неверные даты", new List<string>() { "StartDate", "EndDate" }));
            }

            return errors;
        }
    }
}
