using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.ViewModel.Category;

namespace ATSystem.Models.Interface.BLL
{
    public interface ICategoryManager:IManager<Category>
    {
        ICollection<CategoryVM> GetSome(int n);
        ICollection<Category> GetBranchsByOrganizationId(int? id);
        ICollection<CategoryVM> GetAllCategorywithGeneralCategoryName();

        bool IsExistUpdate(string code, int? id, int? generalcategoryid);
    }
}
