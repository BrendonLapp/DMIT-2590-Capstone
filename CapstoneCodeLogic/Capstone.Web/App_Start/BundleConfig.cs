using System.Web;
using System.Web.Optimization;

namespace Capstone.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/Content/cssjqryUi").Include(
                   "~/Content/jquery-ui.css"));


            bundles.Add(new ScriptBundle("~/bundles/jquery-timepicker").Include(
                      "~/Scripts/jquery-ui-timepicker-addon.js"));

            bundles.Add(new StyleBundle("~/Content/jquery-timepicker").Include(
                   "~/Content/jquery-ui-timepicker-addon.css"));

            bundles.Add(new StyleBundle("~/Content/spectrum").Include(
                   "~/Content/spectrum.css"));

            bundles.Add(new ScriptBundle("~/bundles/spectrum").Include(
                      "~/Scripts/spectrum.js"));

            bundles.Add(new ScriptBundle("~/Content/FullCalendar/core")
                .Include("~/Content/FullCalendar/packages/core/main.js"));

            bundles.Add(new ScriptBundle("~/Content/FullCalendar/daygrid")
                   .Include("~/Content/FullCalendar/packages/daygrid/main.js"));

            bundles.Add(new ScriptBundle("~/Content/FullCalendar/timegrid")
                   .Include("~/Content/FullCalendar/packages/timegrid/main.js"));

            bundles.Add(new StyleBundle("~/bundles/FullCalendar/core")
                .Include("~/Content/FullCalendar/packages/core/main.css"));

            bundles.Add(new StyleBundle("~/bundles/FullCalendar/daygrid")
                    .Include("~/Content/FullCalendar/packages/daygrid/main.css"));

            bundles.Add(new StyleBundle("~/bundles/FullCalendar/timegrid")
                    .Include("~/Content/FullCalendar/packages/timegrid/main.css"));

        }
    }
}
