using ShoesShop.Data;
using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.DTO
{
    public class CartViewModel
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public string NameItem { get; set; }
        public string ImageItem { get; set; }
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public int UnitPrice { get; set; } //  Giá gốc
        public int PromotionPercent { get; set; } // % Giảm giá
        public int CurrentPriceItem { get; set; } // Giá đã trừ % giảm
        public int Quantity { get; set; }
        public int TotalDiscountedPrice { get; set; } // Tổng số tiền đã giảm

        public Double TotalPrice // tổng tiền của sản phẩm
        {
            get
            {
                return Quantity * CurrentPriceItem;
            }
        }
        public CartViewModel()
        {

        }
        public CartViewModel(int idProduct, int quantity, int attributeId)
        {
            var attribute = context.AttributeValues.FirstOrDefault(model => model.AttributeValueId == attributeId);
            var item = context.Products.FirstOrDefault(model => model.ProductId == idProduct);

            this.ItemId = 0;
            this.ProductId = idProduct;
            this.NameItem = item.ProductName;
            this.ImageItem = item.Image;
            this.AttributeId = (int)attribute.AttributeId;
            this.AttributeName = attribute.Name;
            this.UnitPrice = int.Parse(item.OriginalPrice.ToString());
            this.PromotionPercent = item.PromotionPercent;
            this.CurrentPriceItem = int.Parse(item.OriginalPrice.ToString()) - ((int.Parse(item.OriginalPrice.ToString()) * int.Parse(item.PromotionPercent.ToString())) / 100);
            this.Quantity = quantity;
            this.TotalDiscountedPrice = (this.UnitPrice - this.CurrentPriceItem) * quantity;
        }
    }
}
