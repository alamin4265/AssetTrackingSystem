using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class Category
    {
        public int Id { set; get; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name Required")]
        public string Name { set; get; }

        [DisplayName("Category Code")]
        [Required(ErrorMessage = "Category Code Required")]
        public string Code { set; get; }

        public string Description { get; set; }

        [DisplayName("General Category Name")]
        [Required(ErrorMessage = "General Category Required")]
        public int GeneralCategoryId { set; get; }

        public virtual GeneralCategory GeneralCategory { set; get; }

    }
}