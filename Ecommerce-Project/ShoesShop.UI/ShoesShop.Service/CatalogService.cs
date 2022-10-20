using ShoesShop.Data;
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
    }
}
