using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.BAL;
using ATSystem.Models;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;

namespace ATSystem.Controllers
{
    public class MovementAuthorityController : Controller
    {
        private IMovementPermisionManager movementPermisionManager;
        private IMovementManager movementManager;
        private AssetManager assetManager;
        private OrganizationManager organizationManager;
        private BranchManager branchManager;
        private IUserManager userManager;

        public MovementAuthorityController(IMovementPermisionManager _movementPermisionManager, IMovementManager _movementManager, AssetManager _assetManager, OrganizationManager _organizationManager, BranchManager _branchManager, IUserManager _userManager)
        {
            movementPermisionManager = _movementPermisionManager;
            movementManager = _movementManager;
            assetManager = _assetManager;
            organizationManager = _organizationManager;
            branchManager = _branchManager;
            userManager = _userManager;
        }
        
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }
            var username = Session["username"].ToString();
            int branchid = 0;
            var listo = userManager.GetAll().Where(c => c.UserName == username);
            foreach (var t in listo)
            {
                branchid = t.BranchId;
            }
            var result = from move in movementPermisionManager.GetAll().Where(c => c.Permision == false && c.BranchId== branchid)
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
            List<MovementAuthorityN> list = new List<MovementAuthorityN>();
            foreach (var j in jsonitem)
            {
                MovementAuthorityN authorityN = new MovementAuthorityN();
                authorityN.id = j.id;
                authorityN.date = j.date;
                authorityN.asset = j.asset;
                authorityN.fromOrg = j.fromOrg;
                authorityN.frombranch = j.frombranch;
                authorityN.toorg = j.toorg;
                authorityN.tobranch = j.tobranch;
                authorityN.moveby = j.moveby;
                authorityN.permision = j.permision;
                list.Add(authorityN);
            }
            ViewBag.list = list;
            return View();
        }

        public ActionResult MovementAuthorityYes(int? id)
        {
            
            if (id != null)
            {
                MovementPermision movementPermision = movementPermisionManager.GetById(id);
                Movement aMovement=new Movement();
                if (movementPermision != null)
                {
                    aMovement.AssetId = movementPermision.AssetId;
                    aMovement.BranchId = movementPermision.BranchId;
                    aMovement.BranchName = movementPermision.BranchName;
                    aMovement.Id = movementPermision.Id;
                    aMovement.MoveBy = movementPermision.MoveBy;
                    aMovement.OrganizationId = movementPermision.OrganizationId;
                    aMovement.OrganizationName = movementPermision.OrganizationName;
                    aMovement.RegistrationDate = movementPermision.RegistrationDate;

                    if (movementManager.Add(aMovement))
                    {
                        movementPermision.Permision = true;
                        movementPermisionManager.Update(movementPermision);
                    }
                }

            }
            return RedirectToAction("Index");
        }


        public ActionResult MovementAuthorityNo(int? id)
        {
            if (id != null)
            {
                MovementPermision movementPermision = movementPermisionManager.GetById(id);
                if (movementPermision != null)
                {
                    movementPermisionManager.Delete(movementPermision);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Status()
        {
            return View();
        }
    }
}