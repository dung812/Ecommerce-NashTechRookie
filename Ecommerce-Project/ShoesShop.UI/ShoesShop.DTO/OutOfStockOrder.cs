using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO
{
    public class OutOfStockOrder
    {
        public int ProductId { get; set; }
        public int QuantityOfOrder { get; set; }
        public int QuantityOfStock{ get; set; }

    }
}
