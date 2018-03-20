using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class ForgetPassword
    {
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
    }
}