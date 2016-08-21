using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace RentalVideo
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //    "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //    "~/Scripts/jquery.unobtrusive*",
            //    "~/Scripts/jquery.validate*"));


            //bundles.Add(new ScriptBundle("~/bundles/app").Include(
            //    "~/Scripts/sammy-{version}.js",
            //    "~/Scripts/app/common.js",
            //    "~/Scripts/app/app.datamodel.js",
            //    "~/Scripts/app/app.viewmodel.js",
            //    "~/Scripts/app/home.viewmodel.js",
            //    "~/Scripts/app/_run.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/spa").Include(
                "~/Scripts/spa/modules/common.core.js",
                "~/Scripts/spa/modules/common.ui.js",
                "~/Scripts/spa/app.js",
                "~/Scripts/spa/services/apiServices.js",
                "~/Scripts/spa/services/notificationService.js",
                "~/Scripts/spa/services/membershipService.js",
                "~/Scripts/spa/layout/topBar.directive.js",
                "~/Scripts/spa/layout/sideBar.directive.js",
                "~/Scripts/spa/home/rootController.js",
                "~/Scripts/spa/home/indexController.js",
                "~/Scripts/spa/account/loginController.js",
                "~/Scripts/spa/account/registerController.js",
                "~/Scripts/spa/directives/availableMovie.directive.js",
                "~/Scripts/spa/directives/componentRating.directive.js"));

            bundles.Add(new ScriptBundle("~/bundles/vendors").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/jquery.raty.js",
                "~/Scripts/respond.js",
                "~/Scripts/angular.js",
                //"~/Scripts/angular-animate.js",
                "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/angular-resource.js",
                "~/Scripts/angular-cookies.js",
                "~/Scripts/angular-validator.js",
                "~/Scripts/angular-base64.js",
                "~/Scripts/angular-file-upload.js",
                "~/Scripts/angucomplete-alt.js",
                "~/Scripts/underscore.js",
                "~/Scripts/raphael-min.js",
                "~/Scripts/morris.js",
                "~/Scripts/jquery.fancybox.js",
                "~/Scripts/jquery.fancybox.media.js",
                "~/Scripts/loading-bar.js",
                "~/Scripts/toastr.js"));


            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //    "~/Scripts/bootstrap.js",
            //    "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Site.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                //"~/Content/Site.css",
                "~/Content/font-awesome.css",
                "~/Content/morris.css",
                "~/Content/toastr.css",
                "~/Content/jquery.fancybox.css",
                "~/Content/loading-bar.css"));
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif

        }
    }
}
