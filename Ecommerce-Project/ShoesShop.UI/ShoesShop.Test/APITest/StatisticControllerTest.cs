using Microsoft.EntityFrameworkCore;
using Moq;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.API;
using ShoesShop.Service;
using ShoesShop.Test.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ShoesShop.API.Mapper;
using ShoesShop.DTO;
using ShoesShop.API.Controllers;

namespace ShoesShop.Test.APITest
{
    public class StatisticControllerTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Order> _orders;
        private readonly IMapper _mapper;
        public StatisticControllerTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("StatisticTestDB").Options;
            _context = new ApplicationDbContext(_options);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new OrderMapper())).CreateMapper();
            _orders = OrderTestData.GetOrders();
            _context.Database.EnsureDeleted();
            _context.Orders.AddRange(_orders);
            _context.SaveChanges();
        }

        [Fact]
        public void GetStatisticCardNumberAPI_ListStatisticNumberDTO()
        {
            //Arrange
            var listStatisticCard = OrderTestData.GetCardStatistics();
            var mockService = new Mock<IStatisticService>();
            mockService.Setup(ser => ser.GetStatisticNumber()).Returns(listStatisticCard);

            var controller = new StatisticController(mockService.Object);

            // Act
            var result = controller.GetStatisticCardNumber();

            // Assert
            Assert.IsType<List<StatisticNumberViewModel>>(result.Value);
            Assert.Equal(listStatisticCard.Count, result?.Value?.Count);
        }        
        [Fact]
        public void GetRecentOrdersAPI_ShouldReturnListOrderDTOHaveDateOrderIsToday()
        {
            //Arrange
            var listOrder = _mapper.Map<List<OrderViewModel>>(OrderTestData.GetOrders().Where(m => m.OrderDate.Date == DateTime.Today));
            var mockService = new Mock<IStatisticService>();
            mockService.Setup(ser => ser.GetRecentOrders()).Returns(listOrder);

            var controller = new StatisticController(mockService.Object);

            // Act
            var result = controller.GetRecentOrder();

            // Assert
            Assert.IsType<List<OrderViewModel>>(result.Value);
            Assert.Equal(listOrder.Count, result?.Value?.Count); // Check total item in list compare fake list
        }
    }
}
