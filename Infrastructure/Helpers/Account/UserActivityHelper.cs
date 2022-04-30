using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Infrastructure.Helpers.Account
{
    public static class UserActivityHelper
    {
        
        public static void SaveUserActivity(string data, string url)
        {

            string userName = HttpContext.Current.User.Identity.Name;
            string ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ipAddress))
            {
                ipAddress = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                var userActivity = new UserActivity
                {
                    Data = data,
                    Url = url,
                    UserName = userName,
                    IpAddress = ipAddress,
                    ActivityDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))
                };
                uow.UserActivityRepo.Add(userActivity);
                uow.Commit();
            }
        }
    }
}