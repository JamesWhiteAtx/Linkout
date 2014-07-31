using System.Web;
using System.Web.Optimization;

namespace Linkout
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;   //enable CDN support

            bundles.Add(new ScriptBundle("~/bundles/jquery", "http://ajax.googleapis.com/ajax/libs/jquery/2.0.0/jquery.min.js") { CdnFallbackExpression = "window.jQuery" }
                        .Include("~/Scripts/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui", "http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.3/jquery-ui.min.js") { CdnFallbackExpression = "window.jQuery.ui" }
                        .Include("~/Scripts/jquery/jquery-ui-{version}.js"));

            bundles.Add(new StyleBundle("~/css/jqueryui").Include(
                        "~/Css/jquery/jquery.ui.core.css",
                        "~/Css/jquery/jquery.ui.resizable.css",
                        "~/Css/jquery/jquery.ui.selectable.css",
                        "~/Css/jquery/jquery.ui.accordion.css",
                        "~/Css/jquery/jquery.ui.autocomplete.css",
                        "~/Css/jquery/jquery.ui.button.css",
                        "~/Css/jquery/jquery.ui.dialog.css",
                        "~/Css/jquery/jquery.ui.slider.css",
                        "~/Css/jquery/jquery.ui.tabs.css",
                        "~/Css/jquery/jquery.ui.datepicker.css",
                        "~/Css/jquery/jquery.ui.progressbar.css",
                        "~/Css/jquery/jquery.ui.theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/css/site").Include("~/Css/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryuicust").Include(
                    "~/Scripts/jquery/jquery-ui-{version}.custom.js"));

            bundles.Add(new StyleBundle("~/css/jquery/jqueryuicust").Include(
                        "~/css/jquery/jquery-ui-{version}.custom.css"));

            // twitter bootstrap assets
            bundles.Add(new StyleBundle("~/css/bootstrap").Include(
                        "~/css/bootstrap/bootstrap.css"));

            bundles.Add(new StyleBundle("~/css/bootstrap-responsive").Include(
                "~/css/bootstrap/bootstrap-responsive.css"));

            bundles.Add(new StyleBundle("~/css/bootstrap-cust").Include(
                "~/css/bootstrap/bootstrap.cust.css",
                "~/css/bootstrap/social-buttons.css"));
            
            bundles.Add(new ScriptBundle("~/bundles/ui-boots")
                        .Include("~/Scripts/angular/ui-bootstrap-tpls-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs")
                        .Include("~/Scripts/bootstrap/bootstrap.js"));

            // font-awesome assets
            bundles.Add(new StyleBundle("~/css/font-awesome").Include(
                        "~/css/fontawesome/font-awesome.css"));

            /// angularJS assets
            bundles.Add(new ScriptBundle("~/bundles/angular", "http://ajax.googleapis.com/ajax/libs/angularjs/1.0.6/angular.min.js") { CdnFallbackExpression = "window.angular" }
                        .Include("~/Scripts/angular/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-resource", "http://ajax.googleapis.com/ajax/libs/angularjs/1.0.6/angular-resource.min.js") { CdnFallbackExpression = "window.angular" }
                        .Include("~/Scripts/angular/angular-resource.js"));

            bundles.Add(new ScriptBundle("~/bundles/ng-grid")
                        .Include("~/Scripts/angular/ng-grid-{version}.js"));

            bundles.Add(new StyleBundle("~/css/angular/ng-grid").Include(
                        "~/css/angular/ng-grid.css"));

            bundles.Add(new StyleBundle("~/css/linkout").Include(
                        "~/css/linkout.css"));

            bundles.Add(new ScriptBundle("~/bundles/linkout").Include(
                        "~/Scripts/linkout/app.js",
                        "~/Scripts/linkout/services.js",
                        //"~/Scripts/linkout/filters.js",
                        "~/Scripts/linkout/directives.js",
                        "~/Scripts/linkout/controllers.js"));

            bundles.Add(new StyleBundle("~/css/linkout").Include(
                        "~/css/linkout.css"));

            bundles.Add(new ScriptBundle("~/bundles/configure").Include(
                        "~/Scripts/configure/app.js",
                        "~/Scripts/configure/services.js",
                        //"~/Scripts/configure/filters.js",
                        "~/Scripts/configure/directives.js",
                        "~/Scripts/configure/controllers.js",
                        "~/Scripts/linkout/services.js"));

            bundles.Add(new StyleBundle("~/css/configure").Include(
                        "~/css/configure.css"));

            bundles.Add(new ScriptBundle("~/bundles/costcoadmin").Include(
                        "~/Scripts/costcoadmin/app.js",
                        "~/Scripts/costcoadmin/services.js",
                        "~/Scripts/costcoadmin/controllers.js"));

            bundles.Add(new StyleBundle("~/style/costco").Include(
                        "~/css/costco.css"));

            bundles.Add(new StyleBundle("~/css/tree").Include(
                        "~/css/tree.css"));

            bundles.Add(new ScriptBundle("~/bundles/util").Include(
                        "~/Scripts/util/app.js",
                        //"~/Scripts/util/services.js",
                        //"~/Scripts/util/filters.js",
                        //"~/Scripts/util/directives.js",
                        //"~/Scripts/util/services.js",
                        "~/Scripts/util/controllers.js"));

            bundles.Add(new StyleBundle("~/css/util").Include(
                        "~/css/util.css"));


            bundles.Add(new ScriptBundle("~/bundles/roadwire-ebay").Include(
                        "~/Scripts/roadwire_ebay/app.js",
                        //"~/Scripts/roadwire_ebay/services.js",
                        //"~/Scripts/roadwire_ebay/filters.js",
                        //"~/Scripts/roadwire_ebay/directives.js",
                        "~/Scripts/roadwire_ebay/controllers.js",
                        "~/Scripts/roadwire_ebay/services.js"));

            //bundles.Add(new StyleBundle("~/css/roadwire-ebay").Include("~/css/configure.css"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            ////////////////////////////////////////
            // COSTCO //////////////////////////////

            // Script Bundles

            bundles.Add(new ScriptBundle("~/bundles/cc/jquery").Include(
                "~/Scripts/costco/jquery/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/cc/bootstrap").Include(
                "~/Scripts/costco/bootstrap/bootstrap.js",
                "~/Scripts/costco/bootstrap/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/cc/angular").Include(
                "~/Scripts/costco/angular/angular-{version}.js",
                "~/Scripts/costco/angular/angular-route-{version}.js",
                "~/Scripts/costco/angular/angular-resource-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/cc/ui-boots").Include(
                "~/Scripts/costco/angular/ui-bootstrap-custom-tpls-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/cc/angstyles").Include(
                "~/Scripts/costco/angular/route-styles.js"));

            bundles.Add(new ScriptBundle("~/bundles/cc/animate").Include(
                "~/Scripts/costco/angular/angular-animate-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/cc/angstyles").Include(
                "~/Scripts/costco/angular/route-styles.js"));

            bundles.Add(new ScriptBundle("~/bundles/cc/rwutils").Include(
                "~/Scripts/costco/roadwire/rwservices.js",
                "~/Scripts/costco/roadwire/rwdirectives.js"));

            bundles.Add(new ScriptBundle("~/bundles/cc/costco").Include(
                "~/Scripts/costco/costco/app.js",
                "~/Scripts/costco/costco/controllers.js",
                "~/Scripts/costco/costco/services.js",
                "~/Scripts/costco/costco/directives.js"));

            // Style Bundles
            bundles.Add(new StyleBundle("~/css/cc/styles/site").Include(
                "~/css/cc/bootstrap/bootstrap.css",
                "~/css/cc/site/site.css",
                "~/css/cc/costco/costco.css"));

        }
    }
}