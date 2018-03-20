using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATSystem.Models.ViewModel.Movement
{
    public class MovementEntryVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date Required")]
        public string RegistrationDate { get; set; }

        [Required (ErrorMessage = "Asset Required")]
        public int AssetId { get; set; }
        public string Code { get; set; }
        public string SerialNo { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryNme { get; set; }
        public int GeneralCategoryId { get; set; }
        public string GeneralCategoryName { get; set; }
        [Required(ErrorMessage = "Organization Required")]
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        [Required(ErrorMessage = "Branch Required")]
        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public string MoveBy { get; set; }

        public List<SelectListItem> AssetLookUp { get; set; }
        public List<SelectListItem> OrganizationLookUp { get; set; }
    }
}