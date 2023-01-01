using Castle.Core.Resource;
using PayPal.NET.Models.Paypal;
using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.TestData
{
    public class CustomerTestData
    {
        public static List<Customer> GetCustomers()
        {
            return new List<Customer>() {
                new Customer() {
                    CustomerId = 1,
                    Avatar = "",
                    FirstName = "Test",
                    LastName = "A",
                    Email = "test1",
                    Password = "",
                    RegisterDate = DateTime.Now,
                    IsNewRegister = true,
                    IsLockedFirstLogin = false,
                    Status = true
                },                
                new Customer() {
                    CustomerId = 2,
                    Avatar = "",
                    FirstName = "Test",
                    LastName = "B",
                    Email = "test2",
                    Password = "",
                    RegisterDate = DateTime.Now,
                    IsNewRegister = true,
                    IsLockedFirstLogin = false,
                    Status = true
                },                
                new Customer() {
                    CustomerId = 3,
                    Avatar = "",
                    FirstName = "Test",
                    LastName = "C",
                    Email = "test3",
                    Password = "",
                    RegisterDate = DateTime.Now,
                    IsNewRegister = true,
                    IsLockedFirstLogin = false,
                    Status = true
                },
  
            };
        }
    }
}
