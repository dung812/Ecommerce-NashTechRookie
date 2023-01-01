using Microsoft.EntityFrameworkCore;
using ShoesShop.Data;
using ShoesShop.Domain.Enum;
using ShoesShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using JetBrains.Annotations;

namespace ShoesShop.Service
{
    public interface IStatisticService
    {
        public List<StatisticNumberViewModel> GetStatisticNumber();
        public List<OrderViewModel> GetRecentOrders();
        public List<CustomerReportViewModel> ReportCustomer();
    }
    public class StatisticService : IStatisticService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public StatisticService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
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

        public List<OrderViewModel> GetRecentOrders()
        {
            var orders = _context.Orders
                        .Include(m => m.Payment)
                        .Where(m => m.OrderStatus == OrderStatus.NewOrder &&
                                m.OrderDate.Date == DateTime.Today)
                        .OrderByDescending(m => m.OrderDate)
                        .ToList();

            var ordersDTO = _mapper.Map<List<OrderViewModel>>(orders);
            return ordersDTO;
        }
        public List<CustomerReportViewModel> ReportCustomer()
        {
            var customers = _context.Customers.Include(m => m.Orders)
                            .Select(m => new CustomerReportViewModel
                            {
                                FullName = m.FirstName + " " + m.LastName,
                                TotalOrderSuccess = m.Orders.Count(m => m.OrderStatus == OrderStatus.Delivered),
                                TotalOrderCancelled = m.Orders.Count(m => m.OrderStatus == OrderStatus.Cancelled),
                                TotalOrderWaiting = m.Orders.Count(m => m.OrderStatus == OrderStatus.AwatingShipment),
                                TotalMoneyPurchased = m.Orders.Sum(m => m.TotalMoney),
                            }).OrderByDescending(m => m.TotalMoneyPurchased).Take(10).ToList();
            return customers;
        }
    }
}
