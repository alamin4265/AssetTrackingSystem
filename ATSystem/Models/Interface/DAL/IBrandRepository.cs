using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.ViewModel.Sub_Category_or_Brand;

namespace ATSystem.Models.Interface.DAL
{
    public interface IBrandRepository:IRepository<Brand>
    {
        ICollection<Brand> GetBrandssByCategoryId(int? id);
        ICollection<BrandVM> GetSome(int n);
    }
}
