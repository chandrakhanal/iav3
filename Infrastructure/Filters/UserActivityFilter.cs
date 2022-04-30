using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Persistence;
using System.Web;
using IndianArmyWeb.Models;
using System.Web.Mvc;
using AutoMapper;
using System.Threading.Tasks;
using IndianArmyWeb.View_Models;

namespace IndianArmyWeb.Infrastructure.Filters
{
    public class UserActivityFilter : ActionFilterAttribute
    {
        //public void OnActionExecuted(ActionExecutedContext filterContext)
        //{
          
        //}

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var data = "";
            //var areaName = filterContext.RouteData.Values["area"];
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var url = $"{controllerName}/{actionName}";
            if (!string.IsNullOrEmpty(filterContext.HttpContext.Request.QueryString[0].ToString()))
            {
                data = filterContext.HttpContext.Request.QueryString[0].ToString();
            }
            else
            {
                var ActionInfo = filterContext.ActionDescriptor;
                var pars = ActionInfo.GetParameters();
                foreach (var p in pars)
                {
                    var type = p.ParameterType; //get type expected
                }
            }
            var userName = filterContext.HttpContext.User.Identity.Name;
            var ipAddress = HttpContext.Current.Request.UserHostAddress;
            SaveUserActivity(data, url, userName, ipAddress);
            base.OnActionExecuting(filterContext);
        }
        // public void UserActivityStore(string data,string url,string username,string ipaddress)
        public void SaveUserActivity(string data, string url, string userName, string ipAddress)
        {
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var userActivity = new UserActivity
                {
                    Data = data,
                    Url = url,
                    UserName = userName,
                    IpAddress = ipAddress
                };
                uow.UserActivityRepo.Add(userActivity);
                uow.Commit();
            }
        }
       
    }
}