using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.ViewModel.Asset;

namespace ATSystem.Models.Interface.DAL
{
    public interface IAssetRepository:IRepository<Asset>
    {
        ICollection<AssetEntryVm> GetAllAssetWithGeneral_Category_SubCategory_Brand_Product();

        ICollection<AssetEntryVm> GetSome(int n);

        bool IsExistUpdate(string code, int? id, int? brandid);

        Asset GetByName(string name);
    }
}
