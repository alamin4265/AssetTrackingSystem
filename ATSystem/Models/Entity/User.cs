using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name Required")]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Gender Required")]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username Required")]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [StringLength(int.MaxValue,MinimumLength = 8,ErrorMessage = "Password Length Must Be 8 Character Long")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match")]
        public virtual string ConfirmPassword { get; set; }

        
        [DisplayName("Designation")]
        public string Designation  { get; set; }

        [Required(ErrorMessage = "PhoneNo Required")]
        [DisplayName("PhoneNo")]
        public string PhoneNo { set; get; }

        public bool Approve { set; get; }

        public bool AssetApprove { set; get; }

        public string Image { get; set; }

        [DisplayName("Organization")]
        [Required(ErrorMessage = "Organization Required")]
        public int OrganizationId { get; set; }

        [DisplayName("Branch")]
        [Required(ErrorMessage = "Branch Required")]
        public int BranchId { get; set; }

        //public virtual Organization Organization { set; get; }
    }
}