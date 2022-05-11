using HakimLivs.Data;
using HakimLivs.Models;
using HakimLivs.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages.Admin
{
    public class ProductsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public ProductsModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public List<Product> Products { get; set; }
        public List<string> Categories { get; set; }
        public async Task OnGetAsync(string id)
        {
            Categories = await _context.Products.Select(c => c.Category).Distinct().ToListAsync();
            if (id == null)
            {
                Products = await _context.Products.ToListAsync();
            }
            else
            {
                Products = await _context.Products.Where(c => c.Category == id).ToListAsync();
            }
        }
    }
}
