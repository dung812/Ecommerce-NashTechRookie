using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class OrderDetail
    {
        public string OrderId { get; set; } = null!;
        public int ProductId { get; set; }
        public int AttributeValueId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        [Range(1, 100)]
        public int PromotionPercent { get; set; }

        public virtual AttributeValue AttributeValue { get; set; } = null!;
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
