using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;

namespace ATSystem.Controllers
{
    public class BrandController : Controller
    {
        private IBrandManager brandManager;
        private IGeneralCategoryManager generalCategoryManager;

        public BrandController(IBrandManager _brandManager,IGeneralCategoryManager _generalCategoryManager)
        {
            brandManager = _brandManager;
            generalCategoryManager = _generalCategoryManager;
        }
        
        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var brandtable = brandManager.GetSome(5);
            ViewBag.brandlist = brandtable.ToList();

            var list = generalCategoryManager.GetAll();
            ViewBag.orglist = list.ToList();
            bool s = Request.QueryString["success"] == "true";
            if (s)
            {
                ViewData["success"] = "Save Successfully";

            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            var brandtable = brandManager.GetSome(5);
            ViewBag.brandlist = brandtable.ToList();

            if (brandManager.Add(brand))
            {
                ModelState.Clear();
                return RedirectToAction("Create", new { success = "true" });
            }
            var list = generalCategoryManager.GetAll();
            ViewBag.orglist = list.ToList();
            return View(brand);
        }


        public JsonResult GetBrandssByCategoryId(int? categoryId)
        {
            var categories = brandManager.GetBrandssByCategoryId(categoryId).Select(c => new
            {
                c.Id,
                c.Name,
                c.Code,
                c.CategoryId,
                c.Description
            });
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}