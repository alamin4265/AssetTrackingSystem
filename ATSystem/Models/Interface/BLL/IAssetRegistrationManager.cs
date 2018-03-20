using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.ViewModel.AssetRegistration;

namespace ATSystem.Models.Interface.BLL
{
    public interface IAssetRegistrationManager:IManager<AssetRegistration>
    {
        ICollection<AssetRegistrationCreateVM> GetAllAssetRegistrationDetailswithName();
    }
}
