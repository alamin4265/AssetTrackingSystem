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
    public class AssetRegistrationDetailsManager:IAssetRegistrationDetailsManager
    {
        private IAssetRegistrationDetailsRepository repository;

        public AssetRegistrationDetailsManager(IAssetRegistrationDetailsRepository _repository)
        {
            repository = _repository;
        }

        public AssetRegistrationDetailsManager()
        {
            throw new NotImplementedException();
        }

        public bool Add(AssetRegistrationDetails entity)
        {
            return repository.Add(entity);
        }

        public bool Update(AssetRegistrationDetails entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(AssetRegistrationDetails entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<AssetRegistrationDetails> GetAll()
        {
            return repository.GetAll();
        }

        public AssetRegistrationDetails GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<AssetRegistrationDetails> Get(Expression<Func<AssetRegistrationDetails, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public AssetRegistrationDetails GetIdAssetId(int? id)
        {
            return repository.GetIdAssetId(id);
        }

        public List<AssetRegistrationDetails> GetAllforGraph(int id)
        {
            return repository.GetAllforGraph(id);
        }
    }
}