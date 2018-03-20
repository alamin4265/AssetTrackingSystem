using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ATSystem.Context;
using ATSystem.DAL.BaseRepository;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Sub_Category_or_Brand;

namespace ATSystem.DAL
{
    public class BrandRepository:BaseRepository<Brand>,IBrandRepository
    {
        public BrandRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        public AssetDbContext Context {
            get {return _db as AssetDbContext;}
        }

        public ICollection<Brand> GetBrandssByCategoryId(int? id)
        {
            return Context.Brand.Where(c => c.CategoryId == id).ToList();
        }

        public ICollection<BrandVM> GetSome(int n)
        {
            var blist= Context.Brand.Take(n).OrderByDescending(c => c.Name).ToList();
            List<BrandVM> list=new List<BrandVM>();
            foreach (var t in blist)
            {
                BrandVM b=new BrandVM();
                b.Id = t.Id;
                b.Name = t.Name;
                b.Code = t.Code;
                b.CategoryId = t.CategoryId;
                list.Add(b);
            }
            return list.ToList();
        }
    }
}