using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ATSystem.Models.Entity;

namespace ATSystem.Models.ViewModel.Branch
{
    public class BranchlistVM
    {
        public int Id { get; set; }

        [DisplayName("Branch Name")]
        [Required(ErrorMessage = "Branch Name Required")]
        public string Name { get; set; }

        [DisplayName("Branch Short Name")]
        [Required(ErrorMessage = "Short Name Required")]
        public string ShortName { get; set; }

        [DisplayName("Location Name")]
        [Required(ErrorMessage = "Location Name Required")]
        public string LocationName { get; set; }

        [DisplayName("Organization")]
        [Required(ErrorMessage = "Organization Required")]
        public int OrganizationId { get; set; }

        public string Organization { get; set; }


    }
}