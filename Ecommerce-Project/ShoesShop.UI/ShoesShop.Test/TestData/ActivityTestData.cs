using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.TestData
{
    public class ActivityTestData
    {
        public static List<Activity> GetActivities()
        {
            return new List<Activity>() {
                new Activity() {
                    Id = 1,
                    AdminId = 1,
                    ActivityType = "Create",
                    ObjectType = "Product",
                    ObjectName = "Men's Chuck Taylor All Star Street Sneaker Boot",
                    Time = new DateTime(2000, 01, 01)
                },                
                new Activity() {
                    Id = 2,
                    AdminId = 1,
                    ActivityType = "Update",
                    ObjectType = "Product",
                    ObjectName = "Women''s Arizona Footbed Sandal",
                    Time = new DateTime(2000, 01, 01)
                },                
                new Activity() {
                    Id = 3,
                    AdminId = 2,
                    ActivityType = "Create",
                    ObjectType = "Catalog",
                    ObjectName = "Boots",
                    Time = new DateTime(2000, 01, 01)
                },                
                new Activity() {
                    Id = 4,
                    AdminId = 1,
                    ActivityType = "Checked order",
                    ObjectType = "Order",
                    ObjectName = "HD123",
                    Time = new DateTime(2000, 01, 01)
                },                
                new Activity() {
                    Id = 5,
                    AdminId = 2,
                    ActivityType = "Success order",
                    ObjectType = "Order",
                    ObjectName = "HD123",
                    Time = new DateTime(2000, 01, 01)
                },
            };
        }
    }
}
