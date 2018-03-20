using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.ViewModel.ProductCategory;

namespace ATSystem.Models.Interface.BLL
{
    public interface IProductCategoryManager:IManager<ProductCategory>
    {
        ICollection<ProductCategoryVM> GetSome(int n);
    }
}
