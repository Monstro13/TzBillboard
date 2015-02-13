using System.Web;
using System.Web.Optimization;

namespace Billboard
{
    public class BundleConfig
    {
        // Дополнительные сведения о Bundling см. по адресу http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/Css").Include(
                "~/Content/Css/bootstrap.css", 
                "~/Content/Css/bootstrap-theme.css",
                "~/Content/Css/custom.css"));

            bundles.Add(new ScriptBundle("~/bundles/otherJs").Include(
                "~/Content/Js/bootstrap.js", 
                "~/Content/Js/jquery-1.11.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularApp").Include(
                        "~/Content/Js/angular.js",
                        "~/Content/Js/angular-route.js",
                        "~/Content/Js/angular-resource.js",
                        "~/Content/Js/ui-bootstrap-tpls-0.12.0.js",
                        "~/Content/Js/moment.js",
                        "~/Content/Js/moment-with-locales.js",
                        "~/Content/App/app.js",
                        "~/Content/App/Controllers/root.js",
                        "~/Content/App/Controllers/welcome.js",
                        "~/Content/App/Controllers/register.js",
                        "~/Content/App/Directives/repeatPassword.js",
                        "~/Content/App/Services/auth.js",
                        "~/Content/App/Controllers/login.js",
                        "~/Content/App/Controllers/ad.js",
                        "~/Content/App/Controllers/add.js",
                        "~/Content/App/Services/manager.js",
                        "~/Content/App/Filters/fromNow.js",
                        "~/Content/App/Controllers/friends.js",
                        "~/Content/App/Controllers/friend.js"));
        }
    }
}