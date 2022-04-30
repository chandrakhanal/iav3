using IndianArmyWeb.Areas.cpanel.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IndianArmyWeb.Infrastructure.Helpers.Account
{
    public static class UserAccountHelper
    {
        public static int getCurrentUserRoleId()
        {
            using (var context = new ApplicationDbContext())
            {
                int loginid = int.Parse(HttpContext.Current.User.Identity.GetUserId());
                var userls = context.Users.ToList().Find(x => x.Id == loginid);
                var rls = userls.Roles.Select(x => x.RoleId);
                int rl = rls.First();
                int roleId = context.Roles.ToList().First(x => x.Id == rl).Id;
                
                return roleId;
            }
        }
    }
}