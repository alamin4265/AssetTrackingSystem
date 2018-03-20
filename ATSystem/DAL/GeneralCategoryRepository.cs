using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ATSystem.Context;
using ATSystem.DAL.BaseRepository;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.DAL;

namespace ATSystem.DAL
{
    public class GeneralCategoryRepository:BaseRepository<GeneralCategory>,IGeneralCategoryRepository
    {

        public GeneralCategoryRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        public AssetDbContext Context {
            get {return _db as AssetDbContext;}
        }

        public bool IsExist(string code)
        {
            bool IsExist = Context.GeneralCategory.Any(c => c.Code == code);
            return IsExist;
        }

        public ICollection<GeneralCategory> GetSome(int n)
        {
            return Context.GeneralCategory.Take(n).OrderByDescending(c => c.Id).ToList();
        }

        public bool IsExistUpdate(string code, int? id)
        {
            bool IsExist = Context.GeneralCategory.Any(c => c.Code == code && c.Id != id);
            return IsExist;
        }
    }
}