using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class LoginHistory
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public string Ip { get; set; }

        [Required]
        public string Time { get; set; }
    }
}