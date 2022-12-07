using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.Test.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ShoesShop.Service;
using ShoesShop.DTO;

namespace ShoesShop.Test.ServiceTest
{
    public class CatalogServiceTest : IDisposable
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Catalog> _catalogs;
        public CatalogServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CatalogTestDB").Options;
            _context = new ApplicationDbContext(_options);
            _catalogs = CatalogTestData.GetCatalogs();
            _context.Database.EnsureDeleted();
            _context.Catalogs.AddRange(_catalogs);
            _context.SaveChanges();
        }

        [Fact]
        public void GetAllCatalog_ShouldReturnListCatalogDTO()
        {
            //Arrange
            var TotalItem = CatalogTestData.GetCatalogs().Count; // Total item in fake list catalog should return 5
            CatalogService catalogService = new CatalogService(_context);

            // Act
            var result = catalogService.GetAllCatalog();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CatalogViewModel>>(result);
            Assert.Equal(TotalItem, result.Count);
        }  
        
        [Fact]
        public void GetCatalogById_ValidId_ShouldReturnCatalogDTO()
        {
            //Arrange
            var catalogId = 1;
            CatalogService catalogService = new CatalogService(_context);

            // Act
            var result = catalogService.GetCatalogById(catalogId);

            // Assert
            Assert.IsType<CatalogViewModel>(result);
            Assert.NotNull(result);
            Assert.Equal(catalogId, result.CatalogId);
        }           
        [Fact]
        public void GetCatalogById_InvalidId_ShouldReturnNull()
        {
            //Arrange
            var catalogId = 100; // Invalid ID
            CatalogService catalogService = new CatalogService(_context);

            // Act
            var result = catalogService.GetCatalogById(catalogId);

            // Assert
            Assert.IsType<CatalogViewModel>(result);
            Assert.Null(result.CatalogId);
        }        
        
        [Fact]
        public void CreateCatalog_ShouldReturnTrueIfSuccess()
        {
            //Arrange
            var catalogDTO = new CatalogViewModel()
            {
                CatalogId = 6,
                Name = "Test"
            };
            CatalogService catalogService = new CatalogService(_context);

            // Act
            var result = catalogService.CreateCatalog(catalogDTO);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void UpdateCatalog_ValidId_ShouldReturnTrueIfSuccess()
        {
            //Arrange
            int catalogId = 1;
            var catalogDTO = new CatalogViewModel()
            {
                Name = "Test"
            };
            CatalogService catalogService = new CatalogService(_context);

            // Act
            var result = catalogService.UpdateCatalog(catalogId, catalogDTO);

            // Assert
            Assert.True(result);
        }          
        
        [Fact]
        public void UpdateCatalog_InvalidId_ShouldReturnFalseIfFail()
        {
            //Arrange
            int catalogId = 100;
            var catalogDTO = new CatalogViewModel()
            {
                Name = "Test"
            };
            CatalogService catalogService = new CatalogService(_context);

            // Act
            var result = catalogService.UpdateCatalog(catalogId, catalogDTO);

            // Assert
            Assert.False(result);
        }        
        
        [Fact]
        public void DeleteCatalog_ValidId_ShouldReturnTrueIfSuccess()
        {
            //Arrange
            int catalogId = 1;
            CatalogService catalogService = new CatalogService(_context);

            // Act
            var result = catalogService.DeleteCatalog(catalogId);

            // Assert
            Assert.True(result);
        }       
        [Fact]
        public void DeleteCatalog_InvalidId_ShouldReturnFalseIfFail()
        {
            //Arrange
            int catalogId = 100;
            CatalogService catalogService = new CatalogService(_context);

            // Act
            var result = catalogService.DeleteCatalog(catalogId);

            // Assert
            Assert.False(result);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

    }
}
