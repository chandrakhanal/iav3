using IndianArmyWeb.Infrastructure.Helpers.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Infrastructure.Filters
{
    public class UserMenuAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var siteMenuManager = new SiteMenuManager();
            filterContext.Controller.ViewBag.SiteMenuItems = siteMenuManager.GetMenuItems().ToList();
            base.OnActionExecuting(filterContext);
        }
        

    }
}