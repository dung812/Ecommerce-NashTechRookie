using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO
{
    public class OrderStatisticViewModel
    {
        public string OrderType { get; set; } = null!;
        public int QuantityOrder { get; set; }
    }
}
