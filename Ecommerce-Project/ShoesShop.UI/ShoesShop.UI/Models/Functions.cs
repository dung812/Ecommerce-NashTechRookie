using ShoesShop.Domain;

namespace ShoesShop.UI.Models
{
    public class Functions
    {
        public static int DiscountedPriceCalulator(int OriginalPrice, int PromotionPercent)
        {
            return OriginalPrice - ((OriginalPrice * PromotionPercent) / 100);
        }

        public static int AverageRatingCalculator(List<CommentProduct> comments)
        {
            var totalComment = comments.Count();
            var SumStarValue = 0;
            var result = 0;
            foreach (var comment in comments)
            {
                SumStarValue += comment.Star;
            }
            result = (int)(SumStarValue / totalComment);

            return result;
        }
    }
}
