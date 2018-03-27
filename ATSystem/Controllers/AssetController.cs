using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.ViewModel.Asset;
using ATSystem.Models.ViewModel.AssetRegistration;
using AutoMapper;

namespace ATSystem.Controllers
{
    public class AssetController : Controller
    {
        private IGeneralCategoryManager generalCategoryManager;
        private IAssetManager assetManager;
        private IBrandManager brandManager;
        private INewAssetManager newAssetManager;

        public AssetController(IGeneralCategoryManager _generalCategoryManager, IAssetManager _assetManager, IBrandManager _brandManager, INewAssetManager _newAssetManager)
        {
            generalCategoryManager = _generalCategoryManager;
            assetManager = _assetManager;
            brandManager = _brandManager;
            newAssetManager = _newAssetManager;
        }

        public ActionResult Entry()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            ViewBag.orglist = generalCategoryManager.GetAll();

            ViewBag.assetlist = assetManager.GetSome(5);
            bool s = Request.QueryString["success"] == "true";
            if (s)
            {
                ViewData["success"] = "Save Successfully";
            }
            
            return View();
        }

        [HttpPost]
        
        public ActionResult Entry(AssetEntryVm assetEntryVm)
        {
            ViewBag.orglist = generalCategoryManager.GetAll();

            ViewBag.assetlist = assetManager.GetSome(5);

            var assetCodeCheck=assetManager.GetAll().Where(c => c.Code == assetEntryVm.Code).ToList();

            if (!ModelState.IsValid)
            {
                return View(assetEntryVm);
            }
            else if (assetCodeCheck.Count > 0)
            {
                ViewBag.msg = "Code Already Exist";
                return View(assetEntryVm);
            }
            else
            {
                var assetEntry = Mapper.Map<Asset>(assetEntryVm);
                assetEntry.Registered = false;
                assetEntry.Organization = Session["organizationName"].ToString();
                assetManager.Add(assetEntry);

                //var newAssetEntry = Mapper.Map<NewAsset>(assetEntryVm);
                //newAssetManager.Add(newAssetEntry);

                ModelState.Clear();
                return RedirectToAction("Entry", new {success = "true"});
            }
        }

        // Edit Asset
        public ActionResult Edit(int? id)
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            ViewBag.orglist = generalCategoryManager.GetAll();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Asset asset = assetManager.GetById(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            bool s = Request.QueryString["Update"] == "true";
            if (s)
            {
                ViewData["Update"] = "Update Successfully";

            }
            return View(asset);
        }

        [HttpPost]
        public ActionResult Edit(int? id, Asset asset)
        {
            ViewBag.orglist = generalCategoryManager.GetAll();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Asset asset=new Asset();
            //asset.Code = assetvm.Code;
            //asset.Id = assetvm.Id;
            //asset.BrandId = assetvm.BrandId;
            if (assetManager.IsExistUpdate(asset.Code, id,asset.BrandId))
            {
                ViewData["exist"] = "Code Already Exist";
            }
            else
            {
                assetManager.Update(asset);
                return RedirectToAction("Edit", new { Update = "true" });
            }

            return View(asset);
        }


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
            var result = from asset in assetManager.GetAll().Where(c => c.Id == id)
                join brand in brandManager.GetAll()
                on asset.BrandId equals brand.Id
                select new
                {
                    id = asset.Id,
                    name = asset.Name,
                    code = asset.Code,
                    price = asset.Price,
                    serialno = asset.SerialNo,
                    description = asset.Description,
                    brandname = brand.Name
                };
            AssetDetailsVM assetDetailsVm=new AssetDetailsVM();
            foreach (var t in result)
            {
                assetDetailsVm.Id = t.id;
                assetDetailsVm.Name = t.name;
                assetDetailsVm.Code = t.code;
                assetDetailsVm.Price = t.price;
                assetDetailsVm.SerialNo = t.serialno;
                assetDetailsVm.Description = t.description;
                assetDetailsVm.BrandName = t.brandname;
            }
            if (assetDetailsVm == null)
            {
                return HttpNotFound();
            }

            
            return View(assetDetailsVm);
        }




        public ActionResult AssetList()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            int id = Convert.ToInt32(Request.QueryString["delete"]);
            if (id != 0)
            {
                Asset asset = assetManager.GetById(id);
                assetManager.Delete(asset);
                return RedirectToAction("AssetList");
            }

            return View();
        }
        //Json Result Return AssetList

        public JsonResult GetAllAsset()
        {
            var totalRecord = assetManager.GetAllAssetWithGeneral_Category_SubCategory_Brand_Product(Session["organizationName"].ToString()).Count;
           
            var assetlist = assetManager.GetAllAssetWithGeneral_Category_SubCategory_Brand_Product(Session["organizationName"].ToString());

            //.OrderBy(sortColumn + " " + sortColumnDir)
            var assets = assetlist.Select(c => new
            {
                c.Id,
                c.SerialNo,
                c.Name,
                c.Code,
                c.Price,
                c.GeneralCategoryName,
                c.CategoryName,
                c.BrandName,
                c.Description
            });

            

            return Json(new{data=assets}, JsonRequestBehavior.AllowGet);
        }

        //Json Result Return Selected value list

        public JsonResult GetSelectedAllCategorybyid(int? id)
        {
            var Assetlistwithallcategorybyid =
                assetManager.GetAllAssetWithGeneral_Category_SubCategory_Brand_Product(Session["organizationName"].ToString())
                    .Where(c => c.Id == id)
                    .Select(c => new
                    {
                        c.GeneralCategoryId,
                        c.GeneralCategoryName,

                        c.CategoryId,
                        c.CategoryName,

                        c.BrandId,
                        c.BrandName

                    });
            return Json(Assetlistwithallcategorybyid, JsonRequestBehavior.AllowGet);
        }



    }
}