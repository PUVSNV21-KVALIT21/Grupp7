using HakimLivs.Data;
using HakimLivs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;

        }

        public List<Product> Products { get; set; }

        public decimal GetProductDiscountPercentage(Product product)
        {
            double? fraction = product.DiscountPrice / product.Price;
            double? percentage = (1 - fraction) * 100;

            return Math.Round(Convert.ToDecimal(percentage), 0);
        }


        public async Task OnGetAsync()
        {
            Products = await _context.Products.ToListAsync();
        }
    }
}