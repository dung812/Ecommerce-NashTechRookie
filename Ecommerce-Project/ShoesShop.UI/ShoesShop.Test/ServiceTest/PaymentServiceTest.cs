using Microsoft.EntityFrameworkCore;
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
    public class PaymentServiceTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Payment> _payments;
        public PaymentServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("PaymentTestDB").Options;
            _context = new ApplicationDbContext(_options);

            _payments = PaymentTestData.GetPayments();
            _context.Payments.AddRange(_payments);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }
        [Fact]
        public void GetAllPayment_ShouldReturnListPaymentDTO()
        {
            //Arrange
            PaymentService paymentService = new PaymentService(_context);

            // Act
            var result = paymentService.GetAllPayment();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<PaymentViewModel>>(result);
            Assert.True(result.Count > 0);
        }
    }
}
