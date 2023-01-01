using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoesShop.API.Mapper;
using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.DTO;
using ShoesShop.DTO.Admin;
using ShoesShop.Service;
using ShoesShop.Test.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.ServiceTest
{
    public class CommentProductServiceTest
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        private readonly List<CommentProduct> _commentProducts;
        private readonly List<Customer> _customers;
        public CommentProductServiceTest()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CommentProductTestDB").Options;
            _context = new ApplicationDbContext(_options);

            _commentProducts = CommentProductTestData.GetCommentProducts();
            _context.CommentProducts.AddRange(_commentProducts);

            _customers = CustomerTestData.GetCustomers();
            _context.Customers.AddRange(_customers);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }

        [Fact]
        public void GetListCommentOfProductById_ShouldReturnCommentProductDTO()
        {
            //Arrange
            int productId = 1;
            var listCheck = _commentProducts.Where(m => m.ProductId == productId).ToList();
            CommentProductService commentProductService = new CommentProductService(_context);

            // Act
            var result = commentProductService.GetListCommentOfProductById(productId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<CommentProductViewModel>>(result);
            Assert.Equal(listCheck.Count, result.Count); // Check total item in list compare fake list
        }        
        [Fact]
        public void CheckCustomerCommentYet_ShouldReturnTrue()
        {
            //Arrange
            int productId = 1;
            int customerId = 1;
            CommentProductService commentProductService = new CommentProductService(_context);

            // Act
            var result = commentProductService.CheckCustomerCommentYet(productId, customerId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result);
        }        
        [Fact]
        public void AddCommentOfProduct_ShouldReturnTrue()
        {
            //Arrange
            int productId = 1;
            int customerId = 4;
            int star = 5;
            string content = "";
            CommentProductService commentProductService = new CommentProductService(_context);

            // Act
            var result = commentProductService.AddCommentOfProduct(productId, customerId, star, content);

            // Assert
            Assert.NotNull(result);
            Assert.True(result);
        }
    }
}
