using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ATSystem.Context;
using ATSystem.DAL.BaseRepository;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.Movement;

namespace ATSystem.DAL
{
    public class MovementPermisionRepository:BaseRepository<MovementPermision>,IMovementPermisionRepository
    {
        public MovementPermisionRepository(DbContext db) : base(new AssetDbContext())
        {
            base._db = db;
        }

        public AssetDbContext Context { get { return _db as AssetDbContext; } }

        
    }
}