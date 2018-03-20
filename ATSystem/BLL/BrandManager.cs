using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Sub_Category_or_Brand;

namespace ATSystem.BAL
{
    public class BrandManager:IBrandManager
    {
        private IBrandRepository repository;

        public BrandManager(IBrandRepository _repository)
        {
            repository = _repository;
        }

        public BrandManager()
        {
        }

        public bool Add(Brand entity)
        {
            return repository.Add(entity);
        }

        public bool Update(Brand entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(Brand entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<Brand> GetAll()
        {
            return repository.GetAll();
        }

        public Brand GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<Brand> Get(Expression<Func<Brand, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public ICollection<Brand> GetBrandssByCategoryId(int? id)
        {
            return repository.GetBrandssByCategoryId(id);
        }

        public ICollection<BrandVM> GetSome(int n)
        {
            return repository.GetSome(n);
        }
    }
}