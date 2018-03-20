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
    public class LoginHistoryManager:ILoginHistoryManager
    {
        private ILoginHistoryRepository repository;

        public LoginHistoryManager(ILoginHistoryRepository _repository)
        {
            repository = _repository;
        }

        public bool Add(LoginHistory entity)
        {
            return repository.Add(entity);
        }

        public bool Update(LoginHistory entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(LoginHistory entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<LoginHistory> GetAll()
        {
            return repository.GetAll();
        }

        public LoginHistory GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<LoginHistory> Get(Expression<Func<LoginHistory, bool>> predicate)
        {
            return repository.Get(predicate);
        }
    }
}