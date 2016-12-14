using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CSRToolWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //  Log in "short cut"
            routes.MapRoute(
                name: "LogIn",
                url: "LogIn",
                defaults: new { controller = "Home", action = "LogIn" }
            );
            //routes.MapRoute(
            //    name: "LogIn",
            //    url: "LogIn",
            //    defaults: new { controller = "Home", action = "Default" }
            //);

            //  Log out "short cut"
            routes.MapRoute(
                name: "LogOut",
                url: "LogOut",
                defaults: new { controller = "Home", action = "LogOut" }
            );

            //  UserInfo "short cut"
            routes.MapRoute(
                name: "UserInfo",
                url: "UserInfo",
                defaults: new { controller = "Home", action = "UserInfo" }
            );

            //  Home "short cut"
            routes.MapRoute(
                name: "Home",
                url: "Home",
                defaults: new { controller = "Home", action = "Default" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Default", id = UrlParameter.Optional }
            );
        }
    }
}
