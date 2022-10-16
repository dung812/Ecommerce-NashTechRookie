using ShoesShop.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test
{
    public class TestFunctions
    {
        [Fact]
        public void Test_DiscountedPriceCalulator_Method()
        {
            // Arrange
            int oldPrice = 49;
            int promotionPercent = 5;
            int priceExpected = 47;

            // Act
            int result = Functions.DiscountedPriceCalulator(oldPrice, promotionPercent);

            // Assert
            Assert.Equal(priceExpected, result);
        }
    }
}
