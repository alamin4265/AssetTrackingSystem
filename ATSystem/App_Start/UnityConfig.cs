using System;
using System.Data.Entity;
using ATSystem.BAL;
using ATSystem.BLL;
using ATSystem.Context;
using ATSystem.DAL;
using ATSystem.Models.Entity;
using ATSystem.Models.Interface.BLL;
using ATSystem.Models.Interface.DAL;
using ATSystem.Models.ViewModel.AssetRegistration;
using ATSystem.Models.ViewModel.Branch;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace ATSystem.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<DbContext, AssetDbContext>();

            container.RegisterType<IOrganizationRepository, OrganizationRepository>();
            container.RegisterType<IOrganizationManager, OrganizationManager>();

            container.RegisterType<IBranchRepository, BranchRepository>();
            container.RegisterType<IBranchManager, BranchManager>();

            container.RegisterType<IAssetLocationRepository, AssetLocationRepository>();
            container.RegisterType<IAssetLocationManager, AssetLocationManager>();

            container.RegisterType<IGeneralCategoryRepository, GeneralCategoryRepository>();
            container.RegisterType<IGeneralCategoryManager, GeneralCategoryManager>();

            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<ICategoryManager, CategoryManager>();

            container.RegisterType<IBrandRepository, BrandRepository>();
            container.RegisterType<IBrandManager, BrandManager>();

            container.RegisterType<IProductCategoryRepository, ProductCategoryRepository>();
            container.RegisterType<IProductCategoryManager, ProductCategoryManager>();

            container.RegisterType<IAssetRepository, AssetRepository>();
            container.RegisterType<IAssetManager, AssetManager>();

            container.RegisterType<INewAssetRepository, NewAssetRepository>();
            container.RegisterType<INewAssetManager, NewAssetManager>();

            container.RegisterType<IAssetRegistrationRepository, AssetRegistrationRepository>();
            container.RegisterType<IAssetRegistrationManager, AssetRegistrationManager>();

            container.RegisterType<IAssetRegistrationDetailsRepository, AssetRegistrationDetailsRepository>();
            container.RegisterType<IAssetRegistrationDetailsManager, AssetRegistrationDetailsManager>();
            
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<IEmployeeManager, EmployeeManager>();

            container.RegisterType<ILoginUserRepository, LoginUserRepository>();
            container.RegisterType<ILoginUserManager, LoginUserManager>();

            container.RegisterType<ILoginHistoryRepository, LoginHistoryRepository>();
            container.RegisterType<ILoginHistoryManager, LoginHistoryManager>();

            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserManager, UserManager>();

            container.RegisterType<IMovementManager, MovementManager>();
            container.RegisterType<IMovementRepository, MovementRepository>();

            container.RegisterType<IMovementPermisionManager, MovementPermisionManager>();
            container.RegisterType<IMovementPermisionRepository, MovementPermisionRepository>();

            container.RegisterType<IMessageManager, MessageManager>();
            container.RegisterType<IMessageRepository, MessageRepository>();

            container.RegisterType<IContactManager, ContactManager>();
            container.RegisterType<IContactRepository, ContactRepository>();
        }
    }
}
