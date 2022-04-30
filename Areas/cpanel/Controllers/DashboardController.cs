using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IndianArmyWeb.Areas.cpanel.Controllers
{
    public class DashboardController : Controller
    {
        // GET: cpanel/AdminHome
        public ActionResult Index()
        {
            return View();
        }
    }
}