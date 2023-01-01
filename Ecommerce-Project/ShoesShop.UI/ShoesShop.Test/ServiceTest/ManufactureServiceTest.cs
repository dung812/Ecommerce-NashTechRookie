using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoesShop.API.Mapper;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.DTO;
using ShoesShop.Service;
using ShoesShop.Test.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.ServiceTest
{
    public class ManufactureServiceTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Manufacture> _manufactures;
        public ManufactureServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ManufactureTestDB").Options;
            _context = new ApplicationDbContext(_options);

            _manufactures = ManufactureTestData.GetManufactures();
            _context.Manufactures.AddRange(_manufactures);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }

        [Fact]
        public void GetAllManufacture_ShouldReturnListManufactureDTO()
        {
            //Arrange
            ManufactureService manufactureService = new ManufactureService(_context);

            // Act
            var result = manufactureService.GetAllManufacture();

            // Assert
            Assert.IsType<List<ManufactureViewModel>>(result);
        }        
        [Fact]
        public void GetManufactureById_ShouldReturnManufactureDTO()
        {
            //Arrange
            int manufactureId = 1;
            ManufactureService manufactureService = new ManufactureService(_context);

            // Act
            var result = manufactureService.GetManufactureById(manufactureId);

            // Assert
            Assert.IsType<ManufactureViewModel>(result);
        }        
        [Fact]
        public void CreateManufacture_ShouldReturnTrue()
        {
            //Arrange
            ManufactureViewModel manufactureViewModel = new ManufactureViewModel()
            {
                Name = "",
                Logo = ""
            };
            ManufactureService manufactureService = new ManufactureService(_context);

            // Act
            var result = manufactureService.CreateManufacture(manufactureViewModel);

            // Assert
            Assert.True(result);
        }        
        [Fact]
        public void UpdateManufacture_ValidId_ShouldReturnTrue()
        {
            //Arrange
            int manufactureId = 1;
            ManufactureViewModel manufactureViewModel = new ManufactureViewModel()
            {
                Name = "",
                Logo = ""
            };
            ManufactureService manufactureService = new ManufactureService(_context);

            // Act
            var result = manufactureService.UpdateManufacture(manufactureId, manufactureViewModel);

            // Assert
            Assert.True(result);
        }        
        [Fact]
        public void UpdateManufacture_InvalidId_ShouldReturnFalse()
        {
            //Arrange
            int manufactureId = 100;
            ManufactureViewModel manufactureViewModel = new ManufactureViewModel()
            {
                Name = "",
                Logo = ""
            };
            ManufactureService manufactureService = new ManufactureService(_context);

            // Act
            var result = manufactureService.UpdateManufacture(manufactureId, manufactureViewModel);

            // Assert
            Assert.False(result);
        }          
        [Fact]
        public void DeleteManufacture_ValidId_ShouldReturnTrue()
        {
            //Arrange
            int manufactureId = 1;
            ManufactureViewModel manufactureViewModel = new ManufactureViewModel()
            {
                Name = "",
                Logo = ""
            };
            ManufactureService manufactureService = new ManufactureService(_context);

            // Act
            var result = manufactureService.UpdateManufacture(manufactureId, manufactureViewModel);

            // Assert
            Assert.True(result);
        }           
        [Fact]
        public void DeleteManufacture_InvalidId_ShouldReturnFalse()
        {
            //Arrange
            int manufactureId = 100;
            ManufactureViewModel manufactureViewModel = new ManufactureViewModel()
            {
                Name = "",
                Logo = ""
            };
            ManufactureService manufactureService = new ManufactureService(_context);

            // Act
            var result = manufactureService.UpdateManufacture(manufactureId, manufactureViewModel);

            // Assert
            Assert.False(result);
        }        
    }
}
