using PayPal.NET.Models.Paypal;
using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.TestData
{
    public class AdminTestData
    {
        public static List<Admin> GetAdmins()
        {
            return new List<Admin>() {
                new Admin() {
                    AdminId = 1,
                    UserName = "UserA",
                    Password = "21232f297a57a5a743894a0e4a801fc3",
                    FirstName = "Nguyen",
                    LastName = "A",
                    Email = "test",
                    Phone = "test",
                    Birthday = new DateTime(2000, 01, 01),
                    RegisteredDate = new DateTime(2000, 01, 01),
                    Avatar = "test",
                    Status = true,
                    RoleId = 1,
                },
                new Admin() {
                    AdminId = 2,
                    UserName = "UserB",
                    Password = "",
                    FirstName = "Nguyen",
                    LastName = "B",
                    Email = "test",
                    Phone = "test",
                    Birthday = new DateTime(2000, 01, 01),
                    RegisteredDate = new DateTime(2000, 01, 01),
                    Avatar = "test",
                    Status = true,
                    RoleId = 1,
                },
                new Admin() {
                    AdminId = 3,
                    UserName = "UserC",
                    Password = "",
                    FirstName = "Nguyen",
                    LastName = "C",
                    Email = "test",
                    Phone = "test",
                    Birthday = new DateTime(2000, 01, 01),
                    RegisteredDate = new DateTime(2000, 01, 01),
                    Avatar = "test",
                    Status = true,
                    RoleId = 1,
                },
                new Admin() {
                    AdminId = 4,
                    UserName = "UserD",
                    Password = "",
                    FirstName = "Nguyen",
                    LastName = "D",
                    Email = "test",
                    Phone = "test",
                    Birthday = new DateTime(2000, 01, 01),
                    RegisteredDate = new DateTime(2000, 01, 01),
                    Avatar = "test",
                    Status = true,
                    RoleId = 1,
                },
                new Admin() {
                    AdminId = 5,
                    UserName = "UserE",
                    Password = "",
                    FirstName = "Nguyen",
                    LastName = "E",
                    Email = "test",
                    Phone = "test",
                    Birthday = new DateTime(2000, 01, 01),
                    RegisteredDate = new DateTime(2000, 01, 01),
                    Avatar = "test",
                    Status = true,
                    RoleId = 1,
                },

            };
        }
    }
}
