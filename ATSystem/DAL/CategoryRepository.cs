using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ATSystem.Context;
using ATSystem.DAL.BaseRepository;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Category;

namespace ATSystem.DAL
{
    public class CategoryRepository:BaseRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        public AssetDbContext Context {
            get
            {
                return _db as AssetDbContext;
            }
        } 

        public ICollection<CategoryVM> GetSome(int n)
        {
            var join = from c in Context.Category
                join g in Context.GeneralCategory
                on c.GeneralCategoryId equals g.Id
                select new
                {
                    cid = c.Id,
                    cname = c.Name,
                    ccode = c.Code,
                    cdescription = c.Description,
                    gname = g.Name
                };
            var joinlist = join.Take(n).OrderByDescending(c => c.cname).ToList();
            ICollection<CategoryVM>list=new List<CategoryVM>();
            foreach (var t in joinlist)
            {
                CategoryVM categoryVm = new CategoryVM();
                categoryVm.Id = t.cid;
                categoryVm.Name = t.cname;
                categoryVm.Code = t.ccode;
                categoryVm.Description = t.cdescription;
                categoryVm.GeneralCategory = t.gname;
                list.Add(categoryVm);
            }
            return list.ToList();
            //Context.Category.Take(n).OrderByDescending(c => c.Id).ToList();
        }

        public ICollection<Category> GetBranchsByOrganizationId(int? id)
        {
            return Context.Category.Where(c => c.GeneralCategoryId == id).ToList();
        }

        public ICollection<CategoryVM> GetAllCategorywithGeneralCategoryName()
        {
            var join = from c in Context.Category
                       join g in Context.GeneralCategory
                       on c.GeneralCategoryId equals g.Id
                       select new
                       {
                           cid = c.Id,
                           cname = c.Name,
                           ccode = c.Code,
                           cdescription = c.Description,
                           gname = g.Name
                       };
            ICollection<CategoryVM> list = new List<CategoryVM>();
            foreach (var t in join)
            {
                CategoryVM categoryVm = new CategoryVM();
                categoryVm.Id = t.cid;
                categoryVm.Name = t.cname;
                categoryVm.Code = t.ccode;
                categoryVm.Description = t.cdescription;
                categoryVm.GeneralCategory = t.gname;
                list.Add(categoryVm);
            }
            return list.ToList();
        }

        public bool IsExistUpdate(string code, int? id, int? generalcategoryid)
        {
            bool IsExist = Context.Category.Any(c => c.Code == code && c.Id != id && c.GeneralCategoryId == generalcategoryid);
            return IsExist;
        }
    }
}