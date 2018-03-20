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
    public class UserManager:IUserManager
    {
        private IUserRepository repository;
        public UserManager(IUserRepository _repository)
        {
            repository = _repository;
        }

        public UserManager()
        {
        }

        public bool Add(User entity)
        {
            return repository.Add(entity);
        }

        public bool Update(User entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(User entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<User> GetAll()
        {
            return repository.GetAll();
        }

        public User GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<User> Get(Expression<Func<User, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public bool IsExistUserName(string username)
        {
            return repository.IsExistUserName(username);
        }

        public bool IsExistUserNamePassword(string username, string password)
        {
            return repository.IsExistUserNamePassword(username, password);
        }
    }
}