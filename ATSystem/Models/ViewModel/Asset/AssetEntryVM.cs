using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using ATSystem.Models.Entity;

namespace ATSystem.Models.ViewModel.Asset
{
    public class AssetEntryVm
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        //[Remote("IsNameExist", "Assets", ErrorMessage = "Name Already Exist.", AdditionalFields = "InitName")]
        public string Name { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 3, ErrorMessage = "Code Must Be 3-6 Characters")]
        [Index(IsUnique = true)]
        //[Remote("IsCodeExist", "Assets", ErrorMessage = "Code Already Exist.", AdditionalFields = "InitCode")]
        public virtual string Code { get; set; }

        public string CodeWithPrefix { get; set; }

        [Required]
        public double? Price { get; set; }
        public string SerialNo { get; set; }
        public string Description { get; set; }
        public int GeneralCategoryId { get; set; }
        public string GeneralCategoryName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int ProductDetailsId { get; set; }
        public string Organiztion { get; set; }

        public Models.Entity.GeneralCategory GeneralCategory { get; set; }
        public Entity.Category Category { get; set; }
        public virtual Models.Entity.Brand Brand { get; set; }

        //public List<SelectListItem> GeneralCategoryLookUp { get; set; }
        //public List<SelectListItem> CategoryLookUp { get; set; }
        //public List<SelectListItem> SubCategoryLookUp { get; set; }
    }
}