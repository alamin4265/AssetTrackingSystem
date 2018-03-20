using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.ViewModel.AssetRegistration;

namespace ATSystem.Models.Interface.DAL
{
    public interface IAssetRegistrationDetailsRepository:IRepository<AssetRegistrationDetails>
    {
        AssetRegistrationDetails GetIdAssetId(int? id);
        List<AssetRegistrationDetails> GetAllforGraph(int id);
    }
}
