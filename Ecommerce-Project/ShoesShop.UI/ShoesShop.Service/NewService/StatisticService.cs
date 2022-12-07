using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Service.NewService
{
    public interface IStatisticService
    {
        public List<StatisticNumberViewModel> GetStatisticNumber();
    }
    public class StatisticService : IStatisticService
    {
        private readonly ApplicationDbContext _context;
        public StatisticService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<StatisticNumberViewModel> GetStatisticNumber()
        {
            var list = new List<StatisticNumberViewModel>();
            StatisticNumberViewModel customer = new StatisticNumberViewModel()
            {
                Title = "Customer",
                Number = _context.Customers.Count(),
                IsUp = true,
                Percent = "5.27%",
                Prefix = "",
                Suffix = ""
            };
            list.Add(customer);

            StatisticNumberViewModel order = new StatisticNumberViewModel()
            {
                Title = "Order",
                Number = _context.Orders.Count(),
                IsUp = false,
                Percent = "1.08%",
                Prefix = "",
                Suffix = ""
            };
            list.Add(order);

            StatisticNumberViewModel revenue = new StatisticNumberViewModel()
            {
                Title = "Revenue",
                Number = _context.Orders.Sum(m => m.TotalMoney),
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
            return list;
        }
    }
}
