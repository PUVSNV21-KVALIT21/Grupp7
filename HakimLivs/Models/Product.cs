using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HakimLivs.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Image { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public double ComparisonPrice { get; set; }
        [Required]
        public string Origin { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? ExpirationDate { get; set; }
        public int Stock { get; set; }
        [Required]
        public string UnitType { get; set; }
        public double UnitValue { get; set; }
    }
}
