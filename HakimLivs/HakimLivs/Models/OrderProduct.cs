namespace HakimLivs.Models
{
    public class OrderProduct
    {
        public int ID { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
