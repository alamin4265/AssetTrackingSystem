using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.AssetRegistration;

namespace ATSystem.BLL
{
    public class AssetRegistrationManager:IAssetRegistrationManager
    {
        private IAssetRegistrationRepository repository;

        public AssetRegistrationManager(IAssetRegistrationRepository _repository)
        {
            repository = _repository;
        }



        public bool Add(AssetRegistration entity)
        {
            return repository.Add(entity);
        }

        public bool Update(AssetRegistration entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(AssetRegistration entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<AssetRegistration> GetAll()
        {
            return repository.GetAll();
        }

        public AssetRegistration GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<AssetRegistration> Get(Expression<Func<AssetRegistration, bool>> predicate)
        {
            return repository.Get(predicate);
        }
        public ICollection<AssetRegistrationCreateVM> GetAllAssetRegistrationDetailswithName()
        {
            return repository.GetAllAssetRegistrationDetailswithName();
        }
    }
}