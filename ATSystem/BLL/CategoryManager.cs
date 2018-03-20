using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Category;

namespace ATSystem.BAL
{
    public class CategoryManager:ICategoryManager
    {
        private ICategoryRepository repository;

        public CategoryManager(ICategoryRepository _repository)
        {
            repository = _repository;
        }

        public CategoryManager()
        {
        }

        public bool Add(Category entity)
        {
            return repository.Add(entity);
        }

        public bool Update(Category entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(Category entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<Category> GetAll()
        {
            return repository.GetAll();
        }

        public Category GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<Category> Get(Expression<Func<Category, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public ICollection<CategoryVM> GetSome(int n)
        {
            return repository.GetSome(n);
        }

        public ICollection<Category> GetBranchsByOrganizationId(int? id)
        {
            return repository.GetBranchsByOrganizationId(id);
        }

        public ICollection<CategoryVM> GetAllCategorywithGeneralCategoryName()
        {
            return repository.GetAllCategorywithGeneralCategoryName();
        }

        public bool IsExistUpdate(string code, int? id, int? generalcategoryid)
        {
            return repository.IsExistUpdate(code, id, generalcategoryid);
        }
    }
}