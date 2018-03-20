using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class GeneralCategory
    {
        public int Id { set; get; }

        [DisplayName("General Category Name")]
        [Required(ErrorMessage = "General Category Name Required")]
        public string Name { set; get; }

        [DisplayName("General Category Code")]
        [Required(ErrorMessage = "General Category Code Required")]
        [StringLength(2, ErrorMessage = "Code Must Be 2 Digits Long", MinimumLength = 2)]
        public string Code { set; get; }
    }
}