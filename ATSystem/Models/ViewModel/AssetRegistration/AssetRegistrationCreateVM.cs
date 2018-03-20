
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.Models.Entity;

namespace ATSystem.Models.ViewModel.AssetRegistration
{
    public class AssetRegistrationCreateVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Date is Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd--MM--yyyy}", ApplyFormatInEditMode = true)]
        public string RegistrationDate { get; set; }

        public string RegisteredBy { get; set; }

        [Required(ErrorMessage = "Select Asset")]
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string Code { get; set; }
        public string SerialNo { get; set; }

        [Required(ErrorMessage = "Registration No Required")]
        public string RegistrationNo { get; set; }
        [Required(ErrorMessage = "Select Organization")]
        [DisplayName("Assign To")]
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        [Required(ErrorMessage = "Select Branch")]
        public int BranchId { get; set; }
        public string BranchName { get; set; }

       
        public List<SelectListItem> OrganizationLookUp { get; set; }
        public List<SelectListItem> BranchLookUp { get; set; }
        public List<SelectListItem> AssetLookUp { get; set; }

        public ICollection<AssetRegistrationDetails> AssetRegistrationDetailses { get; set; }
    }
}