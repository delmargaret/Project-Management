using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.RoleExceptions
{
    public class RoleIdNotSetException : Exception
    {
        const string RoleIdNotSetMessage = "Не установлен идентификатор роли";

        public RoleIdNotSetException() : base(RoleIdNotSetMessage) { }
    }
}
