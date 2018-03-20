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
using ATSystem.Models.ViewModel.Branch;

namespace ATSystem.DAL
{
    public class AssetRegistrationRepository:BaseRepository<AssetRegistration>,IAssetRegistrationRepository
    {
        public AssetRegistrationRepository(DbContext db) : base(new AssetDbContext())
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
        public ICollection<AssetRegistrationCreateVM> GetAllAssetRegistrationDetailswithName()
        {
            var joinlist = from ard in Context.AssetRegistrationDetail
                join a in Context.Asset
                on ard.AssetId equals a.Id
                join org in Context.Organization
                on ard.OrganizationId equals org.Id
                join b in Context.Branch
                on ard.BranchId equals b.Id
                select new
                {
                    assetId = a.Id,
                    assetName = a.Name,
                    code = ard.Code,
                    serianNo = ard.SerialNo,
                    registrationNo = ard.RegistrationNo,
                    organizationId = org.Id,
                    organizationName = org.Name,
                    brandId = b.Id,
                    brandName = b.Name,
                };

            ICollection<AssetRegistrationCreateVM> list = new List<AssetRegistrationCreateVM>();
            foreach (var t in joinlist)
            {
                AssetRegistrationCreateVM assetRegCreateVm = new AssetRegistrationCreateVM();
                assetRegCreateVm.AssetId = t.assetId;
                assetRegCreateVm.AssetName = t.assetName;
                assetRegCreateVm.Code = t.code;
                assetRegCreateVm.SerialNo = t.serianNo;
                assetRegCreateVm.RegistrationNo = t.registrationNo;
                assetRegCreateVm.OrganizationId = t.organizationId;
                assetRegCreateVm.BranchId = t.brandId;
                assetRegCreateVm.BranchName = t.brandName;
                list.Add(assetRegCreateVm);
            }

            return list.ToList();
        }

    }
}