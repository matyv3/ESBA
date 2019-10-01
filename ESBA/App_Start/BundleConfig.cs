using System.Web;
using System.Web.Optimization;

namespace ESBA
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/now-ui-kit.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                      "~/Scripts/plugins/bootstrap-notify.js",
                      "~/Scripts/plugins/chartjs.min.js",
                      "~/Scripts/plugins/perfect-scrollbar.jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                      "~/Scripts/now-ui-dashboard.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/now-ui-kit.min.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/panel-css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/now-ui-dashboard.min.css",
                      "~/Content/Panel.css"));
        }
    }
}
