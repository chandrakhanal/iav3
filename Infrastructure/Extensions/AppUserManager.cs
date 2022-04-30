using IndianArmyWeb.Areas.Admin.Models;
using IndianArmyWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IndianArmyWeb.Infrastructure.Extensions
{
    public class AppUserManager : UserManager<ApplicationUser>
    {
        public AppUserManager()
            : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
            PasswordValidator = new MinimumLengthValidator(8);
        }
    }
}