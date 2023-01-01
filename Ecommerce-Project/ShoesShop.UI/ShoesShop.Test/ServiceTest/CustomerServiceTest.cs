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
    public class CustomerServiceTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<Customer> _customers;

        public CustomerServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CustomerTestDB").Options;
            _context = new ApplicationDbContext(_options);

            _customers = CustomerTestData.GetCustomers();
            _context.Customers.AddRange(_customers);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }
        [Fact]
        public void GetAllCustomer_ShouldReturnListCustomerDTO()
        {
            //Arrange
            CustomerService customerService = new CustomerService(_context);

            // Act
            var result = customerService.GetAllCustomer();

            // Assert
            Assert.IsType<List<CustomerViewModel>>(result);
        }        
        [Fact]
        public void CreateCustomer_ValidEmail_ShouldReturnTrue()
        {
            //Arrange
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Email = "test4",
                FirstName = "",
                LastName = "",
                Password = ""
            };
            CustomerService customerService = new CustomerService(_context);

            // Act
            var result = customerService.CreateCustomer(customerViewModel);

            // Assert
            Assert.True(result);
        }        
        [Fact]
        public void CreateCustomer_ExistEmail_ShouldReturnTrue()
        {
            //Arrange
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                Email = "test1",
                FirstName = "",
                LastName = "",
                Password = ""
            };
            CustomerService customerService = new CustomerService(_context);

            // Act
            var result = customerService.CreateCustomer(customerViewModel);

            // Assert
            Assert.False(result);
        }        
        [Fact]
        public void CheckExistEmailOfCustomer_ValidEmail_ShouldReturnFalse()
        {
            //Arrange
            string email = "test10";
            CustomerService customerService = new CustomerService(_context);

            // Act
            var result = customerService.CheckExistEmailOfCustomer(email);

            // Assert
            Assert.False(result);
        }        
        [Fact]
        public void CheckExistEmailOfCustomer_ExistEmail_ShouldReturnTrue()
        {
            //Arrange
            string email = "test1";
            CustomerService customerService = new CustomerService(_context);

            // Act
            var result = customerService.CheckExistEmailOfCustomer(email);

            // Assert
            Assert.True(result);
        }
    }
}
