﻿using BLL.Infrastructure;
using Ninject;
using Ninject.Modules;
using ProjectManagement.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing.Constraints;

namespace ProjectManagement
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute(origins: "*", headers: "Content-Type", methods: "GET, POST, PUT, DELETE, OPTIONS");
            config.EnableCors(cors);
            config.Filters.Add(new AuthorizeAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "Def",
                routeTemplate: "{controller}",
                defaults: new { }
                );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { },
                constraints: new { id = new IntRouteConstraint() }
                );

            config.Routes.MapHttpRoute(
                name: "MyAp",
                routeTemplate: "{controller}/{action}"
                );

            config.Routes.MapHttpRoute(
                name: "MyApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { },
                constraints: new { id = new IntRouteConstraint() }
                );
        }
    }
}
