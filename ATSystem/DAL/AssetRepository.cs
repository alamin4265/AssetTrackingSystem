using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Web;
using ATSystem.Context;
using ATSystem.DAL.BaseRepository;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Asset;

namespace ATSystem.DAL
{
    public class AssetRepository:BaseRepository<Asset>,IAssetRepository
    {
        public AssetRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        AssetDbContext Context {
            get { return _db as AssetDbContext;}
        }

        public ICollection<AssetEntryVm> GetAllAssetWithGeneral_Category_SubCategory_Brand_Product(string org)
        {
            var joinlist = from a in Context.Asset.Where(c=>c.Registered==false && c.Organization== org)
                join b in Context.Brand
                on a.BrandId equals b.Id
                join c in Context.Category
                on b.CategoryId equals c.Id
                join gc in Context.GeneralCategory
                on c.GeneralCategoryId equals gc.Id
                
                select new
                {
                    id=a.Id,
                    code=a.Code,
                    serialno=a.SerialNo,
                    name=a.Name,
                    price=a.Price,
                    description=a.Description,
                    generalcategoyid=gc.Id,
                    generalcategory=gc.Name,
                    categoryid=c.Id,
                    category=c.Name,
                    brandid=b.Id,
                    brand=b.Name
                };
            
            ICollection<AssetEntryVm> list = new List<AssetEntryVm>();
            foreach (var t in joinlist)
            {
               AssetEntryVm assetvm=new AssetEntryVm();
                assetvm.Id = t.id;
                assetvm.Code = t.code;
                assetvm.SerialNo = t.serialno;
                assetvm.Name = t.name;
                assetvm.Price = t.price;
                assetvm.Description = t.description;
                assetvm.GeneralCategoryId = t.generalcategoyid;
                assetvm.GeneralCategoryName = t.generalcategory;
                assetvm.CategoryId = t.categoryid;
                assetvm.CategoryName = t.category;
                assetvm.BrandId = t.brandid;
                assetvm.BrandName = t.brand;
                list.Add(assetvm);
            }
            return list.ToList();

        }

        public ICollection<AssetEntryVm> GetSome(int n)
        {
            var join = from a in Context.Asset.Where(c=>c.Registered==false)
                           join b in Context.Brand
                           on a.BrandId equals b.Id
                           join c in Context.Category
                           on b.CategoryId equals c.Id
                           join gc in Context.GeneralCategory
                           on c.GeneralCategoryId equals gc.Id

                           select new
                           {
                               id = a.Id,
                               code = a.Code,
                               serialno = a.SerialNo,
                               name = a.Name,
                               price = a.Price,
                               description = a.Description,
                               generalcategory = gc.Name,
                               category = c.Name,
                               brand = b.Name,
                              
                           };

            var joinlist = join.Take(n).OrderByDescending(c => c.id).ToList();
            ICollection<AssetEntryVm> list = new List<AssetEntryVm>();
            foreach (var t in joinlist)
            {
                AssetEntryVm assetvm = new AssetEntryVm();
                assetvm.Id = t.id;
                assetvm.Code = t.code;
                assetvm.SerialNo = t.serialno;
                assetvm.Name = t.name;
                assetvm.Price = t.price;
                assetvm.Description = t.description;
                assetvm.GeneralCategoryName = t.generalcategory;
                assetvm.CategoryName = t.category;
                assetvm.BrandName = t.brand;
                list.Add(assetvm);
            }
            return list.ToList();

        }

        public bool IsExistUpdate(string code, int? id, int? brandid)
        {
            bool IsExist = Context.Asset.Any(c => c.Code == code && c.Id != id && c.BrandId==brandid);
            return IsExist;
        }

        public Asset GetByName(string name)
        {
            Asset ast=new Asset();
            var assets = Context.Asset.Where(c => c.Name == name);
            foreach (var tAsset in assets)
            {
                ast.Name = tAsset.Name;
                ast.Brand = tAsset.Brand;
                ast.BrandId = tAsset.BrandId;
                ast.Code = tAsset.Code;
                ast.Description = tAsset.Description;
                ast.Id = tAsset.Id;
                ast.Price = tAsset.Price;
                ast.SerialNo = tAsset.SerialNo;
            }
            return ast;
        }
    }
}