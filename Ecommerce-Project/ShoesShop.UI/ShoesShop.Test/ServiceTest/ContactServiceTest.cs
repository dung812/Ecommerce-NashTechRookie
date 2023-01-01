using AutoMapper;
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
    public class ContactServiceTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Order> _orders;

        public ContactServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ContactTestDB").Options;
            _context = new ApplicationDbContext(_options);

            _orders = OrderTestData.GetOrders();
            _context.Orders.AddRange(_orders);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }
        [Fact]
        public void Create_ShouldReturnTrue()
        {
            //Arrange
            ContactViewModel contactViewModel = new ContactViewModel()
            {
                Name = "",
                Email = "",
                Message = "",
                Subject = ""
            };
            ContactService contactService = new ContactService(_context);
            // Act
            var result = contactService.Create(contactViewModel);

            // Assert
            Assert.True(result);
        }
    }
}
