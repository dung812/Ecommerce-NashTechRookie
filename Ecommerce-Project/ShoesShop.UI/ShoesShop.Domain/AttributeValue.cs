using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class AttributeValue
    {
        [Key]
        public int AttributeValueId { get; set; }
        public int? AttributeId { get; set; }
        public string Name { get; set; } = null!;


        public virtual Attribute? Attribute { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}
