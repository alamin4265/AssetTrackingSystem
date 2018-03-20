using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.BAL;
using ATSystem.BLL;
using ATSystem.Models.Entity;
using ATSystem.Models.ViewModel.Movement;

namespace ATSystem.Controllers.JsonData
{
    public class JsonLoaderController : Controller
    {
        private readonly UiLoader.UiLoader loader;
        private readonly GeneralCategoryManager generalCategoryManager;
        private readonly CategoryManager categoryManager;
        private readonly BrandManager brandManager;
        private readonly AssetManager assetManager;
        private readonly AssetRegistrationManager assetRegistrationManager;
        private BranchManager branchManager;
        private MovementManager movementManager;
        private AssetRegistrationDetailsManager assetRegistrationDetailsManager;
        private OrganizationManager organizationManager;
        private UserManager userManager;
        private MovementPermisionManager movementPermisionManager;
        private LoginHistoryManager loginHistoryManager;
        private NewAssetManager newAssetManager;

     
        public JsonLoaderController(GeneralCategoryManager _generalCategoryManager, CategoryManager _categoryManager, BrandManager _brandManager, AssetRegistrationManager _assetRegistrationManager, UiLoader.UiLoader _loader, AssetManager _assetManager, BranchManager _branchManager, AssetRegistrationDetailsManager _assetRegistrationDetailsManager, OrganizationManager _organizationManager, MovementManager _movementManager, UserManager _userManager, MovementPermisionManager _movementPermisionManager, LoginHistoryManager _loginHistoryManager, NewAssetManager _newAssetManager)
        {
            loader = _loader;
            assetRegistrationManager = _assetRegistrationManager;
            assetManager = _assetManager;
            generalCategoryManager = _generalCategoryManager;
            categoryManager = _categoryManager;
            brandManager = _brandManager;
            branchManager = _branchManager;
            assetRegistrationDetailsManager = _assetRegistrationDetailsManager;
            organizationManager = _organizationManager;
            movementManager = _movementManager;
            userManager = _userManager;
            movementPermisionManager = _movementPermisionManager;
            loginHistoryManager = _loginHistoryManager;
            newAssetManager = _newAssetManager;
        }

        public JsonResult GetAssetById(int? assetId)
        {
            Asset asset = new Asset();
            if (assetId == null)
            return Json("NotFound", JsonRequestBehavior.AllowGet);

            asset = assetManager.GetById(assetId);
            if (asset == null)
            return Json("NotFound", JsonRequestBehavior.AllowGet);

            var jsonItem = new
            {
                Id = asset.Id,
                Name = asset.Name,
                Code = asset.Code,
                Price = asset.Price,
                SerialNo = asset.SerialNo,
                Description=asset.Description
            };
            return Json(jsonItem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBranchByOrganizationId(int? organizationId)
        {
            if (organizationId == null)
                return Json("NotFound", JsonRequestBehavior.AllowGet);

             var branches = branchManager.GetAll().Where(c => c.OrganizationId == organizationId).Select(c => new
            {
                c.Id,
                c.Name
            });
            return Json(branches, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAssetDetailsWithAllNameById(int? assetId)
        {
            var result = from asset in assetManager.GetAll().Where(c=>c.Id==assetId)
                        join brand in brandManager.GetAll()
                        on asset.BrandId equals brand.Id
                        join category in categoryManager.GetAll()
                        on brand.CategoryId equals  category.Id
                        join generalc in generalCategoryManager.GetAll()
                        on category.GeneralCategoryId equals generalc.Id
                select new
                {
                    assetid = asset.Id,
                    assetname = asset.Name,
                    assetcode = asset.Code,
                    assetserialno = asset.SerialNo,
                    assetprice = asset.Price,
                    assetdescription = asset.Description,
                    brandid = brand.Id,
                    brandname = brand.Name,
                    categoryid=category.Id,
                    categoryname=category.Name,
                    generalcategoryid=generalc.Id,
                    generalcategoryname=generalc.Name,
                    
                };

            var result2 = from assetdetails in assetRegistrationDetailsManager.GetAll().Where(c => c.AssetId == assetId)
                join organization in organizationManager.GetAll()
                on assetdetails.OrganizationId equals organization.Id
                join branch in branchManager.GetAll()
                on assetdetails.BranchId equals branch.Id
                select new
                {
                    organizationid = organization.Id,
                    organizationname = organization.Name,
                    branchid = branch.Id,
                    branchname = branch.Name
                };

            MovementEntryVM move=new MovementEntryVM();
            foreach (var t in result)
            {
                move.AssetId = t.assetid;
                move.Code = t.assetcode;
                move.Price = t.assetprice;
                move.SerialNo = t.assetserialno;
                move.Description = t.assetdescription;
                move.BrandName = t.brandname;
                move.CategoryId = t.categoryid;
                move.CategoryNme = t.categoryname;
                move.GeneralCategoryId = t.generalcategoryid;
                move.GeneralCategoryName = t.generalcategoryname;
                
            }
            foreach (var t in result2)
            {
                move.OrganizationId = t.organizationid;
                move.OrganizationName = t.organizationname;
                move.BranchId = t.branchid;
                move.BranchName = t.branchname;
            }
            var jsonItem = new
            {
                Id = move.AssetId,
                Code = move.Code,
                Price = move.Price,
                SerialNo = move.SerialNo,
                Description = move.Description,
                BrandName = move.BrandName,
                CategoryName = move.CategoryNme,
                GeneralCategoryName = move.GeneralCategoryName,
                OrganizationId=move.OrganizationId,
                OrganizationName=move.OrganizationName,
                BranchId=move.BranchId,
                BranchName=move.BranchName
                
            };
            return Json(jsonItem, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAllMovementListWithName()
        {
            
            var result = from move in movementManager.GetAll()
                         join asset in assetManager.GetAll()
                         on move.AssetId equals asset.Id
                         join org in organizationManager.GetAll()
                         on move.OrganizationId equals org.Id
                         join branch in branchManager.GetAll()
                         on move.BranchId equals branch.Id
                         select new
                         {
                             id=move.Id,
                             date = move.RegistrationDate,
                             asset = asset.Name,
                             fromOrg = move.OrganizationName,
                             frombranch = move.BranchName,
                             toorg = org.Name,
                             tobranch = branch.Name,
                             moveby = move.MoveBy
                         };

            var jsonitem = result.Select(c=> new
            {
                c.id,
                c.date,
                c.asset,
                c.fromOrg,
                c.frombranch,
                c.toorg,
                c.tobranch,
                c.moveby
            });
            
            return Json(new { data = jsonitem }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAllMovementPermisionListWithName()
        {

            var result = from move in movementPermisionManager.GetAll().Where(c=>c.Permision==false)
                         join asset in assetManager.GetAll()
                         on move.AssetId equals asset.Id
                         join org in organizationManager.GetAll()
                         on move.OrganizationId equals org.Id
                         join branch in branchManager.GetAll()
                         on move.BranchId equals branch.Id
                         select new
                         {
                             id=move.Id,
                             date = move.RegistrationDate,
                             asset = asset.Name,
                             fromOrg = move.OrganizationName,
                             frombranch = move.BranchName,
                             toorg = org.Name,
                             tobranch = branch.Name,
                             moveby = move.MoveBy,
                             permision=move.Permision
                         };
          
            var jsonitem = result.Select(c => new
            {
                c.id,
                c.date,
                c.asset,
                c.fromOrg,
                c.frombranch,
                c.toorg,
                c.tobranch,
                c.moveby,
                c.permision
            });

            return Json(new { data = jsonitem }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetAllMovementPermisionListWithStatus()
        {

            var result = from move in movementPermisionManager.GetAll().Where(c => c.Permision == false)
                         join asset in assetManager.GetAll()
                         on move.AssetId equals asset.Id
                         join org in organizationManager.GetAll()
                         on move.OrganizationId equals org.Id
                         join branch in branchManager.GetAll()
                         on move.BranchId equals branch.Id
                         select new
                         {
                             id = move.Id,
                             date = move.RegistrationDate,
                             asset = asset.Name,
                             fromOrg = move.OrganizationName,
                             frombranch = move.BranchName,
                             toorg = org.Name,
                             tobranch = branch.Name,
                             moveby = move.MoveBy,
                             permision = move.Permision
                         };

            var jsonitem = result.Select(c => new
            {
                c.id,
                c.date,
                c.asset,
                c.fromOrg,
                c.frombranch,
                c.toorg,
                c.tobranch,
                c.moveby,
                c.permision
            });

            return Json(new { data = jsonitem }, JsonRequestBehavior.AllowGet);
        }





        public JsonResult GetAllBranchManagers()
        {
            var list = userManager.GetAll().Where(c => c.UserName == Session["username"].ToString()).ToList();
            int orgid = 0;
            int id = 0;
            foreach (var k in list)
            {
                orgid = k.OrganizationId;
                id = k.Id;
            }

            var result = from user in userManager.GetAll().Where(c => c.Id != id && c.OrganizationId == orgid && c.Designation == "Manager")
            join org in organizationManager.GetAll()
                on user.OrganizationId equals org.Id
                join branch in branchManager.GetAll()
                on user.BranchId equals branch.Id
                select new
                {
                    id=user.Id,
                    fullName=user.FullName,
                    gender=user.Gender,
                    email=user.Email,
                    username=user.UserName,
                    password=user.Password,
                    designation=user.Designation,
                    phoneno=user.PhoneNo,
                    movemnetApprove=user.Approve,
                    assetApprove=user.AssetApprove,
                    organization=org.Name,
                    branch=branch.Name
                };

            
            var jsonData = result.Select(c => new
            {
                c.id,
                c.fullName,
                c.gender,
                c.email,
                c.username,
                c.password,
                c.designation,
                c.phoneno,
                c.movemnetApprove,
                c.assetApprove,
                c.organization,
                c.branch
            });
            return Json(new { data = jsonData }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetAllAssetRegistrationListWithName(int? id)
        {
            var userlist = userManager.GetAll().Where(c => c.UserName == Session["username"].ToString()).ToList();
            int orgid = 0;
            foreach (var t in userlist)
            {
                orgid = t.OrganizationId;
            }
            if (id != null)
            {
                var result = from move in assetRegistrationDetailsManager.GetAll().Where(c => c.OrganizationId == orgid && c.BranchId == id)
                             join asset in assetManager.GetAll()
                             on move.AssetId equals asset.Id
                             join org in organizationManager.GetAll()
                             on move.OrganizationId equals org.Id
                             join branch in branchManager.GetAll()
                             on move.BranchId equals branch.Id
                             select new
                             {
                                 id = move.Id,
                                 asset = asset.Name,
                                 code = move.Code,
                                 serialNo = move.SerialNo,
                                 registrationNo = move.RegistrationNo,
                                 organization = org.Name,
                                 branch = branch.Name,
                                 assetregId = move.AssetRegistration.Id
                             };

                var jsonitem = result.Select(c => new
                {
                    c.id,
                    c.asset,
                    c.code,
                    c.serialNo,
                    c.registrationNo,
                    c.organization,
                    c.branch,
                    c.assetregId
                });
                return Json(new { data = jsonitem }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = from move in assetRegistrationDetailsManager.GetAll().Where(c => c.OrganizationId == orgid)
                             join asset in assetManager.GetAll()
                             on move.AssetId equals asset.Id
                             join org in organizationManager.GetAll()
                             on move.OrganizationId equals org.Id
                             join branch in branchManager.GetAll()
                             on move.BranchId equals branch.Id
                             select new
                             {
                                 id = move.Id,
                                 asset = asset.Name,
                                 code = move.Code,
                                 serialNo = move.SerialNo,
                                 registrationNo = move.RegistrationNo,
                                 organization = org.Name,
                                 branch = branch.Name,
                                 assetregId = move.AssetRegistration.Id
                             };

                var jsonitem = result.Select(c => new
                {
                    c.id,
                    c.asset,
                    c.code,
                    c.serialNo,
                    c.registrationNo,
                    c.organization,
                    c.branch,
                    c.assetregId
                });
                return Json(new { data = jsonitem }, JsonRequestBehavior.AllowGet);
            }

            
        }


        public JsonResult GetAllUsers()
        {
            var result = userManager.GetAll().OrderBy(c => c.FullName).Where(c => c.UserName!=Session["username"].ToString()).ToList();
          var jsonData = result.Select(c => new
            {
              
                c.Id,
                c.FullName,
                c.Gender,
                c.Email,
                c.UserName,
                c.Password,
                c.Designation,
                c.PhoneNo,
                c.Approve,                
            });
            return Json(new { data = jsonData}, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetAllUsersHistory()
        {
            var result = from user in userManager.GetAll()
                join lhistory in loginHistoryManager.GetAll()
                on user.UserName equals lhistory.UserName
                select new
                {
                    name=user.FullName,
                    email=user.Email,
                    phoneno=user.PhoneNo,
                    ip=lhistory.Ip,
                    time=lhistory.Time
                };
            
            var jsonData = result.Select(c => new
            {
                c.name,
                c.email,
                c.phoneno,
                c.ip,
                c.time
            });
            return Json(new { data = jsonData }, JsonRequestBehavior.AllowGet);
        }


        //Get All Branch Home Page

        public JsonResult GetAllBranches()
        {
            int orgid = 0;
            string username = Session["username"].ToString();
            string designation = Session["designation"].ToString();
            if (designation == "Organization Owner" || designation == "Manager")
            {
                var uerlist = userManager.GetAll().Where(c => c.UserName == username && c.Designation == designation);

                foreach (var t in uerlist)
                {
                    orgid = t.OrganizationId;
                }
            }
            //int orgid = OwnerOrgId();

            if (orgid == 0)
            {
                //var totalRecord = branchManager.GetAllBrancheswithOrganizationName().Count();
               // var branchlist = branchManager.GetAllBrancheswithOrganizationName();
                var result = from user in userManager.GetAll()
                             join Branch in branchManager.GetAll()
                             on user.BranchId equals Branch.Id
                             select new
                             {
                                 Name = user.FullName,
                                 Email = user.Email,
                                 Phoneno = user.PhoneNo,
                                 Id=Branch.Id,
                                 BranchName=Branch.Name,
                                 ShortName=Branch.ShortName,
                                 
                                 OrganizationId=Branch.OrganizationId,
                                 LocationName=Branch.LocationName
                             };
                var branches = result.Select(c => new
                {
                    c.Id,
                    c.BranchName,
                    c.ShortName,
                    
                    c.OrganizationId,
                    c.LocationName,
                    c.Name,
                    c.Email,
                    c.Phoneno

                });


                return Json(new { data = branches }, JsonRequestBehavior.AllowGet);
            }


            else
            {
                //var totalRecord = branchManager.GetAllBrancheswithOrganizationName().Where(c => c.OrganizationId == orgid).Count();
               // var branchlist = branchManager.GetAllBrancheswithOrganizationName().Where(c => c.OrganizationId == orgid);
               var result = from user in userManager.GetAll().Where(c=>c.Designation=="Manager" && c.OrganizationId==orgid)
                             join Branch in branchManager.GetAll()
                             on user.BranchId equals Branch.Id
                             select new
                             {
                                 Name = user.FullName,
                                 Email = user.Email,
                                 Phoneno = user.PhoneNo,
                                 Id = Branch.Id,
                                 BranchName = Branch.Name,
                                 ShortName = Branch.ShortName,
                                 OrganizationId = Branch.OrganizationId,
                                 LocationName = Branch.LocationName
                             };
                var branches = result.Select(c => new
                {
                    c.Id,
                    c.BranchName,
                    c.ShortName,
                    c.OrganizationId,
                    c.LocationName,
                    c.Name,
                    c.Email,
                    c.Phoneno

                });


                return Json(new { data = branches }, JsonRequestBehavior.AllowGet);
            }
        }


    }

}