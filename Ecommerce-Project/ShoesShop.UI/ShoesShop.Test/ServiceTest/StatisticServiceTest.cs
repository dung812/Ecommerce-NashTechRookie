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
using AutoMapper;
using ShoesShop.API.Mapper;

namespace ShoesShop.Test.ServiceTest
{
    public class StatisticServiceTest 
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Catalog> _catalogs;
        private readonly List<Customer> _customers;
        private readonly IMapper _mapper;
        public StatisticServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TestDB").Options;
            _context = new ApplicationDbContext(_options);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new OrderMapper())).CreateMapper();

            _catalogs = CatalogTestData.GetCatalogs();
            _context.Catalogs.AddRange(_catalogs);

            _customers = CustomerTestData.GetCustomers();
            _context.Customers.AddRange(_customers);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }

        [Fact]
        public void GetStatisticNumber_ShouldReturnStatisticNumberDTO()
        {
            //Arrange

            StatisticService statisticService = new StatisticService(_mapper, _context);

            // Act
            var result = statisticService.GetStatisticNumber();

            // Assert
            Assert.IsType<List<StatisticNumberViewModel>>(result);
        }         

        [Fact]
        public void GetRecentOrders_ShouldReturnStatisticNumberDTO()
        {
            //Arrange

            StatisticService statisticService = new StatisticService(_mapper, _context);

            // Act
            var result = statisticService.GetRecentOrders();

            // Assert
            Assert.IsType<List<OrderViewModel>>(result);
        }            
        [Fact]
        public void ReportCustomer_ShouldReturnStatisticNumberDTO()
        {
            //Arrange

            StatisticService statisticService = new StatisticService(_mapper, _context);

            // Act
            var result = statisticService.ReportCustomer();

            // Assert
            Assert.IsType<List<CustomerReportViewModel>>(result);
        }    
        
        
    }
}
