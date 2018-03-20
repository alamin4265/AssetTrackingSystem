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
    public class LoginUserManager:ILoginUserManager
    {
        private ILoginUserRepository repository;

        public LoginUserManager(ILoginUserRepository _repository)
        {
            repository = _repository;
        }

        public bool Add(LoginUser entity)
        {
            return repository.Add(entity);
        }

        public bool Update(LoginUser entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(LoginUser entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<LoginUser> GetAll()
        {
            return repository.GetAll();
        }

        public LoginUser GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<LoginUser> Get(Expression<Func<LoginUser, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public bool IsExistUserNamePassword(string username, string password)
        {
            return repository.IsExistUserNamePassword(username,password);
        }
    }
}