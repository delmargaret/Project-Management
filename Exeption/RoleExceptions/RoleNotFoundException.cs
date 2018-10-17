using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.RoleExceptions
{
    public class RoleNotFoundException : Exception
    {
        const string RoleNotFoundMessage = "Роль не найдена";

        public RoleNotFoundException() : base(RoleNotFoundMessage) { }
    }
}
