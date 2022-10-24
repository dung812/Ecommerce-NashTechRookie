using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Moq;
using ShoesShop.Domain;
using ShoesShop.DTO;
using ShoesShop.Service;
using ShoesShop.UI.Controllers;

namespace ShoesShop.Test
{
    public class TestCartController
    {
        [Fact]
        public void Test_AddToCart()
        {
            // Arrange
            var cartItemExpect = new CartViewModel()
            {
                ItemId = 1,
                ProductId = 8,  
                AttributeId = 1,
                AttributeName = "39",
                NameItem = "test name product",
                ImageItem = "",
                UnitPrice = 40,
                PromotionPercent = 5,
                Quantity = 2,
                CurrentPriceItem = 76,
                TotalDiscountedPrice = 4
            };

            int productId = 8;
            int quantity = 2;
            int attributeId = 1;

            var controller = new CartController(null,null,null,null);



            // Act
            var result = controller.AddToCart(productId, quantity, attributeId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CartViewModel>(viewResult.ViewData.Model);
            Assert.Equal(cartItemExpect.AttributeName, model.AttributeName); // Check attribute
        }
    }
}
