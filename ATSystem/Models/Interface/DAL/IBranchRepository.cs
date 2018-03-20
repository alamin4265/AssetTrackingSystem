using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.ViewModel.Branch;

namespace ATSystem.Models.Interface.DAL
{
    public interface IBranchRepository:IRepository<Branch>
    {
        bool IsExist(string shortname,int id);
        bool IsExistUpdate(string code, int? id, int? organizationid);
        ICollection<BranchlistVM> GetSome(int n);
        ICollection<Branch> GetBranchsByOrganizationId(int? organizationid);
        ICollection<BranchlistVM> GetAllBrancheswithOrganizationName();
        //BranchlistVM GetByIdVM(int? id);
    }
}
