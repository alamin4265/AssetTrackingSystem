using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ATSystem.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/datatablejs")
                .Include(
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/DataTables/dataTables.bootstrap.js"
                ));
        }
    }
}