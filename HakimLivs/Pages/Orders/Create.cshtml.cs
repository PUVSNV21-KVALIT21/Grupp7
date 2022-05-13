using HakimLivs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly HakimLivs.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(HakimLivs.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public Order Order { get; set; }

        public List<Product> Products { get; set; }

        // Keep track of quantity per product
        public Dictionary<int, int> ProductsCount { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var httpUser = _userManager.GetUserAsync(User).Result;
            var user = await _context.Users.Include(u => u.Products).FirstOrDefaultAsync(u => u.Id == httpUser.Id);

            // Products = user.Products;

            // test
            var testProduct1 = await _context.Products.Skip(1).FirstAsync();
            var testProduct2 = await _context.Products.Skip(2).FirstAsync();
            var testProduct3 = await _context.Products.Skip(3).FirstAsync();
            var testProduct4 = await _context.Products.Skip(4).FirstAsync();
            var testProduct5 = await _context.Products.Skip(5).FirstAsync();

            Products = new List<Product> {
                testProduct1,
                testProduct1,
                testProduct1,
                testProduct1,
                testProduct2,
                testProduct2,
                testProduct2,
                testProduct3,
                testProduct3,
                testProduct4,
                testProduct5
            };           
            ProductsCount= new Dictionary<int, int>();

            foreach (var product in Products)
            {
                // Add to existing count of product
                if (ProductsCount.ContainsKey(product.ID))
                {
                    ProductsCount[product.ID] += 1;
                }
                // Add new product counter to dict
                else
                {
                    ProductsCount[product.ID] = 1;
                }
            }

            Products = Products.DistinctBy(p => p.ID).ToList();

            return Page();
        }
    }
}
