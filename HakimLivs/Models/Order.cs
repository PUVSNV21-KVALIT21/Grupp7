namespace HakimLivs.Models
{
    public class Order
    {
        public int ID { get; set; }
        public AppUser User { get; set; }
        public string DiscountCode { get; set; }

    }
}
