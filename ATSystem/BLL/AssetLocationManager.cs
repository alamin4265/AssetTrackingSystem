using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;

namespace ATSystem.BAL
{
    public class AssetLocationManager:IAssetLocationManager
    {
        private IAssetLocationRepository repository;
        public AssetLocationManager(IAssetLocationRepository _repository)
        {
            repository = _repository;
        }

        public bool Add(AssetLocation entity)
        {
            return repository.Add(entity);
        }

        public bool Update(AssetLocation entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(AssetLocation entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<AssetLocation> GetAll()
        {
            return repository.GetAll();
        }

        public AssetLocation GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<AssetLocation> Get(Expression<Func<AssetLocation, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public bool IsExist(string shortname)
        {
            return repository.IsExist(shortname);
        }
    }
}