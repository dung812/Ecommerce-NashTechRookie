using ShoesShop.Data;
using ShoesShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Service
{
    public interface IStatisticService
    {
        public List<StatisticNumberViewModel> GetStatisticNumber();
    }
    public class StatisticService : IStatisticService
    {
        public List<StatisticNumberViewModel> GetStatisticNumber()
        {
            var list = new List<StatisticNumberViewModel>();
            using (var context = new ApplicationDbContext())
            {
                StatisticNumberViewModel customer = new StatisticNumberViewModel()
                {
                    Title = "Customer",
                    Number = context.Customers.Count(),
                    IsUp = true,
                    Percent = "5.27%",
                    Prefix = "",
                    Suffix = ""
                };
                list.Add(customer); 
                
                StatisticNumberViewModel order = new StatisticNumberViewModel()
                {
                    Title = "Order",
                    Number = context.Orders.Count(),
                    IsUp = false,
                    Percent = "1.08%",
                    Prefix = "",
                    Suffix = ""
                };
                list.Add(order);                
                
                StatisticNumberViewModel revenue = new StatisticNumberViewModel()
                {
                    Title = "Revenue",
                    Number = context.Orders.Sum(m => m.TotalMoney),
                    IsUp = false,
                    Percent = "1.08%",
                    Prefix = "$",
                    Suffix = ""
                };
                list.Add(revenue);                
                
                StatisticNumberViewModel growth = new StatisticNumberViewModel()
                {
                    Title = "Growth",
                    Number = 30,
                    IsUp = false,
                    Percent = "1.08%",
                    Prefix = "+ ",
                    Suffix = "%"
                };
                list.Add(growth);

            }
            return list;
        }
    }
}
