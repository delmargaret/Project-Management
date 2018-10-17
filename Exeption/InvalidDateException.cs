using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption
{
    public class InvalidDateException : Exception
    {
        const string InvalidDateMessage = "Неверная дата";

        public InvalidDateException() : base(InvalidDateMessage) { }
    }
}
