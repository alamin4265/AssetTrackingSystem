using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.BAL;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.ViewModel;
using ATSystem.Models.ViewModel.Movement;
using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ATSystem.Controllers
{
    public class MovementController : Controller
    {
        private IBranchManager branchManager;
        private IAssetManager assetManager;
        private UiLoader.UiLoader loader;
        private IBrandManager brandManager;
        private IOrganizationManager organizationManager;
        private IMovementManager movementManager;
        private IAssetRegistrationDetailsManager assetdetailsmanager;
        private IUserManager userManager;
        private IMovementPermisionManager movementPermisionManager;

        public MovementController(IAssetManager _assetManager, UiLoader.UiLoader _loader, IBrandManager _brandManager, IOrganizationManager _organizationManager, IMovementManager _movementManager, IAssetRegistrationDetailsManager _assetdetailsmanager, IUserManager _userManager, IMovementPermisionManager _movementPermisionManager, IBranchManager _branchManager)
        {
            assetManager = _assetManager;
            loader = _loader;
            brandManager = _brandManager;
            organizationManager = _organizationManager;
            movementManager = _movementManager;
            assetdetailsmanager = _assetdetailsmanager;
            userManager = _userManager;
            movementPermisionManager = _movementPermisionManager;
            branchManager = _branchManager;
        }

        public ActionResult Entry()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            var username = Session["username"].ToString();
            int orgid = 0;
            var list = userManager.GetAll().Where(c => c.UserName == username);
            foreach (var t in list)
            {
                orgid = t.OrganizationId;
            }
            ViewBag.orglist = organizationManager.GetAll();
            MovementEntryVM model = new MovementEntryVM()
            {
                OrganizationLookUp = loader.GetOrganizationByUserOrgIdSelectitems(orgid),
                //AssetLookUp = loader.GetAssetSelectListItems()
                AssetLookUp = loader.GetAssetSelectListItemsJoinwithAssetDetails(orgid)
            };

            string format = "d";
            model.RegistrationDate = DateTime.Today.ToString(format);
            ViewBag.CartList = Session["CartList"];
            return View(model);
        }

        [HttpPost]
        public ActionResult Entry(MovementEntryVM movementM)
        {

            var username = Session["username"].ToString();
            int orgid = 0;
            var list = userManager.GetAll().Where(c => c.UserName == username);
            foreach (var t in list)
            {
                orgid = t.OrganizationId;
            }

            ViewBag.orglist = organizationManager.GetAll();
            MovementEntryVM model = new MovementEntryVM()
            {
                OrganizationLookUp = loader.GetOrganizationByUserOrgIdSelectitems(orgid),
                //AssetLookUp = loader.GetAssetSelectListItems()
                AssetLookUp = loader.GetAssetSelectListItemsJoinwithAssetDetails(orgid)
            };

            string format = "d";
            model.RegistrationDate = DateTime.Today.ToString(format);
            List<MovementEntryVM> aList = (List<MovementEntryVM>)Session["CartList"];
            if (aList != null && aList.Any())
            {
                List<PdfView> pdfList = new List<PdfView>();
                foreach (var mo in aList)
                {
                    MovementEntryVM movementVM = new MovementEntryVM();
                    movementVM = mo;

                    movementVM.MoveBy = Session["username"].ToString();
                    AssetRegistrationDetails details = assetdetailsmanager.GetIdAssetId(movementVM.AssetId);
                    int detailsId = details.Id;
                    details = assetdetailsmanager.GetById(detailsId);
                    details.OrganizationId = movementVM.OrganizationId;
                    details.BranchId = movementVM.OrganizationId;

                    var movement = Mapper.Map<Movement>(movementVM);

                    MovementPermision mvp = new MovementPermision();
                    mvp.Permision = false;
                    var movementPermision = Mapper.Map<MovementPermision>(movementVM);
                    bool isSaved;
                    if (Session["Designation"].ToString() == "Manager")
                    {
                        isSaved = movementPermisionManager.Add(movementPermision);
                    }
                    else
                    {
                        isSaved = movementManager.Add(movement);
                    }




                    if (isSaved)
                    {
                        details.BranchId = movementVM.BranchId;
                        if (assetdetailsmanager.Update(details))
                        {
                            ModelState.Clear();
                            ViewBag.message = "Save Successfully";



                            PdfView aPdfView = new PdfView();

                            aPdfView.OrganizationId = movement.OrganizationId;
                            aPdfView.OrganizationName = movement.OrganizationName;
                            aPdfView.AssetId = movement.AssetId;
                            aPdfView.AssecCode = movementVM.Code;
                            aPdfView.AssetSerialNo = movementVM.SerialNo;
                            aPdfView.GeneralCategory = movementVM.GeneralCategoryName;
                            aPdfView.Category = movementVM.CategoryNme;
                            aPdfView.Brand = movementVM.BrandName;
                            aPdfView.FromBranch = movement.BranchName;
                            aPdfView.ToBranch = branchManager.GetById(movement.BranchId).Name;
                            aPdfView.MovedBy = movement.MoveBy;
                            aPdfView.Date = movement.RegistrationDate;

                            pdfList.Add(aPdfView);
                        }
                    }
                }

                GeneratePdf(pdfList, DateTime.Now.Date.ToString(), Session["username"].ToString());
                Session["Printed"] = "YES";
            }

            return RedirectToAction("Entry");
        }

        public ActionResult RePrint()
        {
            List<MovementEntryVM> aList = (List<MovementEntryVM>)Session["CartList"];
            if (aList != null && aList.Any())
            {
                List<PdfView> pdfList = new List<PdfView>();

                foreach (var mo in aList)
                {

                    PdfView aPdfView = new PdfView();

                    aPdfView.OrganizationId = mo.OrganizationId;
                    aPdfView.OrganizationName = mo.OrganizationName;
                    aPdfView.AssetId = mo.AssetId;
                    aPdfView.AssecCode = mo.Code;
                    aPdfView.AssetSerialNo = mo.SerialNo;
                    aPdfView.GeneralCategory = mo.GeneralCategoryName;
                    aPdfView.Category = mo.CategoryNme;
                    aPdfView.Brand = mo.BrandName;
                    aPdfView.FromBranch = mo.BranchName;
                    aPdfView.ToBranch = branchManager.GetById(mo.BranchId).Name;
                    aPdfView.MovedBy = mo.MoveBy;
                    aPdfView.Date = mo.RegistrationDate;

                    pdfList.Add(aPdfView);
                }
                GeneratePdf(pdfList, DateTime.Now.Date.ToString(), Session["username"].ToString());
            }
            return RedirectToAction("Entry");

        }
        public ActionResult MovementCart(MovementEntryVM movement)
        {
            List<MovementEntryVM> list = new List<MovementEntryVM>();
            if (Session["CartList"] != null)
            {
                list = (List<MovementEntryVM>)Session["CartList"];
            }

            foreach (var k in list)
            {
                if (k.AssetId == movement.AssetId)
                {
                    return RedirectToAction("Entry");
                }
            }
            list.Add(movement);

            Session["CartList"] = list;

            return RedirectToAction("Entry");
        }

        public ActionResult ItemDelete(int? Id)
        {
            List<MovementEntryVM> CartList = new List<MovementEntryVM>();
            CartList = Session["CartList"] as List<MovementEntryVM>;
            var itemToRemove = CartList.SingleOrDefault(x => x.AssetId == Convert.ToInt32(Id));
            CartList.Remove(itemToRemove);
            Session["CartList"] = CartList;
            return RedirectToAction("Entry");
        }

        public ActionResult MovementList()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            return View();
        }

        public ActionResult GeneratePdf(List<PdfView> sList, string date,string fromuser)
        {
            var document = new Document(PageSize.A4, 10, 10, 42, 35);
            PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();


            var head = new Paragraph(date + "\n\n");
            head.Alignment = Element.ALIGN_LEFT;
            document.Add(head);
            var serial = new Paragraph("MoveBy : "+fromuser);
            serial.Alignment = Element.ALIGN_LEFT;
            document.Add(serial);
            var table = new PdfPTable(4) { TotalWidth = 316f };
            var widths = new float[] { 4f, 2f, 2f, 2f};
            table.SetWidths(widths);
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 30f;
            table.SpacingAfter = 40f;
            table.DeleteBodyRows();



            
            table.AddCell("Code");
            table.AddCell("Category");
            table.AddCell("Brand");
            table.AddCell("To");
            foreach (var c in sList)
            {
                
                table.AddCell(c.AssecCode);
                table.AddCell(c.Category);
                table.AddCell(c.Brand);
                table.AddCell(c.ToBranch);
            }

            document.Add(table);

            var signeture = new Paragraph("Signature" + "                            " + "Signature\n\n" + "----------------" + "                      " + "---------------");
            signeture.Alignment = Element.ALIGN_BOTTOM;
            document.Add(signeture);
            

           


            document.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;  filename =Movement.pdf");
            Response.End();
            return RedirectToAction("Entry");
        }

        public ActionResult Clear()
        {
            Session["Printed"]="";
            Session["CartList"] = null;
            return RedirectToAction("Entry");
        }
    }
}