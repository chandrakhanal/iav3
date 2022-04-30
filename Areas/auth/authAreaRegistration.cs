using System.Web.Mvc;

namespace IndianArmyWeb.Areas.auth
{
    public class authAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "auth";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "auth_default",
                "auth/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}