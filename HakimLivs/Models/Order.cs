using System.ComponentModel.DataAnnotations;

namespace HakimLivs.Models
{
    
    public enum PaymentMethod
    {
        Swish,
        Klarna,
        Faktura,
        Kreditkort,
        Banköverföring
    }

    public enum DeliveryMethod
    {
        Hemleverans,
        Expressleverans,
        [Display(Name = "Hämta i butik")]
        HämtaIButik
    }

    public class Order
    {
        public int ID { get; set; }
        public AppUser User { get; set; }
        public string? DiscountCode { get; set; }
        public string Status { get; set; } = "Inkommen";
        public PaymentMethod PaymentMethod { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
    }
}