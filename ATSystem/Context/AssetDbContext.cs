using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ATSystem.Models.Entity;

namespace ATSystem.Context
{
    public class AssetDbContext:DbContext
    {
        public DbSet<Organization> Organization { set; get; }
        public DbSet<Branch> Branch { set; get; }
        public DbSet<AssetLocation> AssetLocation { set; get; }
        public DbSet<GeneralCategory> GeneralCategory { set; get; }
        public DbSet<Category> Category { set; get; }
        public DbSet<Brand> Brand { set; get; }
        public DbSet<ProductCategory> ProductCategory { set; get; }
        public DbSet<User> User { set; get; }
        public DbSet<Asset> Asset { set; get; }
        public DbSet<NewAsset> NewAssets { set; get; }
        public DbSet<AssetRegistration> AssetRegistrations { set; get; }
        public DbSet<AssetRegistrationDetails> AssetRegistrationDetail { set; get; }
        public DbSet<LoginUser> LoginUsers { set; get; }
        public DbSet<LoginHistory> LoginHistories { set; get; }
        public DbSet<Movement> Movements { set; get; }
        public DbSet<MovementPermision> MovementPermisions { set; get; }
        public DbSet<Message> Messages { set; get; }
        public DbSet<Contact> Contacts { set; get; }
    }
}