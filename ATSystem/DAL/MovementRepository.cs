using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using ATSystem.Context;
using ATSystem.DAL.BaseRepository;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Movement;

namespace ATSystem.DAL
{
    public class MovementRepository:BaseRepository<Movement>,IMovementRepository
    {
        public MovementRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        public AssetDbContext Context { get { return _db as AssetDbContext;} }

        public ICollection<MovementEntryVM> GetAssetDetailsWithAllName()
        {
            //var joinlist = from asset in Context.Asset
            //               join b in Context.Brand
            //               on asset.BrandId equals b.Id
            //               join c in Context.Category
            //               on b.CategoryId equals c.Id
            //               join gc in Context.GeneralCategory
            //               on c.GeneralCategoryId equals gc.Id
            //               select new
            //               {
            //                   assetId = asset.Id,
            //                   code = asset.Code,
            //                   serialNo = asset.SerialNo,
            //                   price = asset.Price,
            //                   description = asset.Description,
            //                   brandId = b.Id,
            //                   brandName = b.Name,
            //                   categoryId = c.Id,
            //                   categoryName = c.Name,
            //                   generalcategoryId = gc.Id,
            //                   generalcategoryName = gc.Name
            //               };

            //ICollection<MovementEntryVM> list = new List<MovementEntryVM>();
            //foreach (var t in joinlist)
            //{
            //    MovementEntryVM movement = new MovementEntryVM();
            //    movement.AssetId = t.assetId;
            //    movement.Code = t.code;
            //    movement.SerialNo = t.serialNo;
            //    movement.Price = t.price;
            //    movement.Description = t.description;
            //    movement.BrandId = t.brandId;
            //    movement.BrandName = t.brandName;
            //    movement.CategoryId = t.categoryId;
            //    movement.CategoryNme = t.categoryName;
            //    movement.GeneralCategoryId = t.generalcategoryId;
            //    movement.GeneralCategoryName = t.generalcategoryName;
            //    list.Add(movement);
            //}

            
            return null;
        }
    }
}