using HakimLivs.Data;
using HakimLivs.Models;
using System.Text.RegularExpressions;

namespace HakimLivs.Utilities
{
    public class Utils
    {
        public static decimal GetProductDiscountPercentage(Product product)
        {
            double? fraction = product.DiscountPrice / product.Price;
            double? percentage = (1 - fraction) * 100;

            return Math.Round(Convert.ToDecimal(percentage), 0);
        }
        // Code from https://stackoverflow.com/a/18154046
        /// <summary>
        /// Strips the HTML.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>Input string without html tags</returns>
        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        /// <summary>
        /// Delete all rows in the databas.
        /// </summary>
        /// <param ApplicationDbContext="database">The database context for enviroment</param>
       
        public async static Task DropDatabase(ApplicationDbContext database)
        {
            database.Orders.RemoveRange(database.Orders);
            database.OrderProducts.RemoveRange(database.OrderProducts);
            database.Products.RemoveRange(database.Products);
            database.Users.RemoveRange(database.Users);

            await database.SaveChangesAsync();
        }
    }
}
