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
    public class ContactRepository:BaseRepository<Contact>,IContactRepository
    {
        public ContactRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        AssetDbContext Context{get{return _db as AssetDbContext;}}
    }
}