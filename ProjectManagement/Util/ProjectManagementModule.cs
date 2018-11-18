using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using BLL.Services;
using BLL.Interfaces;
using Ninject.Web.Common;

namespace ProjectManagement.Util
{
    public class ProjectManagementModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEmployeeService>().To<EmployeeService>().InRequestScope(); 
            Bind<IParticipationHistoryService>().To<ParticipationHistoryService>().InRequestScope();
            Bind<IProjectRoleService>().To<ProjectRoleService>().InRequestScope();
            Bind<IProjectService>().To<ProjectService>().InRequestScope();
            Bind<IProjectWorkService>().To<ProjectWorkService>().InRequestScope();
            Bind<IRoleService>().To<RoleService>().InRequestScope();
            Bind<IScheduleService>().To<ScheduleServise>().InRequestScope();
        }
    }
}