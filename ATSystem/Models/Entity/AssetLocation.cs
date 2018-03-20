using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class AssetLocation
    {
        public int Id { get; set; }

        [DisplayName("Location Name")]
        [Required(ErrorMessage = "Location Name Required")]
        public string Name { get; set; }

        [DisplayName("Location Short Name")]
        [Required(ErrorMessage = "Location Short Name Required")]
        public string ShortName { get; set; }

        [DisplayName("Branch")]
        [Required]
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }
    }
}