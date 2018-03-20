using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ATSystem.App_Start;
using ATSystem.Models.Entity;
using ATSystem.Models.ViewModel.Asset;
using ATSystem.Models.ViewModel.AssetRegistration;
using ATSystem.Models.ViewModel.Branch;
using ATSystem.Models.ViewModel.Movement;
using ATSystem.Models.ViewModel.ResetPassword;
using AutoMapper;

namespace ATSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Mapper.Initialize(conf =>
            {
                conf.CreateMap<AssetEntryVm,Asset> ();
                conf.CreateMap<AssetEntryVm, NewAsset>();
                conf.CreateMap<BranchlistVM,Branch>();
                conf.CreateMap<AssetRegistrationCreateVM,AssetRegistration>();
                conf.CreateMap<MovementEntryVM, Movement>();
                conf.CreateMap<MovementEntryVM, MovementPermision>();
                conf.CreateMap<ResetPasswordVM, User>();
            });
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
    }
}
