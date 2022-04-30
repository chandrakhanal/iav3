using IndianArmyWeb.Infrastructure.Constants;
using IndianArmyWeb.Infrastructure.Helpers.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Infrastructure.Filters
{
    public class FooterMenuAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var siteMenuManager = new SiteMenuManager();
            filterContext.Controller.ViewBag.FooterMenuItems = siteMenuManager.GetMenuItems(PositionType.Bottom).ToList();
            base.OnActionExecuting(filterContext);
        }
    }
}