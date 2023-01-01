using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ShoesShop.API.Mapper;
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

namespace ShoesShop.Test.ServiceTest
{
    public class OrderServiceTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Order> _orders;
        private readonly List<Payment> _payments;
        private readonly IMapper _mapper;

        public OrderServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("OrderTestDB").Options;
            _context = new ApplicationDbContext(_options);
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile(new OrderMapper())).CreateMapper();

            _orders = OrderTestData.GetOrders();
            _context.Orders.AddRange(_orders);

            _payments = PaymentTestData.GetPayments();
            _context.Payments.AddRange(_payments);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }

        [Fact]
        public void CreateNewOrder_ShouldReturnTrue()
        {
            //Arrange
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                OrderId = "HD00000",
                OrderDate = DateTime.Now,
                OrderName = "Tesst",
                Address = "",
                Phone = "",
                Note = "",
                TotalMoney = 0,
                TotalDiscounted = 0
            };
            OrderService orderService = new OrderService(_mapper,_context);

            // Act
            var result = orderService.CreateNewOrder(orderViewModel, 1, 1);

            // Assert
            Assert.True(result);
        }        
        [Fact]
        public void CreateOrderDetail_ShouldReturnTrue()
        {
            //Arrange
            string orderId = "HD123";
            List<CartViewModel> listCart = new List<CartViewModel>();

            OrderService orderService = new OrderService(_mapper,_context);

            // Act
            var result = orderService.CreateOrderDetail(orderId, listCart);

            // Assert
            Assert.True(result);
        }        
        [Fact]
        public void GetOrderListByStatus_ShouldReturnListOrderDTO()
        {
            //Arrange
            OrderService orderService = new OrderService(_mapper,_context);

            // Act
            var result = orderService.GetOrderListByStatus(OrderStatus.NewOrder);

            // Assert
            Assert.IsType<List<OrderViewModel>>(result);
        }

        [Theory]
        [InlineData(OrderStatus.All, null, null)]
        [InlineData(OrderStatus.NewOrder, "2000-01-01", "2000-01-01")]
        public void GetOrderListByStatusFilter_ShouldReturnListOrderDTO(OrderStatus status, string? FromDate, string? ToDate)
        {
            //Arrange
            DateTime dateformat1 = new DateTime();
            DateTime dateformat2 = new DateTime();
            if (FromDate != null && ToDate != null)
            {
                dateformat1 = DateTime.ParseExact(FromDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                dateformat2 = DateTime.ParseExact(ToDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }

            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.GetOrderListByStatusFilter(status, FromDate != null ? dateformat1 : null, ToDate != null ? dateformat2 : null);

            // Assert
            Assert.IsType<List<OrderViewModel>>(result);
        }

        [Fact]
        public void GetListOrderByCustomerId_ShouldReturnListOrderDTO()
        {
            //Arrange
            int customerId = 1;
            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.GetListOrderByCustomerId(customerId);

            // Assert
            Assert.IsType<List<OrderViewModel>>(result);
        }        
        [Fact]
        public void GetOrderDetailById_ShouldReturnListOrderDTO()
        {
            //Arrange
            string orderId = "HD123";
            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.GetOrderDetailById(orderId);

            // Assert
            Assert.Null(result);
            //Assert.IsType<OrderViewModel>(result);
        }       
        [Fact]
        public void GetOrderDetailById_ShouldReturnListOrderDetailDTO()
        {
            //Arrange
            string orderId = "HD123";
            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.GetItemOfOderById(orderId);

            // Assert
            Assert.IsType<List<OrderDetailViewModel>>(result);
        }        
        [Fact]
        public void CheckedOrder_ValidId_ShouldReturnTrue()
        {
            //Arrange
            string orderId = "HD123";
            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.CheckedOrder(orderId);

            // Assert
            Assert.True(result);
        }         
        [Fact]
        public void CheckedOrder_InvalidId_ShouldReturnFalse()
        {
            //Arrange
            string orderId = "HD000312321321";
            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.CheckedOrder(orderId);

            // Assert
            Assert.False(result);
        }        
        [Fact]
        public void SuccessDeliveryOrder_ValidId_ShouldReturnTrue()
        {
            //Arrange
            string orderId = "HD0706";
            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.SuccessDeliveryOrder(orderId);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void SuccessDeliveryOrder_InvalidState_ShouldReturnFalse()
        {
            //Arrange
            string orderId = "HD123";
            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.SuccessDeliveryOrder(orderId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CancellationOrder_ValidId_ShouldReturnTrue()
        {
            //Arrange
            string orderId = "HD0706";
            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.CancellationOrder(orderId);

            // Assert
            Assert.True(result);
        }
        [Fact]
        public void CancellationOrder_InvalidState_ShouldReturnFalse()
        {
            //Arrange
            string orderId = "HD123";
            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.CancellationOrder(orderId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetRecentOrders_ShouldReturnStatisticNumberDTO()
        {
            //Arrange

            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.GetRecentOrders();

            // Assert
            Assert.IsType<List<OrderViewModel>>(result);
        }        
        [Fact]
        public void GetStatisticOrder_ShouldReturnOrderStatisticDTO()
        {
            //Arrange
            OrderService orderService = new OrderService(_mapper, _context);

            // Act
            var result = orderService.GetStatisticOrder();

            // Assert
            Assert.IsType<List<OrderStatisticViewModel>>(result);
        }

    }
}
