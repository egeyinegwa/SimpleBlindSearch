using System.Web;
using System.Web.Optimization;

namespace Blinds.com
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        //"~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery-ui-{version}.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/ExtLibs").Include(
                        "~/Scripts/knockout-{version}.js",
                        "~/Scripts/knockout.mapping-latest.js",
                        "~/Scripts/binding-handlers.js"));

            bundles.Add(new ScriptBundle("~/bundles/Libs").Include(
                        "~/Scripts/functions.js"));

            bundles.Add(new ScriptBundle("~/bundles/JSViewModels").Include(
                        "~/JSViewModels/*.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css",
                                                                 "~/Content/jquery-ui-{version}.custom.css"
                                                                 ));

            bundles.Add(new StyleBundle("~/Content/themes/redmond/css").Include(
                        "~/Content/themes/redmond/jquery.ui.core.css",
                        "~/Content/themes/redmond/jquery.ui.resizable.css",
                        "~/Content/themes/redmond/jquery.ui.selectable.css",
                        "~/Content/themes/redmond/jquery.ui.accordion.css",
                        "~/Content/themes/redmond/jquery.ui.autocomplete.css",
                        "~/Content/themes/redmond/jquery.ui.button.css",
                        "~/Content/themes/redmond/jquery.ui.dialog.css",
                        "~/Content/themes/redmond/jquery.ui.slider.css",
                        "~/Content/themes/redmond/jquery.ui.tabs.css",
                        "~/Content/themes/redmond/jquery.ui.datepicker.css",
                        "~/Content/themes/redmond/jquery.ui.progressbar.css",
                        "~/Content/themes/redmond/jquery.ui.theme.css"));
        }
    }
}