using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;

namespace ATSystem.Controllers
{
    public class MovementAuthorityController : Controller
    {
        private IMovementPermisionManager movementPermisionManager;
        private IMovementManager movementManager;

        public MovementAuthorityController(IMovementPermisionManager _movementPermisionManager, IMovementManager _movementManager)
        {
            movementPermisionManager = _movementPermisionManager;
            movementManager = _movementManager;
        }
        
        public ActionResult Index()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

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