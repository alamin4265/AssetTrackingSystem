using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.BAL;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.ViewModel.AssetRegistration;
using AutoMapper;

namespace ATSystem.Controllers
{
    public class AssetRegistrationController : Controller
    {
        private IAssetRegistrationManager assetRegistrationManager;
        private UiLoader.UiLoader loader;
        private IOrganizationManager organizationManager;
        private IAssetRegistrationDetailsManager assetRegDetailsManager;
        private IUserManager userManager;
        private IBranchManager branchManager;
        private IAssetManager assetManager;
        private INewAssetManager newAssetManager;

        public AssetRegistrationController(IAssetRegistrationManager _manager, UiLoader.UiLoader _loader, IOrganizationManager _organizationManager, IAssetRegistrationDetailsManager _assetRegDetailsManager, IUserManager _userManager, IBranchManager _branchManager, IAssetManager _assetManager, INewAssetManager _newAssetManager)
        {
            assetRegistrationManager = _manager;
            loader = _loader;
            organizationManager = _organizationManager;
            assetRegDetailsManager = _assetRegDetailsManager;
            userManager = _userManager;
            branchManager = _branchManager;
            assetManager = _assetManager;
            newAssetManager = _newAssetManager;
        }

        public ActionResult Create()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var model = new AssetRegistrationCreateVM()
            {
                AssetLookUp = loader.GetUpdateAssetSelectListItems(),
                BranchLookUp = loader.GetBrandSelectListItems(),
            };
            if (Session["designation"].ToString() == "Organization Owner" || Session["designation"].ToString() == "Manager")
            {
                int orgid = 0;
                var list = userManager.GetAll().Where(c => c.UserName == Session["username"].ToString() && c.Designation == Session["designation"].ToString());
                foreach (var t in list)
                {
                    orgid = t.OrganizationId;
                }
                ViewBag.orglist = organizationManager.GetAll().Where(c => c.Id == orgid);
            }
            else
            {
                ViewBag.orglist = organizationManager.GetAll();
            }

            string format = "d";
            model.RegistrationDate = DateTime.Today.ToString(format);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(AssetRegistrationCreateVM model)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            
            string format = "d";
            model.RegistrationDate = DateTime.Today.ToString(format);

            var assetRegistration = Mapper.Map<AssetRegistration>(model);

            bool isSaved = assetRegistrationManager.Add(assetRegistration);

            foreach (var a in assetRegistration.AssetRegistrationDetailses)
            {
                Asset asset = new Asset();
                asset = assetManager.GetById(a.AssetId);
                asset.Registered = true;
                assetManager.Update(asset);
            }

            if (isSaved)
            {
                ModelState.Clear();
                ViewBag.message = "Save Successfully";
                //return RedirectToAction("Create");
            }



            model.AssetLookUp = loader.GetUpdateAssetSelectListItems();
            model.BranchLookUp = loader.GetBrandSelectListItems();

            model.RegisteredBy = Session["username"].ToString();
            if (Session["username"].ToString() == "Organization Owner" || Session["designation"].ToString() == "Manager")
            {
                int orgid = 0;
                var list = userManager.GetAll().Where(c => c.UserName == Session["username"].ToString() && c.Designation == Session["designation"].ToString());
                foreach (var t in list)
                {
                    orgid = t.OrganizationId;
                }
                ViewBag.orglist = organizationManager.GetAll().Where(c => c.Id == orgid);
            }
            else
            {
                ViewBag.orglist = organizationManager.GetAll();
            }


            return View(model);
        }


        //public ActionResult RegisteredAssets(int? id)
        //{
        //    if (Session["username"] == null)
        //    {
        //        return RedirectToAction("HomePage", "Home");
        //    }

        //    var data = assetRegistrationManager.GetAll();
        //    if (id == 0 || id == null)
        //    {
        //        return View(data);
        //    }
        //    else
        //    {
        //        AssetRegistration assetRegistration = assetRegistrationManager.GetById(id);
        //        if (assetRegistration != null)
        //        {
        //            AssetRegistrationDetails assetregDetails = assetRegDetailsManager.GetById(assetRegistration.Id);
        //            if (assetRegistration != null || assetregDetails != null)
        //            {
        //                assetRegDetailsManager.Delete(assetregDetails);
        //                assetRegistrationManager.Delete(assetRegistration);
        //                data = assetRegistrationManager.GetAll();
        //            }
        //        }

        //    }
        //    return View(data);
        //}

        public ActionResult RegisteredAsset(int? id)
        {
            var newasset = newAssetManager.GetAll();
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var userlist = userManager.GetAll().Where(c => c.UserName == Session["username"].ToString()).ToList();
            int orgid = 0;
            foreach (var t in userlist)
            {
                orgid = t.OrganizationId;
            }

            ViewBag.branchlist=branchManager.GetBranchsByOrganizationId(orgid).ToList();

            return View();
        }

    }
}