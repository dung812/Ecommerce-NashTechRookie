using ShoesShop.Data;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using ShoesShop.Domain;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using System;
using System.Net.WebSockets;
using System.Collections.Generic;

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
        public bool CreateProduct(CreateProductViewModel productViewModel);
        public bool UpdateProduct(int productId, ProductViewModel productViewModel);
        public bool DeleteProduct(int productId);
        public AttributeValue GetAttributeById(int attributeId);
        public List<ProductViewModel> GetAllProductDisabled();
        public bool RestoreProduct(int productId);
        public bool HardDeleteProduct(int productId);
        public bool CheckExistedOrder(int productId);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<ProductViewModel> GetAllProduct()
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();
            productList = _context.Products
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
                                        UpdateDate = m.UpdateDate,
                                        Gender = m.ProductGenderCategory == Gender.Men ? "Men" : "Women",
                                        ImageNameGallery1 = "",
                                        ImageNameGallery2 = "",
                                        ImageNameGallery3 = "",
                                    }).ToList();
            foreach (var product in productList)
            {
                var gallerys = _context.ProductGalleries.Where(m => m.ProductId == product.ProductId).ToList();

                if (gallerys.Count >= 3)
                {
                    product.ImageNameGallery1 = gallerys[0].GalleryName;
                    product.ImageNameGallery2 = gallerys[1].GalleryName;
                    product.ImageNameGallery3 = gallerys[2].GalleryName;
                }

                product.Income = _context.OrderDetails.Where(m => m.ProductId == product.ProductId).Sum(m => m.TotalMoney);

            }
            return productList;
        }

        public IPagedList<ProductViewModel> GetAllProductPage(Gender cateGender, int? manufactureId, int? catalogId, int pageNumber, int pageSize)
        {
            IPagedList<ProductViewModel> productList;
            productList = _context.Products
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
                                        ImageNameGallery1 = "",
                                        ImageNameGallery2 = "",
                                        ImageNameGallery3 = "",
                                    }).ToPagedList(pageNumber, pageSize);
            if (manufactureId != null)
            {
                productList = _context.Products
                                        .TagWith("Get list product")
                                        .Where(m => m.Status == true &&
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
                                            ImageNameGallery1 = "",
                                            ImageNameGallery2 = "",
                                            ImageNameGallery3 = "",
                                        }).ToPagedList(pageNumber, pageSize);
            }
            if (catalogId != null)
            {
                productList = _context.Products
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
                                            ImageNameGallery1 = "",
                                            ImageNameGallery2 = "",
                                            ImageNameGallery3 = "",
                                        }).ToPagedList(pageNumber, pageSize);
            }

            foreach (var product in productList)
            {
                var gallerys = _context.ProductGalleries.Where(m => m.ProductId == product.ProductId).ToList();

                if (gallerys.Count >= 3)
                {
                    product.ImageNameGallery1 = gallerys[0].GalleryName;
                    product.ImageNameGallery2 = gallerys[1].GalleryName;
                    product.ImageNameGallery3 = gallerys[2].GalleryName;
                }

                product.Income = _context.OrderDetails.Where(m => m.ProductId == product.ProductId).Sum(m => m.TotalMoney);

            }
            return productList;
        }
        public ProductViewModel GetProductById(int productId)
        {
            ProductViewModel? product = new();
            product = _context.Products
                                    .TagWith("Get product")
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
                                        UpdateDate = m.UpdateDate,
                                        ImageNameGallery1 = "",
                                        ImageNameGallery2 = "",
                                        ImageNameGallery3 = "",
                                    }).SingleOrDefault(m => m.ProductId == productId);
            var gallerys = _context.ProductGalleries.Where(m => m.ProductId == product.ProductId).ToList();

            if (gallerys.Count >= 3)
            {
                product.ImageNameGallery1 = gallerys[0].GalleryName;
                product.ImageNameGallery2 = gallerys[1].GalleryName;
                product.ImageNameGallery3 = gallerys[2].GalleryName;
            }
            return product ?? new ProductViewModel();
        }

        public List<ProductViewModel> RelatedProduct(int productId)
        {

            List<ProductViewModel> products = new List<ProductViewModel>();
            var product = _context.Products.FirstOrDefault(m => m.ProductId == productId);
            products = _context.Products.TagWith("Get related product list")
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
            return products;
        }

        public IPagedList<ProductViewModel> FilterProduct(string filterType, Gender cateGender, int pageNumber, int pageSize)
        {
            IPagedList<ProductViewModel> productList;
            if (filterType == "high to low")
            {
                productList = _context.Products
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
                productList = _context.Products
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
                productList = _context.Products
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
                productList = _context.Products
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
            return productList;
        }

        public IPagedList<ProductViewModel> SearchProduct(string keyword, int pageNumber, int pageSize)
        {
            IPagedList<ProductViewModel> productList;
            productList = _context.Products
                                    .TagWith("Search product")
                                    .Where(m => m.Status == true &&
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

            return productList;
        }
        public List<string> GetNameProductList(string keyword)
        {
            var productList = new List<string>();
            productList = _context.Products.Where(m => m.ProductName.Contains(keyword)).Select(m => m.ProductName).ToList();
            return productList;
        }

        public List<AttributeViewModel> GetAttributeOfProduct(int productId)
        {
            var attributes = new List<AttributeViewModel>();
            attributes = _context.ProductAttributes.Where(m => m.ProductId == productId && m.Status).Include(m => m.AttributeValue).Select(m => new AttributeViewModel
            {
                AttributeId = m.AttributeValueId,
                AttributeName = m.AttributeValue.Name
            }).ToList();
            return attributes;
        }

        public Product GetNewProductAfterSave()
        {
            var product = _context.Products.OrderByDescending(m => m.DateCreate).FirstOrDefault();
            return product;
        }
        public bool CreateProduct(CreateProductViewModel productViewModel)
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
            // Save product entry
            _context.Products.Add(product);
            _context.SaveChanges();

            var newProduct = GetNewProductAfterSave();

            // Save product attribute entry
            for (int i = 1; i <= 4; i++)
            {
                ProductAttribute proAttr = new ProductAttribute();
                proAttr.ProductId = newProduct.ProductId;
                proAttr.AttributeValueId = i;
                proAttr.Status = true;
                _context.ProductAttributes.Add(proAttr);
                _context.SaveChanges();
            }


            // Save product image gallery entry
            ProductGallery productGallery1 = new ProductGallery()
            {
                ProductId = newProduct.ProductId,
                GalleryName = productViewModel.ImageNameGallery1,
                Status = true
            };
            _context.ProductGalleries.Add(productGallery1);
            _context.SaveChanges();

            ProductGallery productGallery2 = new ProductGallery()
            {
                ProductId = newProduct.ProductId,
                GalleryName = productViewModel.ImageNameGallery2,
                Status = true
            };
            _context.ProductGalleries.Add(productGallery2);
            _context.SaveChanges();

            ProductGallery productGallery3 = new ProductGallery()
            {
                ProductId = newProduct.ProductId,
                GalleryName = productViewModel.ImageNameGallery3,
                Status = true
            };
            _context.ProductGalleries.Add(productGallery3);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateProduct(int productId, ProductViewModel productViewModel)
        {
            bool result;

            var product = _context.Products.Find(productId);

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

                _context.Products.Update(product);
                _context.SaveChanges();


                // Update product gallery entry
                var galleryProducts = _context.ProductGalleries.Where(m => m.ProductId == productViewModel.ProductId).ToList();

                galleryProducts[0].GalleryName = productViewModel.ImageNameGallery1;
                _context.ProductGalleries.Update(galleryProducts[0]);
                _context.SaveChanges();

                galleryProducts[1].GalleryName = productViewModel.ImageNameGallery2;
                _context.ProductGalleries.Update(galleryProducts[1]);
                _context.SaveChanges();

                galleryProducts[2].GalleryName = productViewModel.ImageNameGallery3;
                _context.ProductGalleries.Update(galleryProducts[2]);
                _context.SaveChanges();

                result = true;
            }
            else
                result = false;
            return result;
        }
        public bool DeleteProduct(int productId)
        {
            bool result;
            var product = _context.Products.Find(productId);
            if (product != null)
            {

                product.Status = false;

                _context.Products.Update(product);
                _context.SaveChanges();

                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public AttributeValue GetAttributeById(int attributeId)
        {
            var attribute = _context.AttributeValues.FirstOrDefault(model => model.AttributeValueId == attributeId);
            return attribute;
        }

        public List<ProductViewModel> GetAllProductDisabled()
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();
            productList = _context.Products
                                    .TagWith("Get list product")
                                    .Where(m => m.Status == false)
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
                                        UpdateDate = m.UpdateDate,
                                        Gender = m.ProductGenderCategory == Gender.Men ? "Men" : "Women",
                                        ImageNameGallery1 = "",
                                        ImageNameGallery2 = "",
                                        ImageNameGallery3 = "",
                                    }).ToList();
            return productList;
        }
        public bool RestoreProduct(int productId)
        {
            bool result;
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                product.Status = true;

                _context.Products.Update(product);
                _context.SaveChanges();

                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool CheckExistedOrder(int productId)
        {
            int total = _context.OrderDetails.Where(m => m.ProductId == productId).Count();
            return total > 0 ? true : false;
        }
        public bool HardDeleteProduct(int productId)
        {
            bool result;
            var product = _context.Products.Find(productId);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();

                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
