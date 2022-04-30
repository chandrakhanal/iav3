using System.Web;
using System.Web.Optimization;

namespace IndianArmyWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new Bundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-3.6.0.min.js"
            ));
            bundles.Add(new Bundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-2.8.3.js"
            ));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                                  "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/iasite/css").Include(
                                        "~/Content/bootstrap.min.css",
                                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Admin/css").Include(
                                        "~/Areas/Cpanel/assets/css/main.css"));

            bundles.Add(new Bundle("~/bundles/main").Include(
                     "~/Areas/Cpanel/assets/scripts/main.js"
                     ));
        }
    }
}
