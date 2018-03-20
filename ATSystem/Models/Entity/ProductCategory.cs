using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATSystem.Models.Entity
{
    public class ProductCategory
    {
        public int Id { set; get; }

        [DisplayName("Product Category Name")]
        [Required(ErrorMessage = "Product Category Required")]
        public string Name { set; get; }

        [DisplayName("Brand/Sub-Category")]
        [Required(ErrorMessage = "Brand Required")]
        public int BrandId { set; get; }

        public virtual Brand Brand { get; set; }

    }
}