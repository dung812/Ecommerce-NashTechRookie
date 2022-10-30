using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Service
{
    public interface ICatalogService
    {
        public List<CatalogViewModel> GetAllCatalog();
        public CatalogViewModel GetCatalogById(int catalogId);
        public bool CreateCatalog(CatalogViewModel catalogViewModel);
        public bool UpdateCatalog(int catalogId, CatalogViewModel catalogViewModel);
        public bool DeleteCatalog(int catalogId);
    }
    public class CatalogService : ICatalogService
    {
        public List<CatalogViewModel> GetAllCatalog()
        {
            List<CatalogViewModel> catalogs = new List<CatalogViewModel>();
            using (var context = new ApplicationDbContext())
            {
                catalogs = context.Catalogs.Where(m => m.Status).Select(m => new CatalogViewModel
                {
                    CatalogId = m.CatalogId,
                    Name = m.Name,
                }).ToList();
            }
            return catalogs;
        }
        public CatalogViewModel GetCatalogById(int catalogId)
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            using (var context = new ApplicationDbContext())
            {
                var catalog = context.Catalogs.FirstOrDefault(m => m.CatalogId == catalogId && m.Status);
                if (catalog != null)
                {
                    catalogViewModel.CatalogId = catalog.CatalogId;
                    catalogViewModel.Name = catalog.Name;
                }
            }
            return catalogViewModel;
        }

        public bool CreateCatalog(CatalogViewModel catalogViewModel)
        {
            try
            {
                Catalog catalog = new Catalog()
                {
                    Name = catalogViewModel.Name,
                    Status = true
                };
                using (var context = new ApplicationDbContext())
                {
                    context.Catalogs.Add(catalog);
                    context.SaveChanges();
                }
                return true;
            } catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateCatalog(int catalogId, CatalogViewModel catalogViewModel)
        {
            try
            {
                bool result;
                using (var context = new ApplicationDbContext())
                {
                    var catalog = context.Catalogs.Find(catalogId);
                    if (catalog != null)
                    {
                        catalog.Name = catalogViewModel.Name;

                        context.Catalogs.Update(catalog);
                        context.SaveChanges();

                        result = true;
                    }
                    else
                        result = false;
                }
                return result;
            } catch(Exception)
            {
                return false;
            }
        }

        public bool DeleteCatalog(int catalogId)
        {
            try
            {
                bool result;
                using (var context = new ApplicationDbContext())
                {
                    var catalog = context.Catalogs.Find(catalogId);
                    if (catalog != null)
                    {
                        catalog.Status = false;

                        context.Catalogs.Update(catalog);
                        context.SaveChanges();

                        result = true;
                    }
                    else
                        result = false;
                }
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
