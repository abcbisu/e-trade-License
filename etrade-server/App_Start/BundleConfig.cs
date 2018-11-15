using System.Web;
using System.Web.Optimization;

namespace etrade_server
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/App").Include(
                        "~/App/runtime.js",
                        "~/App/polyfills.js",
                        "~/App/styles.js",
                        "~/App/scripts.js",
                        "~/App/vendor.js",
                        "~/App/main.js"
                        ));
        }
    }
}
