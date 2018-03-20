using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;

namespace ATSystem.Models.Interface.BLL
{
    public interface IOrganizationManager:IManager<Organization>
    {
        bool IsExist(string code);
        ICollection<Organization> GetSome(int n);
        bool IsExistUpdate(string code, int? id);
    }
}
