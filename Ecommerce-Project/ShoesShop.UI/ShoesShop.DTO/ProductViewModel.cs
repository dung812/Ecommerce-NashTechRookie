using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ShoesShop.DTO
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string ProductName { get; set; }
        public string Image { get; set; }
        public string ImageList { get; set; }
        public int OriginalPrice { get; set; }
        public int PromotionPercent { get; set; }
        [DataType(DataType.MultilineText)]
        public string ProductDescription { get; set; }
        public string GenderCategory { get; set; }
        public string ManufactureName { get; set; }
        public string CatalogName { get; set; }
        public string AdminCreate { get; set; }
        public string DateCreate { get; set; }
        public int Income { get; set; } //  Tổng doanh thu sản phẩm
        public int AvgStar { get; set; } // Trung bình số sao đánh giá
        public int TotalComment { get; set; } // Tổng số lượt đánh giá
    }
}
