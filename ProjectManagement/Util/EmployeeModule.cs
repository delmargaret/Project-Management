using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using BLL.Services;
using BLL.Interfaces;

namespace ProjectManagement.Util
{
    public class EmployeeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<IParticipationHistoryService>().To<PartisipationHistoryService>();
            Bind<IProjectRoleService>().To<IProjectRoleService>();
            Bind<IRoleService>().To<RoleService>();
        }
    }
}