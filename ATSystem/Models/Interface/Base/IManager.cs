using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ATSystem.Models.Entity;

namespace ATSystem.Models.Interface.Base
{
    public interface IManager<T> where T:class 
    {
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        ICollection<T> GetAll();
        T GetById(int? id);
        ICollection<T> Get(Expression<Func<T, bool>> predicate);
    }
}
