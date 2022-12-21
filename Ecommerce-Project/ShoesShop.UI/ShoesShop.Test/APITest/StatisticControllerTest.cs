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
using Microsoft.AspNetCore.Mvc;

namespace ShoesShop.Test.APITest
{
    public class StatisticControllerTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Admin> _admins;
        private readonly IMapper _mapper;
        public StatisticControllerTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AdminTestDB").Options;
            _context = new ApplicationDbContext(_options);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new OrderMapper())).CreateMapper();
            _admins = AdminTestData.GetAdmins();
            _context.Database.EnsureDeleted();
            _context.Admins.AddRange(_admins);
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
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<StatisticNumberViewModel>>(okResult.Value);
            Assert.Equal(listStatisticCard.Count, returnValue?.Count);
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
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<OrderViewModel>>(okResult.Value);
            Assert.Equal(listOrder.Count, returnValue?.Count); // Check total item in list compare fake list
            Assert.Equal(DateTime.Today, returnValue?.FirstOrDefault().OrderDate.Date); // Check date of item
        }
    }
}
