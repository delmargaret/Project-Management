using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.EmployeeExeptions
{
    public class EmployeeIdNotSetException : Exception
    {
        const string EmployeeIdNotSetMessage = "Не установлен идентификатор сотрудника";

        public EmployeeIdNotSetException() : base(EmployeeIdNotSetMessage) { }
    }
}
