using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ATSystem.Context;
using ATSystem.DAL.BaseRepository;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Branch;

namespace ATSystem.DAL
{
    public class BranchRepository:BaseRepository<Branch>,IBranchRepository
    {
        public BranchRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        public AssetDbContext Context {
            get { return _db as AssetDbContext; }
        }

        public bool IsExist(string shortname,int id)
        {
            bool IsExist = Context.Branch.Any(c => c.ShortName == shortname && c.OrganizationId==id);
            return IsExist;
        }
        


        public ICollection<BranchlistVM> GetSome(int n)
        {
            var join = from bra in Context.Branch
                       join org in Context.Organization
                       on bra.OrganizationId equals org.Id
                       select new
                       {
                           branchname = bra.Name, branchshortname = bra.ShortName,location=bra.LocationName, organizatrionname =org.Name,branchid=bra.Id,organizationid=org.Id
                       };
            var joinlist=join.Take(n).OrderByDescending(c => c.branchid).ToList();
            
            ICollection<BranchlistVM> list=new List<BranchlistVM>();
            foreach (var t in joinlist)
            {
                BranchlistVM branches = new BranchlistVM();
                branches.Name = t.branchname;
                branches.ShortName = t.branchshortname;
                branches.Organization =t.organizatrionname;
                branches.LocationName = t.location;
                branches.OrganizationId = t.organizationid;
                list.Add(branches);
            }


            return list.ToList();
        }

        public ICollection<BranchlistVM> GetAllBrancheswithOrganizationName()
        {
            var joinlist = from bra in Context.Branch
                       join org in Context.Organization
                       on bra.OrganizationId equals org.Id
                       select new
                       {
                           branchid = bra.Id,
                           branchname = bra.Name,
                           branchshortname = bra.ShortName,
                           locationname = bra.LocationName,
                           organizatrionname = org.Name,
                           organizationid=org.Id,
                       };

            ICollection<BranchlistVM> list = new List<BranchlistVM>();
            foreach (var t in joinlist)
            {
                BranchlistVM branches = new BranchlistVM();
                branches.Id = t.branchid;
                branches.Name = t.branchname;
                branches.ShortName = t.branchshortname;
                branches.OrganizationId = t.organizationid;
                branches.Organization = t.organizatrionname;
                branches.LocationName = t.locationname;
                list.Add(branches);
            }


            return list.ToList();
        }

        public ICollection<Branch> GetBranchsByOrganizationId(int? organizationid)
        {
            var getbranch = Context.Branch.Where(c => c.OrganizationId == organizationid).ToList();

            return getbranch;
        }

        public bool IsExistUpdate(string code, int? id, int? organizationid)
        {
            bool IsExist = Context.Branch.Any(c => c.ShortName == code && c.Id != id && c.OrganizationId==organizationid);
            return IsExist;
        }

        //public BranchlistVM GetByIdVM(int? id)
        //{
        //    var join = from bra in Context.Branch
        //               join org in Context.Organization
        //               on bra.OrganizationId equals org.Id
        //               select new
        //               {
        //                   branchname = bra.Name,
        //                   branchshortname = bra.ShortName,
        //                   organizatrionname = org.Name,
        //                   branchid = bra.Id
        //               };
        //    var joinlist = join.Where(c=>c.branchid==id).OrderByDescending(c => c.branchid).ToList();

        //    BranchlistVM branches = new BranchlistVM();
        //    foreach (var t in joinlist)
        //    {
        //        branches.Name = t.branchname;
        //        branches.ShortName = t.branchshortname;
        //        branches.Organization = t.organizatrionname;
        //    }
        //    return branches;

        //}
    }
}