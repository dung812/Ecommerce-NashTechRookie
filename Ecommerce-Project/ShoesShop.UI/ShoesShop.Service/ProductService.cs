using ShoesShop.Data;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using ShoesShop.Domain;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ShoesShop.Service
{
    public interface IProductService
    {
        List<ProductViewModel> GetAllProduct();
        public IPagedList<ProductViewModel> GetAllProductPage(Gender cateGender, int? manufactureId, int? catalogId, int pageNumber, int pageSize);
        ProductViewModel GetProductById(int productId);
    }

    public class ProductService : IProductService
    {
        public List<ProductViewModel> GetAllProduct()
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();
            using (var context = new ApplicationDbContext())
            {
                productList = context.Products
                                        .TagWith("Get list product")
                                        .Where(m => m.Status == true)
                                        .Include(m => m.Catalog)
                                        .Include(m => m.Manufacture)
                                        .Include(m => m.Admin)
                                        .Select(m => new ProductViewModel
                                        {
                                            ProductId = m.ProductId,
                                            ProductName = m.ProductName,
                                            Image = m.Image,
                                            ImageList = m.ImageList,
                                            OriginalPrice = m.OriginalPrice,
                                            PromotionPercent = m.PromotionPercent,
                                            Description = m.Description,
                                            ProductGenderCategory = m.ProductGenderCategory,
                                            ManufactureName = m.Manufacture.Name,
                                            CatalogName = m.Catalog.Name,
                                            AdminCreate = m.Admin.UserName,
                                            DateCreate = m.DateCreate,
                                        }).ToList();
            }
            return productList;
        }        

        public IPagedList<ProductViewModel> GetAllProductPage(Gender cateGender, int? manufactureId, int? catalogId, int pageNumber, int pageSize)
        {
            IPagedList<ProductViewModel> productList;
            using (var context = new ApplicationDbContext())
            {
                productList = context.Products
                                        .TagWith("Get list product")
                                        .Where(m => m.Status == true && m.ProductGenderCategory == cateGender)
                                        .Include(m => m.Catalog)
                                        .Include(m => m.Manufacture)
                                        .Include(m => m.Admin)
                                        .Select(m => new ProductViewModel
                                        {
                                            ProductId = m.ProductId,
                                            ProductName = m.ProductName,
                                            Image = m.Image,
                                            ImageList = m.ImageList,
                                            OriginalPrice = m.OriginalPrice,
                                            PromotionPercent = m.PromotionPercent,
                                            Description = m.Description,
                                            ProductGenderCategory = m.ProductGenderCategory,
                                            ManufactureName = m.Manufacture.Name,
                                            CatalogName = m.Catalog.Name,
                                            AdminCreate = m.Admin.UserName,
                                            DateCreate = m.DateCreate,
                                        }).ToPagedList(pageNumber, pageSize);
                if (manufactureId != null)
                {
                    productList = context.Products
                                            .TagWith("Get list product")
                                            .Where( m => m.Status == true && 
                                                    m.ProductGenderCategory == cateGender &&
                                                    m.ManufactureId == manufactureId)
                                            .Include(m => m.Catalog)
                                            .Include(m => m.Manufacture)
                                            .Include(m => m.Admin)
                                            .Select(m => new ProductViewModel
                                            {
                                                ProductId = m.ProductId,
                                                ProductName = m.ProductName,
                                                Image = m.Image,
                                                ImageList = m.ImageList,
                                                OriginalPrice = m.OriginalPrice,
                                                PromotionPercent = m.PromotionPercent,
                                                Description = m.Description,
                                                ProductGenderCategory = m.ProductGenderCategory,
                                                ManufactureName = m.Manufacture.Name,
                                                CatalogName = m.Catalog.Name,
                                                AdminCreate = m.Admin.UserName,
                                                DateCreate = m.DateCreate,
                                            }).ToPagedList(pageNumber, pageSize);
                }
                if (catalogId != null)
                {
                    productList = context.Products
                                            .TagWith("Get list product")
                                            .Where(m => m.Status == true &&
                                                    m.ProductGenderCategory == cateGender &&
                                                    m.CatalogId == catalogId)
                                            .Include(m => m.Catalog)
                                            .Include(m => m.Manufacture)
                                            .Include(m => m.Admin)
                                            .Select(m => new ProductViewModel
                                            {
                                                ProductId = m.ProductId,
                                                ProductName = m.ProductName,
                                                Image = m.Image,
                                                ImageList = m.ImageList,
                                                OriginalPrice = m.OriginalPrice,
                                                PromotionPercent = m.PromotionPercent,
                                                Description = m.Description,
                                                ProductGenderCategory = m.ProductGenderCategory,
                                                ManufactureName = m.Manufacture.Name,
                                                CatalogName = m.Catalog.Name,
                                                AdminCreate = m.Admin.UserName,
                                                DateCreate = m.DateCreate,
                                            }).ToPagedList(pageNumber, pageSize);
                }
            }
            return productList;
        }
        public ProductViewModel GetProductById(int productId)
        {
            ProductViewModel? product = new();
            using (var context = new ApplicationDbContext()) 
            {
                product = context.Products
                                        .TagWith("Get list product")
                                        .Where(m => m.Status == true)
                                        .Include(m => m.Catalog)
                                        .Include(m => m.Manufacture)
                                        .Include(m => m.Admin)
                                        .Select(m => new ProductViewModel
                                        {
                                            ProductId = m.ProductId,
                                            ProductName = m.ProductName,
                                            Image = m.Image,
                                            ImageList = m.ImageList,
                                            OriginalPrice = m.OriginalPrice,
                                            PromotionPercent = m.PromotionPercent,
                                            Description = m.Description,
                                            ProductGenderCategory = m.ProductGenderCategory,
                                            ManufactureName = m.Manufacture.Name,
                                            CatalogName = m.Catalog.Name,
                                            AdminCreate = m.Admin.UserName,
                                            DateCreate = m.DateCreate,
                                        }).SingleOrDefault(m => m.ProductId == productId);
            }
                
            return product ?? new ProductViewModel();
        }
    }
}
