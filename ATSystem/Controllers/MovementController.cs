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

        public MovementController(IAssetManager _assetManager,UiLoader.UiLoader _loader, IBrandManager _brandManager, IOrganizationManager _organizationManager, IMovementManager _movementManager, IAssetRegistrationDetailsManager _assetdetailsmanager, IUserManager _userManager, IMovementPermisionManager _movementPermisionManager,IBranchManager _branchManager)
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
            MovementEntryVM model=new MovementEntryVM()
            {
                OrganizationLookUp = loader.GetOrganizationByUserOrgIdSelectitems(orgid),
                //AssetLookUp = loader.GetAssetSelectListItems()
                AssetLookUp = loader.GetAssetSelectListItemsJoinwithAssetDetails(orgid)
            };

            string format = "d";
            model.RegistrationDate = DateTime.Today.ToString(format);

            return View(model);
        }

        [HttpPost]
        public ActionResult Entry(MovementEntryVM movementVM)
        {
            var username = Session["username"].ToString();
            int orgid = 0;
            var list = userManager.GetAll().Where(c => c.UserName == username);
            foreach (var t in list)
            {
                orgid = t.OrganizationId;
            }


            movementVM.MoveBy = Session["username"].ToString();
            ViewBag.orglist = organizationManager.GetAll();
            MovementEntryVM model = new MovementEntryVM()
            {
                OrganizationLookUp = loader.GetOrganizationByUserOrgIdSelectitems(orgid),
                //AssetLookUp = loader.GetAssetSelectListItems()
                AssetLookUp = loader.GetAssetSelectListItemsJoinwithAssetDetails(orgid)
            };

            string format = "d";
            model.RegistrationDate = DateTime.Today.ToString(format);

            AssetRegistrationDetails details = assetdetailsmanager.GetIdAssetId(movementVM.AssetId);
            int detailsId = details.Id;
            details = assetdetailsmanager.GetById(detailsId);
            details.OrganizationId = movementVM.OrganizationId;
            details.BranchId = movementVM.OrganizationId;

        var movement = Mapper.Map<Movement>(movementVM);

            MovementPermision mvp=new MovementPermision();
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
                    aPdfView.MovedBy =movement.MoveBy;
                    aPdfView.Date = movement.RegistrationDate;

                    GeneratePdf(aPdfView);
                }
            }
            return View(model);
        }

        public ActionResult MovementList()
        {
            if (Session["username"] == null)
            {
                return RedirectToAction("HomePage", "Home");
            }

            return View();
        }

        public void GeneratePdf(PdfView sList)
        {
            var document = new Document(PageSize.A4, 10, 10, 42, 35);
            PdfWriter.GetInstance(document, Response.OutputStream);
            document.Open();


            var head = new Paragraph(sList.Date+"\n"+sList.OrganizationName+"\n\n");
            head.Alignment = Element.ALIGN_LEFT;
            document.Add(head);

            //var date = new Paragraph(sList.Date);
            //date.Alignment = Element.ALIGN_RIGHT;
            //document.Add(date);

            var info = new Paragraph("Asset Code : "+sList.AssecCode+"\n"+ "Serial No : " + sList.AssetSerialNo + "\n" + "General Category: " + sList.GeneralCategory + "\n" + "Category     : " + sList.Category + "\n" + "Brand     : " + sList.Brand + "\n\n");
            info.Alignment = Element.ALIGN_LEFT;
            document.Add(info);

            var move=new Paragraph("This Item Movement \n"+"From"+"                                      "+"To\n"+ sList.FromBranch + "                     " + sList.ToBranch+"\n");
            move.Alignment = Element.ALIGN_LEFT;
            document.Add(move);

            var signeture = new Paragraph("Signature"+"                            "+ "Signature\n\n"+ "----------------" + "                      " + "---------------");
            signeture.Alignment = Element.ALIGN_LEFT;
            document.Add(signeture);
            //var serial = new Paragraph(sList.AssetSerialNo);
            //serial.Alignment = Element.ALIGN_LEFT;
            //document.Add(serial);

            //var gCategory = new Paragraph(sList.GeneralCategory);
            //gCategory.Alignment = Element.ALIGN_LEFT;
            //document.Add(gCategory);

            //var category = new Paragraph(sList.Category);
            //category.Alignment = Element.ALIGN_CENTER;
            //document.Add(category);

            //var brand = new Paragraph(sList.Brand);
            //brand.Alignment = Element.ALIGN_CENTER;
            //document.Add(brand);

            //var From = new Paragraph("From");
            //From.Alignment = Element.ALIGN_LEFT;
            //document.Add(From);

            //var FBranch = new Paragraph("the<\n>"+sList.FromBranch);
            //FBranch.Alignment = Element.ALIGN_LEFT;
            //document.Add(FBranch);

            //var To = new Paragraph("To");
            //To.Alignment = Element.ALIGN_RIGHT;
            //document.Add(To);

            //var TBranch = new Paragraph(sList.ToBranch);
            //TBranch.Alignment = Element.ALIGN_RIGHT;
            //document.Add(TBranch);



            //var newline = new Paragraph("\n");
            //document.Add(newline);


            //create pdf table
            //var table = new PdfPTable(5) { TotalWidth = 316f };
            //var widths = new float[] { 4f, 2f, 2f, 2f, 2f };
            //table.SetWidths(widths);
            //table.HorizontalAlignment = 0;
            //table.SpacingBefore = 30f;
            //table.SpacingAfter = 40f;
            //table.DeleteBodyRows();


            //double total = 0;
            //double countProduct = 0;
            //double buyPrice = 0;
            //double profit = total - buyPrice;


            //table.AddCell("Product Name");
            //table.AddCell("Code");
            //table.AddCell("Price");
            //table.AddCell("Quantity");
            //table.AddCell("Sub Total");
            //table.AddCell("Warrenty");
            //foreach (var c in sList)
            //{
            //    table.AddCell(c.Product.Name);
            //    table.AddCell(c.Product.Code);
            //    table.AddCell(c.Product.SellPrice.ToString());
            //    table.AddCell(c.Quantity.ToString());
            //    double sub = c.Product.SellPrice * c.Quantity;
            //    table.AddCell(sub.ToString());

            //    total += sub;
            //    countProduct += c.Quantity;
            //    buyPrice += c.Product.BuyPrice;
            //}

            //string pDate = DateTime.Now.ToString();
            //table.HorizontalAlignment = Element.ALIGN_CENTER;
            //document.Add(table);

            document.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;  filename =Movement.pdf");
            Response.End();
        }
    }
}