using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class LoginUser
    {
        public int Id { get; set; }

        [DisplayName("UserName")]
        [Required(ErrorMessage = "UserName Can not be empty")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password Can not be empty")]
        public string Password { get; set; }
    }
}