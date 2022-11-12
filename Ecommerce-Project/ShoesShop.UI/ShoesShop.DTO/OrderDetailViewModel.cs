using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO
{
    public class OrderDetailViewModel
    {
        public string OrderId { get; set; } 
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string AttributeName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int DiscountedPrice { get; set; }
        public int PromotionPercent { get; set; }
        public int TotalDiscounted { get; set; }
        public int TotalMoney { get; set; }

    }
}
