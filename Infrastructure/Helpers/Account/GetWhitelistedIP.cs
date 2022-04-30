using IndianArmyWeb.DataContexts;
using IndianArmyWeb.Models;
using IndianArmyWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Infrastructure.Helpers.Account
{
    public static class GetWhitelistedIP
    {
        public static string GetCSIP(string ipAddress)
        {
            string ips = "";
            using (var uow = new UnitOfWork(new IASiteContext()))
            {
                ips = uow.CSConfRepo.FirstOrDefault(x=> x.ConfIP == ipAddress).ConfIP;
            }
            return ips;
        }
    }
}