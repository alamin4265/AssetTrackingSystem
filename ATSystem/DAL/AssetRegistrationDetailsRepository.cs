using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ATSystem.Context;
using ATSystem.DAL.BaseRepository;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.AssetRegistration;

namespace ATSystem.DAL
{
    public class AssetRegistrationDetailsRepository : BaseRepository<AssetRegistrationDetails>, IAssetRegistrationDetailsRepository
    {
        public AssetRegistrationDetailsRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        public AssetDbContext Context
        {
            get
            {
                return _db as AssetDbContext;
            }
        }


        public AssetRegistrationDetails GetIdAssetId(int? id)
        {
            AssetRegistrationDetails assetdeDetails = new AssetRegistrationDetails();
            var details = Context.AssetRegistrationDetail.Where(c => c.AssetId == id);
            foreach (var t in details)
            {
                assetdeDetails.Id = t.Id;
            }
            return assetdeDetails;
        }
        public List<AssetRegistrationDetails> GetAllforGraph(int id)
        {
            var graphasset = from a in Context.AssetRegistrationDetail.Where(a => a.OrganizationId == id)
                             join o in Context.Organization on a.OrganizationId equals o.Id
                             join b in Context.Branch on a.BranchId equals b.Id
                             select new
                             {
                                 id = a.Id,
                                 orgId = a.OrganizationId,
                                 orgName = o.Name,
                                 branchid = a.BranchId,
                                 branchName = b.Name
                             };
            List<AssetRegistrationDetails> ardlist = new List<AssetRegistrationDetails>();
            foreach (var a in graphasset)
            {
                AssetRegistrationDetails d = new AssetRegistrationDetails();
                d.AssetId = a.id;
                d.OrganizationId = a.orgId;
                d.OrganizationName = a.orgName;
                d.BranchId = a.branchid;
                d.BranchName = a.branchName;
                ardlist.Add(d);
            }
            return ardlist;
        }
    }
}