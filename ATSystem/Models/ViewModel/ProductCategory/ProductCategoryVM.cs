using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ATSystem.Models.Entity;

namespace ATSystem.Models.ViewModel.ProductCategory
{
    public class ProductCategoryVM
    {
        public int Id { set; get; }

        [DisplayName("Product Category Name")]
        [Required(ErrorMessage = "Product Category Required")]
        public string Name { set; get; }

        [DisplayName("Brand/Sub-Category")]
        [Required(ErrorMessage = "Brand Required")]
        public int BrandId { set; get; }

        public string Brand { get; set; }
    }
}