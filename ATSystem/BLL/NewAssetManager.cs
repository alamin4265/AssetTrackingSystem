using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;

namespace ATSystem.BLL
{
    public class NewAssetManager:INewAssetManager
    {
        private INewAssetRepository repository;

        public NewAssetManager(INewAssetRepository _repository)
        {
            repository = _repository;
        }

        public NewAssetManager()
        {
            throw new NotImplementedException();
        }

        public bool Add(NewAsset entity)
        {
            return repository.Add(entity);
        }

        public bool Update(NewAsset entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(NewAsset entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<NewAsset> GetAll()
        {
            return repository.GetAll();
        }

        public NewAsset GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<NewAsset> Get(Expression<Func<NewAsset, bool>> predicate)
        {
            return repository.Get(predicate);
        }
    }
}