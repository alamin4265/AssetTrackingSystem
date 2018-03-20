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
    public class ContactManager:IContactManager
    {
        private IContactRepository repository;

        public ContactManager(IContactRepository _repository)
        {
            repository = _repository;
        }
        public bool Add(Contact entity)
        {
            return repository.Add(entity);
        }

        public bool Update(Contact entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(Contact entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<Contact> GetAll()
        {
            return repository.GetAll();
        }

        public Contact GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<Contact> Get(Expression<Func<Contact, bool>> predicate)
        {
            return repository.Get(predicate);
        }
    }
}