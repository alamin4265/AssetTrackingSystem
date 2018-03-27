using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Asset;

namespace ATSystem.BAL
{
    public class AssetManager:IAssetManager
    {
        private IAssetRepository repository;

        public AssetManager(IAssetRepository _repository)
        {
            repository = _repository;
        }

        public AssetManager()
        {
        }

        public bool Add(Asset entity)
        {
            return repository.Add(entity);
        }

        public bool Update(Asset entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(Asset entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<Asset> GetAll()
        {
            return repository.GetAll();
        }

        public Asset GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<Asset> Get(Expression<Func<Asset, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public ICollection<AssetEntryVm> GetAllAssetWithGeneral_Category_SubCategory_Brand_Product(string org)
        {
            return repository.GetAllAssetWithGeneral_Category_SubCategory_Brand_Product(org);
        }

        public ICollection<AssetEntryVm> GetSome(int n)
        {
            return repository.GetSome(n);
        }

        public bool IsExistUpdate(string code, int? id, int? brandid)
        {
            return repository.IsExistUpdate(code, id, brandid);
        }

        public Asset GetByName(string name)
        {
            return repository.GetByName(name);
        }
    }
}