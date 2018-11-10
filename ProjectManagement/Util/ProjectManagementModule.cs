using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using BLL.Services;
using BLL.Interfaces;

namespace ProjectManagement.Util
{
    public class ProjectManagementModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEmployeeService>().To<EmployeeService>();
            Bind<IParticipationHistoryService>().To<ParticipationHistoryService>();
            Bind<IProjectRoleService>().To<ProjectRoleService>();
            Bind<IProjectService>().To<ProjectService>();
            Bind<IProjectWorkService>().To<ProjectWorkService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<IScheduleService>().To<ScheduleServise>();
        }
    }
}