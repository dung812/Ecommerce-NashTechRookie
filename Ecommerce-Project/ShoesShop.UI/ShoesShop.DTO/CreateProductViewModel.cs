using ShoesShop.Domain.Enum;
using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ShoesShop.DTO
{
    public class CreateProductViewModel
    {
        public string ProductName { get; set; }
        public string ImageFileName { get; set; }
        public string ImageName { get; set; }
        public int OriginalPrice { get; set; }
        public int PromotionPercent { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Gender { get; set; }
        public int AdminId { get; set; }
        public int ManufactureId { get; set; }
        public int CatalogId { get; set; }
        public string ImageNameGallery1 { get; set; }
        public string ImageNameGallery2 { get; set; }
        public string ImageNameGallery3 { get; set; }
    }
}
