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
    public class UserRepository:BaseRepository<User>,IUserRepository
    {
        public UserRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        public AssetDbContext Context
        {
            get { return _db as AssetDbContext; }
        }

        public bool IsExistUserName(string username)
        {
            return Context.User.Any(c => c.UserName == username);
        }

        public bool IsExistUserNamePassword(string username, string password)
        {
            return Context.User.Any(u => u.UserName == username && u.Password == password);
        }
    }
}