using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Branch;

namespace ATSystem.BAL
{
    public class BranchManager:IBranchManager
    {
        private IBranchRepository repository;
        public BranchManager(IBranchRepository _repository)
        {
            repository = _repository;
        }
        public bool Add(Branch entity)
        {
            return repository.Add(entity);
        }

        public bool Update(Branch entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(Branch entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<Branch> GetAll()
        {
            return repository.GetAll();
        }

        public Branch GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<BranchlistVM> GetAllBrancheswithOrganizationName()
        {
            return repository.GetAllBrancheswithOrganizationName();
        }

        public ICollection<Branch> Get(Expression<Func<Branch, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public bool IsExist(string shortname,int id)
        {
            return repository.IsExist(shortname,id);
        }

        public ICollection<Branch> GetBranchsByOrganizationId(int? organizationid)
        {
            return repository.GetBranchsByOrganizationId(organizationid).ToList();
        }

        public ICollection<BranchlistVM> GetSome(int n)
        {
            return repository.GetSome(n);
        }

        public bool IsExistUpdate(string code, int? id, int? organizationid)
        {
            return repository.IsExistUpdate(code, id, organizationid);
        }

        //public BranchlistVM GetByIdVM(int? id)
        //{
        //    return repository.GetByIdVM(id);
        //}
    }
}