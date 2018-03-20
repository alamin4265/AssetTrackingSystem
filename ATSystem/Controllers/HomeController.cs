using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ATSystem.Context;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;

namespace ATSystem.Controllers
{
    public class HomeController : Controller
    {

        private IContactManager contactManager;
        private IAssetRegistrationDetailsManager assetRegDetailsManager;
        public HomeController(IContactManager _contactManager, IAssetRegistrationDetailsManager _assetRegDetailsManager)
        {
            contactManager = _contactManager;
            assetRegDetailsManager = _assetRegDetailsManager;
        }

        public ActionResult HomePage()
        {

            if (Request.QueryString["success"] == "true")
            {
                ViewBag.Message = "Request Sent Successfully , You will be notified soon...";
            }
            else if (Request.QueryString["success"] == "false")
            {
                ViewBag.Message = "Request Sent Failed";
            }
            else
            {
                return View();
            }

            return View();
        }

        public ActionResult Home()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            int oId = (int)Session["organizationId"];



            List<string> branchnamelist = new List<string>();
            List<int> count = new List<int>();
            /*List<AssetRegistrationDetails> list*/
            int x = 0;
            var l =assetRegDetailsManager.GetAllforGraph(oId).GroupBy(b=>b.BranchId);
            foreach (var lk in l)
            {
                x = (int) lk.LongCount();
                count.Add(x);
                branchnamelist.Add(lk.First().BranchName);
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Contents.RemoveAll();
            HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Response.Cache.SetNoServerCaching();
            HttpContext.Response.Cache.SetNoStore();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            Session.Abandon();

            return RedirectToAction("HomePage", "Home");
        }

        [HttpPost]
        public ActionResult Contact(Contact aContact)
        {
            if (contactManager.Add(aContact))
            {
                return RedirectToAction("HomePage", new { success = "true" });
            }
            else
            {
                return RedirectToAction("HomePage", new { success = "false" });
            }

        }

        public ActionResult Instruction()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }
    }
}