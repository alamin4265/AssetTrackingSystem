using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ATSystem.Context;
using ATSystem.DAL.BaseRepository;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.ProductCategory;

namespace ATSystem.DAL
{
    public class ProductCategoryRepository:BaseRepository<ProductCategory>,IProductCategoryRepository
    {
        public ProductCategoryRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        public AssetDbContext Context {
            get {return _db as AssetDbContext;}
        }

        public ICollection<ProductCategoryVM> GetSome(int n)
        {
            var plist = Context.ProductCategory.Take(n).OrderByDescending(c => c.Name).ToList();
            List<ProductCategoryVM> list = new List<ProductCategoryVM>();
            foreach (var t in plist)
            {
                ProductCategoryVM b = new ProductCategoryVM();
                b.Id = t.Id;
                b.Name = t.Name;
                b.BrandId = t.BrandId;
                list.Add(b);
            }
            return list.ToList();

        }
    }
}