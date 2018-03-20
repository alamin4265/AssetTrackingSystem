using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.ViewModel.Movement;

namespace ATSystem.Models.Interface.BLL
{
    public interface IMovementManager:IManager<Movement>
    {
        ICollection<MovementEntryVM> GetAssetDetailsWithAllName();
    }
}
