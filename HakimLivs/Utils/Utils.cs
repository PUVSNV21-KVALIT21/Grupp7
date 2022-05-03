using HakimLivs.Models;

namespace HakimLivs.Utils
{
    public class Utils
    {
        public static decimal GetProductDiscountPercentage(Product product)
        {
            double? fraction = product.DiscountPrice / product.Price;
            double? percentage = (1 - fraction) * 100;

            return Math.Round(Convert.ToDecimal(percentage), 0);
        }
    }
}
