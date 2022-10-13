using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class Catalog
    {
        [Key]
        public int CatalogId { get; set; }
        public string Name { get; set; } = null!;
        public bool Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
