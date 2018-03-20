using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATSystem.BAL;
using ATSystem.BLL;
using ATSystem.Context;
using ATSystem.Models.Entity;

namespace ATSystem.UiLoader
{
    public class UiLoader
    {
        private GeneralCategoryManager generalCategoryManager;
        private CategoryManager categoryManager;
        private BrandManager brandManager;
        private EmployeeManager employeeManager;
        private AssetManager assetManager;
        private OrganizationManager organizationManager;
        private AssetRegistrationDetailsManager assetRegistrationDetailsManager;
        private UserManager userManager;
        private NewAssetManager newAssetManager;

        public UiLoader()
        {
            generalCategoryManager=new GeneralCategoryManager();
            categoryManager=new CategoryManager();
            brandManager=new BrandManager();
            assetManager=new AssetManager();
            employeeManager=new EmployeeManager();
            organizationManager=new OrganizationManager();
            assetRegistrationDetailsManager=new AssetRegistrationDetailsManager();
            userManager=new UserManager();
            newAssetManager=new NewAssetManager();

        }

        public UiLoader(GeneralCategoryManager _generalCategoryManager, CategoryManager _categoryManager, BrandManager _brandManager,EmployeeManager _employeeManager, AssetManager _assetManager, OrganizationManager _organizationManager, AssetRegistrationDetailsManager _assetRegistrationDetailsManager, UserManager _userManager, NewAssetManager _newAssetManager)

        {
            generalCategoryManager = _generalCategoryManager;
            categoryManager = _categoryManager;
            brandManager = _brandManager;
            assetManager = _assetManager;
            employeeManager = _employeeManager;
            organizationManager = _organizationManager;
            assetRegistrationDetailsManager = _assetRegistrationDetailsManager;
            userManager = _userManager;
            newAssetManager = _newAssetManager;
        }


        AssetDbContext db=new AssetDbContext();

        public List<SelectListItem> GetDefaultSelectListItem()
        {
            var items = new List<SelectListItem>
            {
                new SelectListItem {Value = "", Text = "---Select---"},
            };
            return items;
        }

        public List<SelectListItem> GetOrganizationSelectitems()
        {
            var organization = organizationManager.GetAll();
            var items = GetDefaultSelectListItem();
            items.AddRange(organization.Select(o => new SelectListItem()
            {
                Value = o.Id.ToString(),
                Text = o.Name,
            }));
            return items;
        }

        public List<SelectListItem> GetOrganizationByUserOrgIdSelectitems(int orgid)
        {
            

            var organization = organizationManager.GetAll().Where(c=>c.Id==orgid);
            var items = GetDefaultSelectListItem();
            items.AddRange(organization.Select(o => new SelectListItem()
            {
                Value = o.Id.ToString(),
                Text = o.Name,
            }));
            return items;
        }

        public List<SelectListItem> GetGeneralCategorySelectListItems()
        {
            var generalcategory = generalCategoryManager.GetAll();
            var items = GetDefaultSelectListItem();
            items.AddRange(generalcategory.Select(gc=>new SelectListItem()
            {
                Value = gc.Id.ToString(),
                Text = gc.Name,
            }));
            return items;
        }

        public List<SelectListItem> GetCategorySelectListItems()
        {
            var categories = categoryManager.GetAll();
            var items = GetDefaultSelectListItem();
            items.AddRange(categories.Select(subCat => new SelectListItem()
            {
                Value = subCat.Id.ToString(),
                Text = subCat.Name
            }));
            return items;
        }

        public List<SelectListItem> GetBrandSelectListItems()
        {
            var brand = brandManager.GetAll();
            var items = GetDefaultSelectListItem();
            items.AddRange(brand.Select(c=>new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }));
            return items;
        }

        public List<SelectListItem> GetEmployeeSelectListItems()
        {
            var employees = employeeManager.GetAll();
            var items = GetDefaultSelectListItem();
            items.AddRange(employees.Select(subCat => new SelectListItem()
            {
                Value = subCat.Id.ToString(),
                Text = subCat.Name
            }));
            return items;
        }

        public List<SelectListItem> GetAssetSelectListItems()
        {
            var assets = assetManager.GetAll();
            var items = GetDefaultSelectListItem();
            items.AddRange(assets.Select(subCat => new SelectListItem()
            {
                Value = subCat.Id.ToString(),
                Text = subCat.Name
            }));
            return items;
        }

        public List<SelectListItem> GetUpdateAssetSelectListItems()
        {
            var assets = assetManager.GetAll().Where(c=>c.Registered==false);
            var items = GetDefaultSelectListItem();
            items.AddRange(assets.Select(subCat => new SelectListItem()
            {
                Value = subCat.Id.ToString(),
                Text = subCat.Name
            }));
            return items;
        }

        public List<SelectListItem> GetAssetSelectListItemsJoinwithAssetDetails(int orgid)
        {

            var result = from asset in assetManager.GetAll()
                join assetdetails in assetRegistrationDetailsManager.GetAll().Where(c=>c.OrganizationId==orgid)
                on asset.Id equals assetdetails.AssetId
                select new
                {
                    assetid=asset.Id,
                    assetname=asset.Name
                };

            
            var items = GetDefaultSelectListItem();
            items.AddRange(result.Select(subCat => new SelectListItem()
            {
                Value = subCat.assetid.ToString(),
                Text = subCat.assetname
            }));
            return items;
        }


        public List<SelectListItem> GetUserByOrgIdandNotCurrentUser(int id,int orgid)
        {
            var users = userManager.GetAll().Where(c => c.OrganizationId == orgid && c.Id != id);
            var items = GetDefaultSelectListItem();
            items.AddRange(users.Select(subCat => new SelectListItem()
            {
                Value = subCat.Id.ToString(),
                Text = subCat.FullName
            }));
            return items;
        }
    }
}