namespace HakimLivs.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public Product Product { get; set; }
    }
}
