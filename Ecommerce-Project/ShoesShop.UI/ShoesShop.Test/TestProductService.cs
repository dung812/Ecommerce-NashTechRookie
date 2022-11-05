using ShoesShop.Domain;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using ShoesShop.Service;
using ShoesShop.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test
{
    public class TestProductService
    {
        // Test create new product entry
        [Fact]
        public void Test_CreateNewProduct()
        {
            // Arrange
            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductName = "test",
                Image = "test",
                ImageList = "test",
                OriginalPrice = 1,
                PromotionPercent = 1,
                Description = "test",
                Quantity = 1,
                Gender = "Women",
                AdminId = 1,
                ManufactureId = 1,
                CatalogId = 1 
            };

            ProductService productService = new ProductService();

            // Act
            bool result = productService.CreateProduct(productViewModel);

            // Assert
            Assert.True(result);
        }

        // Test update product entry
        [Fact]
        public void Test_UpdateProduct_InvalidId()
        {
            // Arrange
            int productId = 100;

            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductName = "update",
                Image = "update",
                ImageList = "update",
                OriginalPrice = 1,
                PromotionPercent = 1,
                Description = "",
                Quantity = 1,
                Gender = "Women",
                AdminId = 1,
                ManufactureId = 1,
                CatalogId = 1,
            };

            ProductService productService = new ProductService();

            // Act
            bool result = productService.UpdateProduct(productId, productViewModel);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Test_UpdateProduct_ValidId()
        {
            // Arrange
            int productId = 60;

            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductName = "update",
                Image = "update",
                ImageList = "update",
                OriginalPrice = 1,
                PromotionPercent = 1,
                Description = "",
                Quantity = 1,
                Gender = "Women",
                AdminId = 1,
                ManufactureId = 1,
                CatalogId = 1,
            };

            ProductService productService = new ProductService();

            // Act
            bool result = productService.UpdateProduct(productId, productViewModel);

            // Assert
            Assert.True(result);
        }

        // Test delete product entry
        [Fact]
        public void Test_DeleteProduct_InvalidId()
        {
            // Arrange
            int productId = 600;

            ProductService productService = new ProductService();

            // Act
            bool result = productService.DeleteProduct(productId);

            // Assert
            Assert.False(result);
        }        
        [Fact]
        public void Test_DeleteProduct_ValidId()
        {
            // Arrange
            int productId = 60;

            ProductService productService = new ProductService();

            // Act
            bool result = productService.DeleteProduct(productId);

            // Assert
            Assert.True(result);
        }
    }
}
