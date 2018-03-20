using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;

namespace ATSystem.BAL
{
    public class OrganizationManager:IOrganizationManager
    {
        private IOrganizationRepository repository;
        public OrganizationManager(IOrganizationRepository _repository)
        {
            repository = _repository;
        }

        public OrganizationManager()
        {
            throw new NotImplementedException();
        }

        public bool Add(Organization entity)
        {
            return repository.Add(entity);
        }

        public bool Update(Organization entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(Organization entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<Organization> GetAll()
        {
            return repository.GetAll();
        }

        public Organization GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<Organization> Get(Expression<Func<Organization, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public bool IsExist(string code)
        {
            return repository.IsExist(code);
        }
        public ICollection<Organization> GetSome(int n)
        {
            return repository.GetSome(n);
        }

        public bool IsExistUpdate(string code, int? id)
        {
            return repository.IsExistUpdate(code, id);
        }

    }
}