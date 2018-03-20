using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.ViewModel.ResetPassword
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Last Password Required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "Password Length Must Be 8 Character Long")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Required")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match")]
        public virtual string ConfirmPassword { get; set; }
    }
}