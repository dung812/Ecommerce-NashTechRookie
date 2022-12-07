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
        private readonly ApplicationDbContext _context;
        public CatalogService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<CatalogViewModel> GetAllCatalog()
        {
            List<CatalogViewModel> catalogs = new List<CatalogViewModel>();
            catalogs = _context.Catalogs.Where(m => m.Status).Select(m => new CatalogViewModel
            {
                CatalogId = m.CatalogId,
                Name = m.Name,
            }).ToList();
            return catalogs;
        }
        public CatalogViewModel GetCatalogById(int catalogId)
        {
            CatalogViewModel catalogViewModel = new CatalogViewModel();
            var catalog = _context.Catalogs.FirstOrDefault(m => m.CatalogId == catalogId && m.Status);
            if (catalog != null)
            {
                catalogViewModel.CatalogId = catalog.CatalogId;
                catalogViewModel.Name = catalog.Name;
            }
            return catalogViewModel;
        }

        public bool CreateCatalog(CatalogViewModel catalogViewModel)
        {
            Catalog catalog = new Catalog()
            {
                Name = catalogViewModel.Name,
                Status = true
            };
            _context.Catalogs.Add(catalog);
            return true;
        }

        public bool UpdateCatalog(int catalogId, CatalogViewModel catalogViewModel)
        {
            bool result;
            var catalog = _context.Catalogs.Find(catalogId);
            if (catalog != null)
            {
                catalog.Name = catalogViewModel.Name;

                _context.Catalogs.Update(catalog);
                _context.SaveChanges();

                result = true;
            }
            else
                result = false;
            return result;
        }

        public bool DeleteCatalog(int catalogId)
        {
            bool result;
            var catalog = _context.Catalogs.Find(catalogId);
            if (catalog != null)
            {
                catalog.Status = false;

                _context.Catalogs.Update(catalog);
                _context.SaveChanges();

                result = true;
            }
            else
                result = false;
            return result;
        }

    }
}
