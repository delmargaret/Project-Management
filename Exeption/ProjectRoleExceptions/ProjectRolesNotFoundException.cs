using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exeption.ProjectRoleExceptions
{
    public class ProjectRolesNotFoundException : Exception
    {
        const string ProjectRolesNotFoundMessage = "Роли на проекте не найдены";

        public ProjectRolesNotFoundException() : base(ProjectRolesNotFoundMessage) { }
    }
}
