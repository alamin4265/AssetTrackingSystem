using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Context;
using ATSystem.Models.Interface.Base;

namespace ATSystem.DAL.BaseRepository
{
    public class BaseRepository<T>:IRepository<T> where T:class
    {
        protected DbContext _db;
        public BaseRepository(DbContext db)
        {
            _db = db;
        }

        private DbSet<T> Table {
            get { return _db.Set<T>(); }
        }


        public bool Add(T entity)
        {
            Table.Add(entity);
            return _db.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            Table.Attach(entity);
            _db.Entry(entity).State=EntityState.Modified;
            return _db.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            Table.Remove(entity);
            _db.Entry(entity).State=EntityState.Deleted;
            return _db.SaveChanges() > 0;
        }

        public ICollection<T> GetAll()
        {
            return Table.ToList();
        }

        public T GetById(int? id)
        {
            return Table.Find(id);
        }

        public ICollection<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Table.Where(predicate).ToList();
        }
    }
}