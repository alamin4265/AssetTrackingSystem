using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.ProductCategory;

namespace ATSystem.BAL
{
    public class ProductCategoryManager:IProductCategoryManager
    {
        private IProductCategoryRepository repository;

        public ProductCategoryManager(IProductCategoryRepository _repository)
        {
            repository = _repository;
        }

        public bool Add(ProductCategory entity)
        {
            return repository.Add(entity);
        }

        public bool Update(ProductCategory entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(ProductCategory entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<ProductCategory> GetAll()
        {
            return repository.GetAll();
        }

        public ProductCategory GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<ProductCategory> Get(Expression<Func<ProductCategory, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public ICollection<ProductCategoryVM> GetSome(int n)
        {
            return repository.GetSome(n);
        }
    }
}