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
    public class GeneralCategoryManager:IGeneralCategoryManager
    {
        private IGeneralCategoryRepository Repository;
        public GeneralCategoryManager(IGeneralCategoryRepository _generalCategoryRepository)
        {
            Repository = _generalCategoryRepository;
        }

        public GeneralCategoryManager()
        {
        }

        public bool Add(GeneralCategory entity)
        {
            return Repository.Add(entity);
        }

        public bool Update(GeneralCategory entity)
        {
            return Repository.Update(entity);
        }

        public bool Delete(GeneralCategory entity)
        {
            return Repository.Delete(entity);
        }

        public ICollection<GeneralCategory> GetAll()
        {
            return Repository.GetAll();
        }

        public GeneralCategory GetById(int? id)
        {
            return Repository.GetById(id);
        }

        public ICollection<GeneralCategory> Get(Expression<Func<GeneralCategory, bool>> predicate)
        {
            return Repository.Get(predicate);
        }

        public bool IsExist(string code)
        {
            return Repository.IsExist(code);
        }

        public ICollection<GeneralCategory> GetSome(int n)
        {
            return Repository.GetSome(n);
        }

        public bool IsExistUpdate(string code, int? id)
        {
            return Repository.IsExistUpdate(code, id);
        }
    }
}