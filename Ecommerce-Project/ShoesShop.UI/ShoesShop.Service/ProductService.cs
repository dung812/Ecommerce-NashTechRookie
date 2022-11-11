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
        public Product GetNewProductAfterSave();
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
                                            ImageFileName = m.ImageFileName,
                                            ImageName = m.ImageName,
                                            OriginalPrice = m.OriginalPrice,
                                            PromotionPercent = m.PromotionPercent,
                                            Description = m.Description,
                                            ProductGenderCategory = m.ProductGenderCategory,
                                            ManufactureName = m.Manufacture.Name,
                                            CatalogName = m.Catalog.Name,
                                            CatalogId = m.CatalogId,
                                            ManufactureId = m.ManufactureId,
                                            Quantity = m.Quantity,
                                            AdminCreate = m.Admin.UserName,
                                            DateCreate = m.DateCreate,
                                            Gender = m.ProductGenderCategory == Gender.Men ? "Men": "Women",
                                            ImageNameGallery1 = "",
                                            ImageNameGallery2 = "",
                                            ImageNameGallery3 = "",
                                        }).ToList();
                foreach(var product in productList)
                {
                    var gallerys = context.ProductGalleries.Where(m => m.ProductId == product.ProductId).ToList();

                    if (gallerys.Count >= 3)
                    {
                        product.ImageNameGallery1 = gallerys[0].GalleryName;
                        product.ImageNameGallery2 = gallerys[1].GalleryName;
                        product.ImageNameGallery3 = gallerys[2].GalleryName;
                    }    
                }

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
                                            ImageFileName = m.ImageFileName,
                                            ImageName = m.ImageName,
                                            OriginalPrice = m.OriginalPrice,
                                            PromotionPercent = m.PromotionPercent,
                                            Description = m.Description,
                                            Quantity = m.Quantity,
                                            ProductGenderCategory = m.ProductGenderCategory,
                                            ManufactureName = m.Manufacture.Name,
                                            CatalogName = m.Catalog.Name,
                                            CatalogId = m.CatalogId,
                                            ManufactureId = m.ManufactureId,
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
                                                ImageFileName = m.ImageFileName,
                                                ImageName = m.ImageName,
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
                                                ImageFileName = m.ImageFileName,
                                                ImageName = m.ImageName,
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
                                            ImageFileName = m.ImageFileName,
                                            ImageName = m.ImageName,
                                            OriginalPrice = m.OriginalPrice,
                                            PromotionPercent = m.PromotionPercent,
                                            Description = m.Description,
                                            Quantity = m.Quantity,
                                            ProductGenderCategory = m.ProductGenderCategory,
                                            ManufactureName = m.Manufacture.Name,
                                            CatalogName = m.Catalog.Name,
                                            CatalogId = m.CatalogId,
                                            ManufactureId = m.ManufactureId,
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
                                            ImageFileName = m.ImageFileName,
                                            ImageName = m.ImageName,
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
                                                ImageFileName = m.ImageFileName,
                                                ImageName = m.ImageName,
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
                                                ImageFileName = m.ImageFileName,
                                                ImageName = m.ImageName,
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
                                            ImageFileName = m.ImageFileName,
                                            ImageName = m.ImageName,
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
                                                ImageFileName = m.ImageFileName,
                                                ImageName = m.ImageName,
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
                                               ImageFileName = m.ImageFileName,
                                               ImageName = m.ImageName,
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
    
        public Product GetNewProductAfterSave()
        {
            using (var context = new ApplicationDbContext())
            {
                var product = context.Products.OrderByDescending(m => m.DateCreate).FirstOrDefault();
                return product;
            }
        }
        public bool CreateProduct(ProductViewModel productViewModel)
        {
            try
            {
                Product product = new Product()
                {
                    ProductName = productViewModel.ProductName,
                    ImageFileName = productViewModel.ImageFileName,
                    ImageName = productViewModel.ImageName,
                    OriginalPrice = productViewModel.OriginalPrice,
                    PromotionPercent = productViewModel.PromotionPercent,
                    Description = productViewModel.Description,
                    Quantity = productViewModel.Quantity,
                    Status = true,
                    ProductGenderCategory = productViewModel.Gender == "Women" ? Gender.Women : Gender.Men,
                    DateCreate = DateTime.Now,
                    AdminId = productViewModel.AdminId,
                    ManufactureId = productViewModel.ManufactureId,
                    CatalogId = productViewModel.CatalogId
                };
                using (var context = new ApplicationDbContext())
                {
                    // Save product entry
                    context.Products.Add(product);
                    context.SaveChanges();

                    var newProduct = GetNewProductAfterSave();

                    // Save product image gallery entry
                    ProductGallery productGallery1 = new ProductGallery()
                    {
                        ProductId = newProduct.ProductId,
                        GalleryName = productViewModel.ImageNameGallery1,
                        Status = true
                    };
                    context.ProductGalleries.Add(productGallery1);
                    context.SaveChanges();

                    ProductGallery productGallery2 = new ProductGallery()
                    {
                        ProductId = newProduct.ProductId,
                        GalleryName = productViewModel.ImageNameGallery2,
                        Status = true
                    };             
                    context.ProductGalleries.Add(productGallery2);
                    context.SaveChanges();

                    ProductGallery productGallery3 = new ProductGallery()
                    {
                        ProductId = newProduct.ProductId,
                        GalleryName = productViewModel.ImageNameGallery3,
                        Status = true
                    };
                    context.ProductGalleries.Add(productGallery3);
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
                        // Update product entry
                        product.ProductName = productViewModel.ProductName;
                        product.ImageFileName = productViewModel.ImageFileName;
                        product.ImageName = productViewModel.ImageName;
                        product.OriginalPrice = productViewModel.OriginalPrice;
                        product.PromotionPercent = productViewModel.PromotionPercent;
                        product.Description = productViewModel.Description;
                        product.Quantity = productViewModel.Quantity;
                        product.ProductGenderCategory = productViewModel.Gender == "Women" ? Gender.Women : Gender.Men;
                        product.AdminId = productViewModel.AdminId;
                        product.ManufactureId = productViewModel.ManufactureId;
                        product.CatalogId = productViewModel.CatalogId;
                        product.UpdateDate = DateTime.Now;

                        context.Products.Update(product);
                        context.SaveChanges();


                        // Update product gallery entry
                        var galleryProducts = context.ProductGalleries.Where(m => m.ProductId == productViewModel.ProductId).ToList();

                        galleryProducts[0].GalleryName = productViewModel.ImageNameGallery1;
                        context.ProductGalleries.Update(galleryProducts[0]);
                        context.SaveChanges();

                        galleryProducts[1].GalleryName = productViewModel.ImageNameGallery2;
                        context.ProductGalleries.Update(galleryProducts[1]);
                        context.SaveChanges();

                        galleryProducts[2].GalleryName = productViewModel.ImageNameGallery3;
                        context.ProductGalleries.Update(galleryProducts[2]);
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
