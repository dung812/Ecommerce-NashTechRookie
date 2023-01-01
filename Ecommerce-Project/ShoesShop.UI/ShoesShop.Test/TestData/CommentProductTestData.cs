using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.TestData
{
    public class CommentProductTestData
    {
        public static List<CommentProduct> GetCommentProducts()
        {
            return new List<CommentProduct>() {
                new CommentProduct() {
                    ProductId = 1,
                    CustomerId = 1,
                    Star = 5,
                    Content = "Test",
                    Date = new DateTime(2000, 01, 01),
                    Status = true
                },                
                new CommentProduct() {
                    ProductId = 1,
                    CustomerId = 2,
                    Star = 4,
                    Content = "Test",
                    Date = new DateTime(2000, 01, 01),
                    Status = true
                },                
                new CommentProduct() {
                    ProductId = 2,
                    CustomerId = 2,
                    Star = 1,
                    Content = "Test",
                    Date = new DateTime(2000, 01, 01),
                    Status = true
                },                
                new CommentProduct() {
                    ProductId = 3,
                    CustomerId = 2,
                    Star = 3,
                    Content = "Test",
                    Date = new DateTime(2000, 01, 01),
                    Status = true
                },                
                new CommentProduct() {
                    ProductId = 4,
                    CustomerId = 1,
                    Star = 3,
                    Content = "Test",
                    Date = new DateTime(2000, 01, 01),
                    Status = true
                },
            };
        }
    }
}
