using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class ProductAttribute
    {
        public int ProductId { get; set; }
        public int AttributeValueId { get; set; }
        public bool Status { get; set; }
        public string? Note { get; set; }

        public virtual AttributeValue AttributeValue { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
