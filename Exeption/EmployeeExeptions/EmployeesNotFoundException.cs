using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.EmployeeExeptions
{
    public class EmployeesNotFoundException : Exception
    {
        const string EmployeesNotFoundMessage = "Сотрудники не найдены";

        public EmployeesNotFoundException() : base(EmployeesNotFoundMessage) { }
    }
}
