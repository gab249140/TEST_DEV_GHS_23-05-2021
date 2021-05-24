using System.Web;
using System.Web.Optimization;

namespace PruebaWebApp
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

            bundles.Add(new ScriptBundle("~/bundles/js")
                .Include("~/Scripts/DataTables.sum.js")
                .Include("~/Scripts/jquery-3.4.1.intellisense.js")
                .Include("~/Scripts/jquery-3.4.1.js")
                .Include("~/Scripts/jquery-3.4.1.min.js")
                .Include("~/Scripts/jquery-3.4.1.min.map")
                .Include("~/Scripts/jquery-3.4.1.slim.js")
                .Include("~/Scripts/jquery-3.4.1.slim.min.js")
                .Include("~/Scripts/jquery.dataTables.js")
                .Include("~/Scripts/jquery.jquery.dataTables.min.js")
                .Include("~/Scripts/jquery.jquery.validate-vsdoc.js")
                .Include("~/Scripts/jquery.jquery.validate.js")
                .Include("~/Scripts/jquery.jquery.validate.min.js")
                .Include("~/Scripts/jquery.jquery.validate.unobtrusive.js")
                );
        }
    }
}
