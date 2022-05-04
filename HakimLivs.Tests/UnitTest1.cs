using HakimLivs.Models;
using HakimLivs.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HakimLivs.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void DiscountPercentageInt()
        {
            Product product = new Product
            {
                Price = 2,
                DiscountPrice = 1
            };

            decimal test = Utils.GetProductDiscountPercentage(product);

            Assert.AreEqual(test, 50);
        }

        [TestMethod]
        public void DiscountPercentageDouble()
        {
            Product product = new Product
            {
                Price = 9.5,
                DiscountPrice = 3.5
            };

            decimal test = Utils.GetProductDiscountPercentage(product);

            Assert.AreEqual(test, 63);
        }

        [TestMethod]
        public void StripHTMLTags()
        {
            string tag = "<p>HEJ</p>";

            string test = Utils.StripHTML(tag);
            Assert.AreEqual(test, "HEJ");
        }
    }
}