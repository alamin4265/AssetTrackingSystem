using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;

namespace ATSystem.Controllers
{
    public class ProductCategoryController : Controller
    {
        private IProductCategoryManager productCategoryManager;
        private IGeneralCategoryManager generalCategoryManager;

        public ProductCategoryController(IProductCategoryManager _productCategoryManager,IGeneralCategoryManager _generalCategoryManager)
        {
            productCategoryManager = _productCategoryManager;
            generalCategoryManager = _generalCategoryManager;
        }


        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var productcategorylist = productCategoryManager.GetSome(5);
            ViewBag.plist = productcategorylist.ToList();

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
        public ActionResult Create(ProductCategory productCategory)
        {
            var productcategorylist = productCategoryManager.GetSome(5);
            ViewBag.plist = productcategorylist.ToList();

            var lists = generalCategoryManager.GetAll();
            ViewBag.orglist = lists.ToList();

            if (productCategoryManager.Add(productCategory))
            {
                ModelState.Clear();
                return RedirectToAction("Create", new { success = "true" });
            }
            var list = generalCategoryManager.GetAll();
            ViewBag.orglist = list.ToList();
            return View(productCategory);
        }
    }
}