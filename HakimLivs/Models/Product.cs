using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HakimLivs.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Namn")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Beskrivning")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Kategori")]
        public string Category { get; set; }
        [Required]
        [Display(Name = "Varumärke")]
        public string Brand { get; set; }
        [Required]
        [Display(Name = "Bild")]
        public string Image { get; set; }
        [Display(Name = "Pris")]
        public double Price { get; set; }
        [Display(Name = "Rabatterat pris")]
        public double? DiscountPrice { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Display(Name = "Jämförpris")]
        public double? ComparisonPrice
        {
            get { return 1 / UnitValue * DiscountPrice;  }
        }            
        [Required]
        [Display(Name = "Ursprungsland")]
        public string Origin { get; set; }
        [Column(TypeName = "smalldatetime")]
        [Display(Name = "Bäst före-datum")]
        public DateTime? ExpirationDate { get; set; }
        [Display(Name = "Lagersaldo")]
        public int Stock { get; set; }
        [Required]
        [Display(Name = "Måttenhet")]
        public string UnitType { get; set; }
        [Display(Name = "Mängd")]
        public double UnitValue { get; set; }
    }
}