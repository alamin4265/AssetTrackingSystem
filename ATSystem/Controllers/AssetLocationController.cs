using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;

namespace ATSystem.Controllers
{
    public class AssetLocationController : Controller
    {
        IAssetLocationManager assetLocationManager;
        IOrganizationManager organizationManager;

        public AssetLocationController(IAssetLocationManager _assetLocationManager, IOrganizationManager _organizationManager)
        {
            assetLocationManager = _assetLocationManager;
            organizationManager = _organizationManager;
        }

        public ActionResult Create()
        {

            var list = organizationManager.GetAll();
            ViewBag.orglist = list.ToList();
            bool s = Request.QueryString["success"] == "true";
            if (s)
            {
                ViewData["success"] = "Save Successfully";

            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(AssetLocation assetLocation)
        {
            if (assetLocationManager.IsExist(assetLocation.ShortName))
            {
                ViewData["exist"] = "ShortName Already Exist";
                //var dr =Request.Form["OrganizationId"];
                
            }
            else
            {
                assetLocationManager.Add(assetLocation);
                ModelState.Clear();
                return RedirectToAction("Create", new { success = "true" });
            }

            
            var list = organizationManager.GetAll();
            ViewBag.orglist = list.ToList();
            return View(assetLocation);
        }
    }
}