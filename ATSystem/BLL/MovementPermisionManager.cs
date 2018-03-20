using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Movement;

namespace ATSystem.BLL
{
    public class MovementPermisionManager:IMovementPermisionManager
    {
        private IMovementPermisionRepository repository;

        public MovementPermisionManager(IMovementPermisionRepository _repository)
        {
            repository = _repository;
        }

        public bool Add(MovementPermision entity)
        {
            return repository.Add(entity);
        }

        public bool Update(MovementPermision entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(MovementPermision entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<MovementPermision> GetAll()
        {
            return repository.GetAll();
        }

        public MovementPermision GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<MovementPermision> Get(Expression<Func<MovementPermision, bool>> predicate)
        {
            return repository.Get(predicate);
        }
    }
}