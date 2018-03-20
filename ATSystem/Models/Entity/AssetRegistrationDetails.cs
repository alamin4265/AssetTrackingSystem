using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class AssetRegistrationDetails
    {
        public int Id { get; set; }
        
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



        //public virtual Asset Asset { get; set; }
        //public virtual Organization Organization { get; set; }
        //public virtual Branch Branch { get; set; }
        public virtual AssetRegistration AssetRegistration { get; set; }
    }
}