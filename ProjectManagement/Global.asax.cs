using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject.Modules;
using ProjectManagement.Util;
using BLL.Infrastructure;
using Ninject;
using Ninject.Web.Mvc;
using Ninject.Web.WebApi;
using Ninject.Web.WebApi.Filter;

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

            NinjectModule employeeModule = new ProjectManagementModule();
            NinjectModule serviceModule = new ServiceModule("ManagementContext");
            var kernel = new StandardKernel(employeeModule, serviceModule);
            kernel.Bind<DefaultFilterProviders>().ToConstant(new DefaultFilterProviders(GlobalConfiguration.Configuration.Services.GetServices(typeof(System.Web.Http.Filters.IFilterProvider)).Cast<System.Web.Http.Filters.IFilterProvider>()));
            kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetServices(typeof(System.Web.Http.Validation.ModelValidatorProvider)).Cast<System.Web.Http.Validation.ModelValidatorProvider>()));
            DependencyResolver.SetResolver(new Ninject.Web.Mvc.NinjectDependencyResolver(kernel));
            GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
        }
    }
}
