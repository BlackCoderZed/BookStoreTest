using System.Web;
using System.Web.Optimization;

namespace BookStoreWebUI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/commonLibs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/popper.min.js",
                "~/Scripts/bootbox.js",
                "~/Scripts/bootstrap.min.js",
                //"~/Scripts/modernizr-*",
                "~/Scripts/jquery-ui.js",
                "~/Scripts/jquery.blockUI.js",
                "~/Scripts/jquery-ui-sliderAccess.js",
                "~/Scripts/jquery-ui-timepicker-addon.js",
                "~/Scripts/jquery.mCustomScrollbar.concat.min.js",
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/DataTables/dataTables.jqueryui.js",
                "~/Scripts/DataTables/dataTables.rowReorder.js",
                "~/Scripts/DataTables/dataTables.responsive.js",
                "~/Scripts/Site/common.js"
            ));

            bundles.Add(new Bundle("~/bundles/operationLibs").Include(
                "~/Scripts/Site/book-list.js",
                "~/Scripts/Site/cart.js",
                "~/Scripts/Site/book-detail.js"
            ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/jquery-ui.theme.css",
                      "~/Content/jquery-ui-timepicker-addon.css",
                      "~/Content/jquery.mCustomScrollbar.min.css",
                      "~/Content/DataTables/css/rowReorder.dataTables.css",
                      "~/Content/DataTables/css/responsive.dataTables.css",
                      "~/Content/DataTables/css/jquery.dataTables.css",
                      "~/Content/DataTables/css/dataTables.jqueryui.css",
                      "~/Content/DataTables/css/buttons.dataTables.css",
                      "~/Content/DataTables/css/colReorder.dataTables.css",
                      "~/Content/site.css"));
        }
    }
}
