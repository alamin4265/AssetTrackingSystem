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
    public class EmployeeManager:IEmployeeManager
    {
        private IEmployeeRepository repository;

        public EmployeeManager(IEmployeeRepository _repository)
        {
            repository = _repository;
        }

        public EmployeeManager()
        {
        }

        public bool Add(Employee entity)
        {
            return repository.Add(entity);
        }

        public bool Update(Employee entity)
        {
            return repository.Update(entity);
        }

        public bool Delete(Employee entity)
        {
            return repository.Delete(entity);
        }

        public ICollection<Employee> GetAll()
        {
            return repository.GetAll();
        }

        public Employee GetById(int? id)
        {
            return repository.GetById(id);
        }

        public ICollection<Employee> Get(Expression<Func<Employee, bool>> predicate)
        {
            return repository.Get(predicate);
        }
    }
}