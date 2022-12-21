using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO
{
    public class CustomerReportViewModel
    {
        public string FullName { get; set; }
        public int TotalOrderSuccess { get; set; }
        public int TotalOrderCancelled { get; set; }
        public int TotalOrderWaiting { get; set; }
        public int TotalMoneyPurchased { get; set; }
    }
}
