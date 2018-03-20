using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;

namespace ATSystem.Controllers
{
    public class GeneralCategoryController : Controller
    {
        private IGeneralCategoryManager generalCategoryManager;
        private ICategoryManager categoryManager;

        public GeneralCategoryController(IGeneralCategoryManager _generalCategoryManager,ICategoryManager _categoryManager)
        {
            generalCategoryManager = _generalCategoryManager;
            categoryManager = _categoryManager;
        }
        
        // Create GeneralCategory
        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var list = generalCategoryManager.GetSome(5);
            ViewBag.olist = list.ToList();
            bool s = Request.QueryString["success"] == "true";
            if (s)
            {
                ViewData["success"] = "Save Successfully";

            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(GeneralCategory generalCategory)
        {
            var list = generalCategoryManager.GetSome(5);
            ViewBag.olist = list.ToList();
            if (generalCategoryManager.IsExist(generalCategory.Code))
            {
                ViewData["exist"] = "ShortName Already Exist";
            }
            else
            {
                generalCategoryManager.Add(generalCategory);
                ModelState.Clear();
                return RedirectToAction("Create", new { success = "true" });
            }
            return View(generalCategory);
        }


        // General CategoryList
        public ActionResult GeneralCategoryList()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            int id = Convert.ToInt32(Request.QueryString["delete"]);
            if (id != 0)
            {
                GeneralCategory generalCategory = generalCategoryManager.GetById(id);
                generalCategoryManager.Delete(generalCategory);
                return RedirectToAction("GeneralCategoryList");
            }
            return View();
        }

        // Json Result GetAllGeneralCategory
        public JsonResult GetAllGeneralCategory()
        {
            var totalRecord = generalCategoryManager.GetAll().Count;


            var generalcategorylist = generalCategoryManager.GetAll();

            //.OrderBy(sortColumn + " " + sortColumnDir)
            var gcategories = generalcategorylist.Select(c => new
            {
                c.Id,
                c.Name,
                c.Code
            });

            
            return Json(new{data=gcategories}, JsonRequestBehavior.AllowGet);
        }

        // Edit GeneralCategory
        public ActionResult Edit(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralCategory generalCategory = generalCategoryManager.GetById(id);
            if (generalCategory == null)
            {
                return HttpNotFound();
            }
            bool s = Request.QueryString["Update"] == "true";
            if (s)
            {
                ViewData["Update"] = "Update Successfully";

            }
            return View(generalCategory);
        }

        [HttpPost]
        public ActionResult Edit(int? id, Organization organizations)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralCategory generalCategory = generalCategoryManager.GetById(id);
            generalCategory.Name = organizations.Name;
            generalCategory.Code = organizations.ShortName;
            if (generalCategoryManager.IsExistUpdate(generalCategory.Code, id))
            {
                ViewData["exist"] = "Code Already Exist";
            }
            else
            {
                generalCategoryManager.Update(generalCategory);
                return RedirectToAction("Edit", new { Update = "true" });
            }

            return View(generalCategory);
        }


        // Indivisual Organization Details
        public ActionResult Details(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralCategory generalCategory = generalCategoryManager.GetById(id);

            if (generalCategory == null)
            {
                return HttpNotFound();
            }

            ViewBag.category = categoryManager.GetAll().Where(c => c.GeneralCategoryId == id).ToList();
            return View(generalCategory);
        }
    }
}