using ShoesShop.DTO;
using ShoesShop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test
{
    public class TestCatalogService
    {
        // Test create new catalog entry
        [Fact]
        public void Test_CreateNewProduct()
        {
            // Arrange
            CatalogViewModel catalogViewModel = new CatalogViewModel()
            {
                Name = "test"
            };

            CatalogService catalogService = new CatalogService();

            // Act
            bool result = catalogService.CreateCatalog(catalogViewModel);

            // Assert
            Assert.True(result);
        }

        // Test update catalog entry
        [Fact]
        public void Test_UpdateCatalog_InvalidId()
        {
            // Arrange
            int catalogId = 150;

            CatalogViewModel catalogViewModel = new CatalogViewModel()
            {
                Name = "test update"
            };

            CatalogService catalogService = new CatalogService();

            // Act
            bool result = catalogService.UpdateCatalog(catalogId, catalogViewModel);

            // Assert
            Assert.False(result);
        }        
        [Fact]
        public void Test_UpdateCatalog_ValidId()
        {
            // Arrange
            int catalogId = 15;

            CatalogViewModel catalogViewModel = new CatalogViewModel()
            {
                Name = "test update"
            };

            CatalogService catalogService = new CatalogService();

            // Act
            bool result = catalogService.UpdateCatalog(catalogId, catalogViewModel);

            // Assert
            Assert.True(result);
        }


        // Test delete catalog entry
        [Fact]
        public void Test_DeleteCatalog_InvalidId()
        {
            // Arrange
            int catalogId = 150;

            CatalogService catalogService = new CatalogService();

            // Act
            bool result = catalogService.DeleteCatalog(catalogId);

            // Assert
            Assert.False(result);
        }        
        [Fact]
        public void Test_DeleteCatalog_ValidId()
        {
            // Arrange
            int catalogId = 15;

            CatalogService catalogService = new CatalogService();

            // Act
            bool result = catalogService.DeleteCatalog(catalogId);

            // Assert
            Assert.True(result);
        }
    }
}
