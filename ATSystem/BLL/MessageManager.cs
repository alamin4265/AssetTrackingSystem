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
    public class MessageManager:IMessageManager
    {
        private IMessageRepository repository;

        public MessageManager(IMessageRepository _repository)
        {
            repository = _repository;
        }

        public bool Add(Message entity)
        {
            return repository.Add(entity);
        }

        public bool Update(Message entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(Message entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<Message> GetAll()
        {
            return repository.GetAll();
        }

        public Message GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<Message> Get(Expression<Func<Message, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public int MakeReadMessage(string username, bool read)
        {
            throw new NotImplementedException();
        }
    }
}