using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ATSystem.Context;
using ATSystem.DAL.BaseRepository;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.Base;
using ATSystem.Models.Interface.DAL;

namespace ATSystem.DAL
{
    public class OrganizationRepository:BaseRepository<Organization>,IOrganizationRepository
    {

        public OrganizationRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }
        public AssetDbContext context
        { get
            { return _db as AssetDbContext; }
        }

        public bool IsExist(string code)
        {
            bool IsExist = context.Organization.Any(c => c.Code == code);
            return IsExist;
        }
        public bool IsExistUpdate(string code, int? id)
        {
            bool IsExist = context.Organization.Any(c => c.Code == code && c.Id != id);
            return IsExist;
        }
        public ICollection<Organization> GetSome(int n)
        {
            return context.Organization.Take(n).OrderByDescending(c => c.Id).ToList();
        }
    }
}