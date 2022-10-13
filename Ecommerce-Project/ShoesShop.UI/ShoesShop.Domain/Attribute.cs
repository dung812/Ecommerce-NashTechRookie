using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class Attribute
    {
        [Key]
        public int AttributeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<AttributeValue> AttributeValues { get; set; }
    }
}
