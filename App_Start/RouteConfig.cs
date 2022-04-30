using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IndianArmyWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Content1",
                url: "Content1/{slug}",
                defaults: new { controller = "Dynamic", action = "DynamicPL1", slug = "" }
            );

            routes.MapRoute(
                name: "Content2",
                url: "Content2/{parentSlug}/{childSlug}",
                defaults: new { controller = "Dynamic", action = "DynamicL1", parentSlug = "", childSlug = "" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            "404-PageNotFound",
            "{*url}",
            new { controller = "StaticContent", action = "PageNotFound" }
            );
            //routes.MapRoute(
            //    name: "DGRL2",
            //    url: "DGRL2/{parentSlug}/{childSlug}",
            //    defaults: new { controller = "Dynamic", action = "DynamicL2", parent = "", child = "" }
            //);

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
