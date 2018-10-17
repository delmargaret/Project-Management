using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption
{
    public class EmptyFieldException : Exception
    {
        const string EmptyFieldMessage = "Введено пустое поле";

        public EmptyFieldException() : base(EmptyFieldMessage) { }
    }
}
