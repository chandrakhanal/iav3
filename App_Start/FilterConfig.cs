using System.Web;
using System.Web.Mvc;
using IndianArmyWeb.Infrastructure.Filters;
namespace IndianArmyWeb
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CSPLHeaders());
        }
    }
}
