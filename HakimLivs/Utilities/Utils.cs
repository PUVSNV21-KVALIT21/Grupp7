using HakimLivs.Data;
using HakimLivs.Models;
using System.Globalization;
using System.Text.RegularExpressions;

namespace HakimLivs.Utilities
{    
    public class Utils
    {        
        public static List<string> UnitTypes = new List<string>
        {
            "kg", "g", "mg", "l", "cl", "ml", "st"
        };

        /// <summary>
        /// Decodes the HTML.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>String with no encoded characters (plain text).</returns>
        public static string DecodeHTML(string text)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            return System.Web.HttpUtility.HtmlDecode(text);
        }

        /// <summary>
        /// Displays the price with a standard format.
        /// </summary>
        /// <param name="price">The price.</param>
        /// <returns>Price in a formatted string.</returns>
        public static string DisplayPrice(double? price)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            if (price.ToString().Contains('.')) {
                var priceParts = price.ToString().Split(".");

                string dec = priceParts[1];

                if (dec.Length == 1)
                {
                    dec += "0";
                }
                if (dec.Length > 2)
                {
                    dec = dec.Substring(0, 2);
                }

                return priceParts[0] + "," + dec + " kr";
            }

            return price.ToString() + ":-";
        }

        /// <summary>
        /// Displays the unit value with a standard format.
        /// </summary>
        /// <param name="unitValue">The unit value.</param>
        /// <returns>Unit value in a formatted string.</returns>
        public static string DisplayUnitvalue(double? unitValue)
        {
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            if (unitValue.ToString().Contains('.'))
            {
                var unitParts = unitValue.ToString().Split(".");

                string dec = unitParts[1];

                if (dec.Length > 2)
                {
                    dec = dec.Substring(0, 2);
                }

                return unitParts[0] + "," + dec;
            }

            return unitValue.ToString();
        }

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