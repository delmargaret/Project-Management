using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.RoleExceptions
{
    public class RolesNotFoundException : Exception
    {
        const string RolesNotFoundMessage = "Роль не найдена";

        public RolesNotFoundException() : base(RolesNotFoundMessage) { }
    }
}
