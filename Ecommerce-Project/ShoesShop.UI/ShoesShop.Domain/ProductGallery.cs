using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Domain
{
    public class ProductGallery
    {
        public int ProductGalleryId { get; set; }
        public int ProductId { get; set; }
        public string GalleryName { get; set; } = null!;
        public bool Status { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
