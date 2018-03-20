using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATSystem.BAL;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.ViewModel.Branch;
using AutoMapper;

namespace ATSystem.Controllers
{
    public class BranchController : Controller
    {
        IBranchManager branchManager;
        IOrganizationManager organizationManager;
        private IUserManager userManager;

        public BranchController(IBranchManager _branchManager, IOrganizationManager _organizationManager, IUserManager _userManager)
        {
            branchManager = _branchManager;
            organizationManager = _organizationManager;
            userManager = _userManager;
        }

        public int OwnerOrgId()
        {
            int orgid = 0;
            string username = Session["username"].ToString();
            string designation = Session["designation"].ToString();
            if (designation == "Organization Owner" || designation=="Manager")
            {
                var uerlist = userManager.GetAll().Where(c => c.UserName == username && c.Designation==designation);

                foreach (var t in uerlist)
                {
                    orgid = t.OrganizationId;
                }
            }
            return orgid;
        }

        // Create Organization Branch
        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            int orgid = OwnerOrgId();
            if (orgid == 0)
            {
                ViewBag.orglist = organizationManager.GetAll();
                var blist = branchManager.GetSome(5);
                if (blist.Count != 0)
                {
                    ViewBag.brlist = blist.ToList();
                }
            }
            else
            {
                var list = organizationManager.GetAll().Where(c => c.Id == orgid);
                ViewBag.orglist = list.ToList();

                var blist = branchManager.GetSome(5).Where(c => c.OrganizationId == orgid).ToList();
                if (blist.Count != 0)
                {
                    ViewBag.brlist = blist.ToList();
                }
            }
            

            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            int orgid = OwnerOrgId();
            if (orgid == 0)
            {
                ViewBag.orglist = organizationManager.GetAll();
            }
            else
            {
                var list = organizationManager.GetAll().Where(c => c.Id == orgid);
                ViewBag.orglist = list.ToList();

                var blist = branchManager.GetSome(5).Where(c => c.OrganizationId == orgid).ToList();
                if (blist.Count != 0)
                {
                    ViewBag.brlist = blist.ToList();
                }
            }

            if (branchManager.IsExist(branch.ShortName, branch.OrganizationId))
            {
                ViewData["exist"] = "This Short Name Already Exist";
            }
            else
            {
                branchManager.Add(branch);
                ModelState.Clear();
                return RedirectToAction("Create", new { success = "true" });
            }
            return View(branch);
        }

        public ActionResult Branchlist()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            int id = Convert.ToInt32(Request.QueryString["delete"]);
            if (id != 0)
            {
                Branch branch = branchManager.GetById(id);
                branchManager.Delete(branch);
                return RedirectToAction("Branchlist");
            }
            return View();
        }

        // All Branches
        public JsonResult GetAllBranches()
        {
            int orgid = OwnerOrgId();
            if (orgid == 0)
            {
                var totalRecord = branchManager.GetAllBrancheswithOrganizationName().Count();
                var branchlist = branchManager.GetAllBrancheswithOrganizationName();
                var branches = branchlist.Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.ShortName,
                    c.Organization,
                    c.OrganizationId,
                    c.LocationName
                });

                
                return Json(new { data = branches }, JsonRequestBehavior.AllowGet);
            }


            else
            {
                var totalRecord = branchManager.GetAllBrancheswithOrganizationName().Where(c => c.OrganizationId == orgid).Count();
                var branchlist = branchManager.GetAllBrancheswithOrganizationName().Where(c => c.OrganizationId == orgid);
                var branches = branchlist.Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.ShortName,
                    c.Organization,
                    c.OrganizationId,
                    c.LocationName
                });

                
                return Json(new {data= branches }, JsonRequestBehavior.AllowGet);
            }
        }


        



        public JsonResult GetBranchsByOrganizationId(int? organizationId)
        {
            var branches = branchManager.GetBranchsByOrganizationId(organizationId).Select(c => new
            {
                c.Id,
                c.Name,
                c.ShortName
            });
            return Json(branches, JsonRequestBehavior.AllowGet);
        }


        // Edit Branch
        public ActionResult Edit(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            int orgid = OwnerOrgId();
            if (orgid == 0)
            {
                ViewBag.orglist = organizationManager.GetAll();
            }
            else
            {
                var list = organizationManager.GetAll().Where(c => c.Id == orgid);
                ViewBag.orglist = list.ToList();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Branch branch = branchManager.GetById(id);
            
            if (branch == null)
            {
                return HttpNotFound();
            }
            bool s = Request.QueryString["Update"] == "true";
            if (s)
            {
                ViewData["Update"] = "Update Successfully";

            }
            return View(branch);
        }

        [HttpPost]
        public ActionResult Edit(int? id, Branch branch)
        {
            int orgid = OwnerOrgId();
            if (orgid == 0)
            {
                ViewBag.orglist = organizationManager.GetAll();
            }
            else
            {
                var list = organizationManager.GetAll().Where(c => c.Id == orgid);
                ViewBag.orglist = list.ToList();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (branchManager.IsExistUpdate(branch.ShortName, branch.Id,branch.OrganizationId))
            {
                ViewData["exist"] = "Code Already Exist";
            }
            else
            {
                branchManager.Update(branch);
                return RedirectToAction("Edit", new { Update = "true" });
            }
            return View(branch);
        }
        
    }
}