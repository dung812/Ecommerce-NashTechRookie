using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ShoesShop.Domain.Enum;

namespace ShoesShop.Domain
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ImageFileName { get; set; } = null!;
        public string ImageName { get; set; } = null!;
        public int OriginalPrice { get; set; }
        [Range(1,100)]
        public int PromotionPercent { get; set; }
        public string Description { get; set; } = null!;
        public int Quantity { get; set; } 
        public bool Status { get; set; }
        public Gender ProductGenderCategory { get; set; } 
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public int AdminId { get; set; }
        public int ManufactureId { get; set; }
        public int CatalogId { get; set; }

        public virtual Admin Admin { get; set; } = null!;
        public virtual Catalog Catalog { get; set; } = null!;
        public virtual Manufacture Manufacture { get; set; } = null!;
        public virtual ICollection<CommentProduct> CommentProducts { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttributes { get; set; }
        public virtual ICollection<ProductGallery> ProductGallery { get; set; }
    }
}
