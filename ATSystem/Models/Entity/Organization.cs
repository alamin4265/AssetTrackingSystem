using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class Organization
    {
        public int Id { get; set; }

        [DisplayName("Organization Name")]
        [Required(ErrorMessage = "Organization Name Required")]
        public string Name { get; set; }

        [DisplayName("Organization Short Name")]
        [Required(ErrorMessage = "Short Name Required")]
        public string ShortName { get; set; }

        [DisplayName("Organization Code")]
        [Required(ErrorMessage = "Organization Code Required")]
        [StringLength(3, ErrorMessage = "Code Must Be 3 Digits Long",MinimumLength = 3)]
        public string Code { get; set; }

        public string Location { get; set; }

        public virtual IList<Branch> Branches { set; get; }
    }
}