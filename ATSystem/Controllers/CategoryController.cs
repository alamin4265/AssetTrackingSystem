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
    public class CategoryController : Controller
    {
        private ICategoryManager categoryManager;
        private IGeneralCategoryManager generalCategoryManager;

        public CategoryController(ICategoryManager manager, IGeneralCategoryManager gmanager)
        {
            categoryManager = manager;
            generalCategoryManager = gmanager;
        }
        

        // Create Category
        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var list = generalCategoryManager.GetAll();
            ViewBag.gclist = list.ToList();

            var lists = categoryManager.GetSome(5);
            ViewBag.clist = lists.ToList();

            bool s = Request.QueryString["success"] == "true";
            if (s)
            {
                ViewData["success"] = "Save Successfully";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            var list = generalCategoryManager.GetAll();
            ViewBag.gclist = list.ToList();
            

            var lists = categoryManager.GetSome(5);
            ViewBag.clist = lists.ToList();

            if (categoryManager.Add(category))
            {
                ModelState.Clear();
                return RedirectToAction("Create", new { success = "true" });
            }
            
            return View(category);
        }


        // Edit Branch
        public ActionResult Edit(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var list = generalCategoryManager.GetAll();
            ViewBag.orglist = list.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = categoryManager.GetById(id);

            if (category == null)
            {
                return HttpNotFound();
            }
            bool s = Request.QueryString["Update"] == "true";
            if (s)
            {
                ViewData["Update"] = "Update Successfully";

            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(int? id, Category category)
        {
            var list = generalCategoryManager.GetAll();
            ViewBag.orglist = list.ToList();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            

            if (categoryManager.IsExistUpdate(category.Code, category.Id, category.GeneralCategoryId))
            {
                ViewData["exist"] = "Code Already Exist";
            }
            else
            {
                categoryManager.Update(category);
                return RedirectToAction("Edit", new { Update = "true" });
            }
            return View(category);
        }


        // Category List

        public ActionResult CategoryList()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            int id = Convert.ToInt32(Request.QueryString["delete"]);
            if (id != 0)
            {
                Category category = categoryManager.GetById(id);
                categoryManager.Delete(category);
                return RedirectToAction("CategoryList");
            }
            return View();
        }

        // Json GetAllCategory
        public JsonResult GetAllCategory()
        {
            var totalRecord = categoryManager.GetAllCategorywithGeneralCategoryName().Count;
            


            var categorylist = categoryManager.GetAllCategorywithGeneralCategoryName();
            var branches = categorylist.Select(c => new
            {
                c.Id,
                c.Name,
                c.Code,
                c.Description,
                c.GeneralCategory,
                c.GeneralCategoryId
            });
            return Json(new{data=branches}, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetCategoriesByGeneralCategoryId(int? generalCategoryId)
        {

            var categories = categoryManager.GetBranchsByOrganizationId(generalCategoryId).Select(c => new
            {
                c.Id,
                c.Name,
                c.Code,
                c.Description,
                c.GeneralCategoryId

            });
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}