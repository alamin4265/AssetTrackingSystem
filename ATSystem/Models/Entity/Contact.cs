using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "OrganizationName Required")]
        public string OrganizationName { get; set; }

        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "MobileNo Required")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Subject Required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Message Required")]
        public string Message { get; set; }
    }
}