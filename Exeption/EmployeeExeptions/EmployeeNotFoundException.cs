using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.EmployeeExeptions
{
    public class EmployeeNotFoundException : Exception
    {
        const string EmployeeNotFoundMessage = "Сотрудник не найден";

        public EmployeeNotFoundException() : base(EmployeeNotFoundMessage) { }
    }
}
