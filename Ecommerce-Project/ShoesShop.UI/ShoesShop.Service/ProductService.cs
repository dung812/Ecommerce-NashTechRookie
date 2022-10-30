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
        public List<ProductViewModel> RelatedProduct(int productId);
        public IPagedList<ProductViewModel> FilterProduct(string filterType, Gender cateGender, int pageNumber, int pageSize);
        public IPagedList<ProductViewModel> SearchProduct(string keyword, int pageNumber, int pageSize);
        public List<string> GetNameProductList(string keyword);
        public List<AttributeViewModel> GetAttributeOfProduct(int productId);
        public bool CreateProduct(ProductViewModel productViewModel);
        public bool UpdateProduct(int productId, ProductViewModel productViewModel);
        public bool DeleteProduct(int productId);
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
                                        .OrderByDescending(m => m.DateCreate)
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
                                        .OrderByDescending(m => m.DateCreate)
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

        public List<ProductViewModel> RelatedProduct(int productId)
        {

            List<ProductViewModel> products = new List<ProductViewModel>();
            using (var context = new ApplicationDbContext())
            {
                var product = context.Products.FirstOrDefault(m => m.ProductId == productId);
                products = context.Products.TagWith("Get related product list")
                                        .Where(m => m.Status == true &&
                                                    m.CatalogId == product.CatalogId &&
                                                    m.ProductId != productId)
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
                                        }).Take(4).ToList();
            }
            return products;
        }

        public IPagedList<ProductViewModel> FilterProduct(string filterType, Gender cateGender, int pageNumber, int pageSize)
        {
            IPagedList<ProductViewModel> productList;
            using (var context = new ApplicationDbContext())
            {
                if (filterType == "high to low")
                {
                    productList = context.Products
                                            .TagWith("Get list product")
                                            .Where(m => m.Status == true && m.ProductGenderCategory == cateGender)
                                            .Include(m => m.Catalog)
                                            .Include(m => m.Manufacture)
                                            .Include(m => m.Admin)
                                            .OrderByDescending(m => m.OriginalPrice)
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
                else if (filterType == "low to high")
                {
                    productList = context.Products
                                            .TagWith("Get list product")
                                            .Where(m => m.Status == true && m.ProductGenderCategory == cateGender)
                                            .Include(m => m.Catalog)
                                            .Include(m => m.Manufacture)
                                            .Include(m => m.Admin)
                                            .OrderBy(m => m.OriginalPrice)
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
                else if (filterType == "Sale")
                {
                    productList = context.Products
                                        .TagWith("Get list product")
                                        .Where(m => m.Status == true && 
                                               m.ProductGenderCategory == cateGender &&
                                               m.PromotionPercent > 0)
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
                else
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
                                            }).ToPagedList(pageNumber, pageSize);
                }
            }
            return productList;
        }
    
        public IPagedList<ProductViewModel> SearchProduct(string keyword, int pageNumber, int pageSize)
        {
            IPagedList<ProductViewModel> productList;
            using (var context = new ApplicationDbContext())
            {
                productList = context.Products
                                           .TagWith("Search product")
                                           .Where(  m => m.Status == true && 
                                                    m.ProductName.Contains(keyword))
                                           .Include(m => m.Catalog)
                                           .Include(m => m.Manufacture)
                                           .Include(m => m.Admin)
                                           .OrderByDescending(m => m.OriginalPrice)
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

            return productList;
        }
        public List<string> GetNameProductList(string keyword)
        {
            var productList = new List<string>();
            using (var context = new ApplicationDbContext())
            {
                productList = context.Products.Where(m => m.ProductName.Contains(keyword)).Select(m => m.ProductName).ToList();
            }
            return productList;
        }
    
        public List<AttributeViewModel> GetAttributeOfProduct(int productId)
        {
            var attributes = new List<AttributeViewModel>();
            using (var context = new ApplicationDbContext())
            {
                attributes = context.ProductAttributes.Where(m => m.ProductId == productId && m.Status).Include(m => m.AttributeValue).Select(m => new AttributeViewModel
                {
                    AttributeId = m.AttributeValueId,
                    AttributeName = m.AttributeValue.Name
                }).ToList();
            }
            return attributes;
        }
    
        public bool CreateProduct(ProductViewModel productViewModel)
        {
            try
            {
                Product product = new Product()
                {
                    ProductName = productViewModel.ProductName,
                    Image = productViewModel.Image,
                    ImageList = productViewModel.ImageList,
                    OriginalPrice = productViewModel.OriginalPrice,
                    PromotionPercent = productViewModel.PromotionPercent,
                    Description = productViewModel.Description,
                    Quantity = 50,
                    Status = true,
                    ProductGenderCategory = productViewModel.Gender == "Women" ? Gender.Women : Gender.Men,
                    DateCreate = DateTime.Now,
                    AdminId = productViewModel.AdminId,
                    ManufactureId = productViewModel.ManufactureId,
                    CatalogId = productViewModel.CatalogId
                };
                using (var context = new ApplicationDbContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
                return true;

            } catch(Exception)
            {
                return false;
            }
 
        }
        public bool UpdateProduct (int productId, ProductViewModel productViewModel)
        {
            try
            {
                bool result;

                using (var context = new ApplicationDbContext())
                {
                    var product = context.Products.Find(productId);

                    if (product != null)
                    {
                        product.ProductName = productViewModel.ProductName;
                        product.Image = productViewModel.Image;
                        product.ImageList = productViewModel.ImageList;
                        product.OriginalPrice = productViewModel.OriginalPrice;
                        product.PromotionPercent = productViewModel.PromotionPercent;
                        product.Description = productViewModel.Description;
                        product.ProductGenderCategory = productViewModel.Gender == "Women" ? Gender.Women : Gender.Men;
                        product.AdminId = productViewModel.AdminId;
                        product.ManufactureId = productViewModel.ManufactureId;
                        product.CatalogId = productViewModel.CatalogId;
                        product.UpdateDate = DateTime.Now;

                        context.Products.Update(product);
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
        public bool DeleteProduct(int productId)
        {
            try
            {
                bool result;
                using (var context = new ApplicationDbContext())
                {
                    var product = context.Products.Find(productId);
                    if (product != null)
                    {

                        product.Status = false;

                        context.Products.Update(product);
                        context.SaveChanges();

                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                return result;
            }
            catch(Exception)
            {
                return false;
            }
   
        }
    }
}
