using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject.Modules;
using ProjectManagement.Util;
using BLL.Infrastructure;
using Ninject;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.WebHost;
using Ninject.Web.Common.WebHost;
using BLL.Interfaces;
using BLL.Services;
using Ninject.Web.Common;
using System;
using System.Web;

namespace ProjectManagement
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //NinjectModule employeeModule = new ProjectManagementModule();
            //NinjectModule serviceModule = new ServiceModule("ManagementContext");
            //var kernel = new StandardKernel(serviceModule);
            ////Registration(kernel);
            //GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
        }

        //private void Registration(IKernel kernel)
        //{
        //    kernel.Bind<IEmployeeService>().To<EmployeeService>().InRequestScope();
        //    kernel.Bind<IParticipationHistoryService>().To<ParticipationHistoryService>().InRequestScope();
        //    kernel.Bind<IProjectRoleService>().To<ProjectRoleService>().InRequestScope();
        //    kernel.Bind<IProjectService>().To<ProjectService>().InRequestScope();
        //    kernel.Bind<IProjectWorkService>().To<ProjectWorkService>().InRequestScope();
        //    kernel.Bind<IRoleService>().To<RoleService>().InRequestScope();
        //    kernel.Bind<IScheduleService>().To<ScheduleServise>().InRequestScope();
        //}
    }
}
