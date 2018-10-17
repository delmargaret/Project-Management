using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.ProjectRoleExceptions
{
    public class ProjectRoleIdNotSetException : Exception
    {
        const string ProjectRoleIdNotSetMessage = "Не установлен идентификатор роли на проекте";

        public ProjectRoleIdNotSetException() : base(ProjectRoleIdNotSetMessage) { }
    }
}
