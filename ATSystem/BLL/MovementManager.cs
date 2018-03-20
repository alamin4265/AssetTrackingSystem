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
    public class MovementManager:IMovementManager
    {
        private IMovementRepository repository;

        public MovementManager(IMovementRepository _repository)
        {
            repository = _repository;
        }
        public bool Add(Movement entity)
        {
            return repository.Add(entity);
        }

        public bool Update(Movement entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(Movement entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<Movement> GetAll()
        {
            return repository.GetAll();
        }

        public Movement GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<Movement> Get(Expression<Func<Movement, bool>> predicate)
        {
            return repository.Get(predicate);
        }

        public ICollection<MovementEntryVM> GetAssetDetailsWithAllName()
        {
            return repository.GetAssetDetailsWithAllName();
        }
    }
}