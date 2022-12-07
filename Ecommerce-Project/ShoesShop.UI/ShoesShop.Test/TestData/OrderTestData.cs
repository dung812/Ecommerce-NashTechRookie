using ShoesShop.Domain;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.TestData
{
    public class OrderTestData
    {
        public static List<Order> GetOrders()
        {
            return new List<Order>() {
                new Order() {
                   OrderId = "HD123",
                   OrderDate = DateTime.Now,
                   OrderStatus = OrderStatus.NewOrder,
                   OrderName = "Nguyen Dung",
                   Address = "Test Address",
                   Phone = "123456",
                   TotalMoney = 100,
                   TotalDiscounted = 100,
                },
                new Order() {
                   OrderId = "HD456",
                   OrderDate = DateTime.Now,
                   OrderStatus = OrderStatus.NewOrder,
                   OrderName = "Thanh Duy",
                   Address = "Test Address",
                   Phone = "123456",
                   TotalMoney = 100,
                   TotalDiscounted = 100,
                },                
                new Order() {
                   OrderId = "HD121212",
                   OrderDate = new DateTime(2000, 01, 01),
                   OrderStatus = OrderStatus.NewOrder,
                   OrderName = "Thanh Duy",
                   Address = "Test Address",
                   Phone = "123456",
                   TotalMoney = 100,
                   TotalDiscounted = 100,
                },
            };
        }
    
        public static List<StatisticNumberViewModel> GetCardStatistics()
        {
            return new List<StatisticNumberViewModel>() {
                new StatisticNumberViewModel() {
                    Title = "Customer",
                    Number = 200,
                    IsUp = true,
                    Percent = "20%",
                    Prefix = "+",
                    Suffix = "%"
                },                   
                new StatisticNumberViewModel() {
                    Title = "Revenue",
                    Number = 50,
                    IsUp = true,
                    Percent = "10%",
                    Prefix = "-",
                    Suffix = "%"
                },   
            };
        }
    }

}
