using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.TestData
{
    public class RoleTestData
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>() {
                new Role() {
                    RoleId = 1,
                    RoleName = "Admin",
                    Note = ""
                },
                new Role() {
                    RoleId = 2,
                    RoleName = "Employee",
                    Note = ""
                },
            };
        }
    }
}
