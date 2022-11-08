using ShoesShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO
{
    public class OrderViewModel
    {
        public string OrderId { get; set; } 
        public DateTime OrderDate { get; set; } 
        public OrderStatus OrderStatus { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? OrderName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Note { get; set; }
        public int TotalMoney { get; set; }
        public string PaymentName { get; set; }
    }
}
