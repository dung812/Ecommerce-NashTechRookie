using ShoesShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; } = null!;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? CancellationDate { get; set; }
        public string? OrderName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Note { get; set; }
        public int TotalMoney { get; set; }
        public int TotalDiscounted { get; set; }

        public int CustomerId { get; set; }
        public int PaymentId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Payment Payment { get; set; } = null!;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
