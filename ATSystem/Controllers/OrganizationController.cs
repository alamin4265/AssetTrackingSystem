using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ATSystem.BAL;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;

namespace ATSystem.Controllers
{
    public class OrganizationController : Controller
    {
        private IOrganizationManager organizationManager;
        private IBranchManager branchManager;
        public OrganizationController(IOrganizationManager _organizationManager,IBranchManager _branchManager)
        {
            organizationManager = _organizationManager;
            branchManager = _branchManager;
        }



        public ActionResult OrganizationList()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            int id = Convert.ToInt32(Request.QueryString["delete"]);
            if (id != 0)
            {
                Organization organization = organizationManager.GetById(id);
                organizationManager.Delete(organization);
                return RedirectToAction("OrganizationList");
            }

            return View();
        }
        public JsonResult GetAllOrganization(string draw, int? start, int? length)
        {
            
            var totalRecord = organizationManager.GetAll().Count;
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();


            var organizationlist = organizationManager.GetAll().Skip(start ?? 0).Take(length ?? 0).OrderBy(c => sortColumn + " " + sortColumnDir);

            //.OrderBy(sortColumn + " " + sortColumnDir)
            var organizations = organizationlist.Select(c => new
            {
                c.Id,
                c.Name,
                c.ShortName,
                c.Code,
                c.Location
            });

            var jsonData = new { draw = draw, recordsTotal = totalRecord, recordsFiltered = totalRecord, data = organizations};

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllOrganization()
        {
            var result = organizationManager.GetAll().OrderBy(c=>c.Name).ToList();
            var jsonData = result.Select(c => new
            {
                c.Id,
                c.Name,
                c.ShortName,
                c.Code,
                c.Location
            });
            return Json(new {data= jsonData}, JsonRequestBehavior.AllowGet);
        }


        // Create Organization
        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var list = organizationManager.GetSome(5);
            ViewBag.olist = list.ToList();
            bool s = Request.QueryString["success"] == "true";
            if (s)
            {
                ViewData["success"] = "Save Successfully";

            }
            return View();
        }
         
        [HttpPost]
        public ActionResult Create(Organization organization)
        {
            var list = organizationManager.GetSome(5);
            ViewBag.olist = list.ToList();

            if (organizationManager.IsExist(organization.Code))
            {
                ViewData["exist"] = "Code Already Exist";
            }
            else
            {
                organizationManager.Add(organization);
                ModelState.Clear();
                return RedirectToAction("Create", new { success = "true" });
            }
            return View(organization);
        }



        // Edit Organization
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
            Organization organization = organizationManager.GetById(id);
            if (organization == null)
            {
                return HttpNotFound();
            }
            bool s = Request.QueryString["Update"] == "true";
            if (s)
            {
                ViewData["Update"] = "Update Successfully";

            }
            return View(organization);
        }

        [HttpPost]
        public ActionResult Edit(int? id, Organization organizations)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Organization organization = organizationManager.GetById(id);
            organization.Name = organizations.Name;
            organization.ShortName = organizations.ShortName;
            organization.Code = organizations.Code;
            if (organizationManager.IsExistUpdate(organization.Code, id))
            {
                ViewData["exist"] = "Code Already Exist";
            }
            else
            {
                organizationManager.Update(organization);
                return RedirectToAction("Edit", new { Update = "true" });
            }

            return View(organization);
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
            Organization organization = organizationManager.GetById(id);
            
            if (organization == null)
            {
                return HttpNotFound();
            }

            ViewBag.branchlist = branchManager.GetAll().Where(c => c.OrganizationId == id).ToList();
            return View(organization);
        }


    }
}