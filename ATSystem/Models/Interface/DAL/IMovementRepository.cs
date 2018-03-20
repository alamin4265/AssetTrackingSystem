using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.ViewModel.Movement;

namespace ATSystem.Models.Interface.DAL
{
    public interface IMovementRepository:IRepository<Movement>
    {
        ICollection<MovementEntryVM> GetAssetDetailsWithAllName();
    }
}
