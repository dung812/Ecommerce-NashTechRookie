using Microsoft.EntityFrameworkCore;
using PayPal.NET.Models.Paypal;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using ShoesShop.Service;
using ShoesShop.Test.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace ShoesShop.Test.ServiceTest
{
    public class ProductServiceTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Product> _products;
        private readonly List<Catalog> _catalogs;
        private readonly List<Manufacture> _manufatures;
        private readonly List<Admin> _admins;
        private readonly List<ProductGallery> _productGalleries;

        public ProductServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ProductTestDB").Options;
            _context = new ApplicationDbContext(_options);

            _manufatures = ManufactureTestData.GetManufactures();
            _context.Manufactures.AddRange(_manufatures);            
            
            _catalogs = CatalogTestData.GetCatalogs();
            _context.Catalogs.AddRange(_catalogs);

            _products = ProductTestData.GetProducts();
            _context.Products.AddRange(_products);

            _productGalleries = ProductGalleriesTestData.GetProductGalleries();
            _context.ProductGalleries.AddRange(_productGalleries);            
            
            _admins = AdminTestData.GetAdmins();
            _context.Admins.AddRange(_admins);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }

        [Fact]
        public void GetAllProduct_ShouldReturnListProductDTO()
        {
            //Arrange
            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.GetAllProduct();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductViewModel>>(result);
        }
        [Theory]
        [InlineData(Gender.Men, 1, null, 1, 1)]
        [InlineData(Gender.Men, 1, 1, 1, 1)]
        public void GetAllProductPage_ShouldReturnListProductDTO(Gender cateGender, int? manufactureId, int? catalogId, int pageNumber, int pageSize)
        {
            //Arrange
            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.GetAllProductPage(cateGender, manufactureId, catalogId, pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void GetProductById_ShouldReturnProductDTO()
        {
            //Arrange
            int productId = 1;
            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.GetProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProductViewModel>(result);
        }        
        [Fact]
        public void RelatedProduct_ShouldReturnListProductDTO()
        {
            //Arrange
            int productId = 1;
            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.RelatedProduct(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProductViewModel>>(result);
        }
        [Theory]
        [InlineData("test", Gender.Men, 1, 1)]
        [InlineData("high to low", Gender.Men, 1, 1)]
        [InlineData("low to high", Gender.Men, 1, 1)]
        [InlineData("Sale", Gender.Men, 1, 1)]
        public void FilterProduct_ShouldReturnListProductDTO(string filterType, Gender cateGender, int pageNumber, int pageSize)
        {
            //Arrange
            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.FilterProduct(filterType, cateGender, pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
        }        
        [Theory]
        [InlineData("test", 1, 1)]
        public void SearchProduct_ShouldReturnListProductDTO(string keyword, int pageNumber, int pageSize)
        {
            //Arrange
            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.SearchProduct(keyword, pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void GetNameProductList_ShouldReturnListName()
        {
            //Arrange
            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.GetNameProductList("Product A");

            // Assert
            Assert.NotNull(result);
        }        
        [Fact]
        public void GetAttributeOfProduct_ShouldReturnListAttributeDTO()
        {
            //Arrange
            int productId = 1;
            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.GetAttributeOfProduct(productId);

            // Assert
            Assert.IsType<List<AttributeViewModel>>(result);
        }        
        [Fact]
        public void GetNewProductAfterSave_ShouldReturnProduct()
        {
            //Arrange
            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.GetNewProductAfterSave();

            // Assert
            Assert.IsType<Product>(result);
        }
        [Fact]
        public void CreateProduct_ShouldReturnTrue()
        {
            //Arrange
            CreateProductViewModel productViewModel = new CreateProductViewModel()
            {
                ProductName = "",
                ImageFileName = "",
                ImageName = "",
                OriginalPrice = 0,
                PromotionPercent = 0,
                Description = "",
                Quantity = 0,
                Gender = "Women",
                AdminId = 1,
                ManufactureId = 1,
                CatalogId = 1,
                ImageNameGallery1 = "",
                ImageNameGallery2 = "",
                ImageNameGallery3 = "",
            };

            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.CreateProduct(productViewModel);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void UpdateProduct_ValidId_ShouldReturnTrue()
        {
            //Arrange
            int productId = 1;
            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductId = productId,
                ProductName = "",
                ImageFileName = "",
                ImageName = "",
                OriginalPrice = 0,
                PromotionPercent = 0,
                Description = "",
                Quantity = 0,
                Gender = "Women",
                AdminId = 1,
                ManufactureId = 1,
                CatalogId = 1,
                ImageNameGallery1 = "",
                ImageNameGallery2 = "",
                ImageNameGallery3 = "",
            };

            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.UpdateProduct(productId, productViewModel);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void UpdateProduct_InvalidId_ShouldReturnFalse()
        {
            //Arrange
            int productId = 100;
            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductId = productId,
                ProductName = "",
                ImageFileName = "",
                ImageName = "",
                OriginalPrice = 0,
                PromotionPercent = 0,
                Description = "",
                Quantity = 0,
                Gender = "Women",
                AdminId = 1,
                ManufactureId = 1,
                CatalogId = 1,
                ImageNameGallery1 = "",
                ImageNameGallery2 = "",
                ImageNameGallery3 = "",
            };

            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.UpdateProduct(productId, productViewModel);

            // Assert
            Assert.False(result);
        }
        [Fact]
        public void DeleteProduct_ValidId_ShouldReturnTrue()
        {
            //Arrange
            int productId = 1;

            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.DeleteProduct(productId);

            // Assert
            Assert.True(result);
        }        
        [Fact]
        public void DeleteProduct_InvalidId_ShouldReturnFalse()
        {
            //Arrange
            int productId = 100;

            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.DeleteProduct(productId);

            // Assert
            Assert.False(result);
        }       
        [Fact]
        public void GetAttributeById_ShouldReturnAttributeValue()
        {
            //Arrange
            ProductService productService = new ProductService(_context);

            // Act
            var result = productService.GetAttributeById(1);

            // Assert
            Assert.Null(result);
        }
    }
}
