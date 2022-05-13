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

        [BindProperty(SupportsGet = true)]
        public string Message { get; set; } 
        
        public Order Order { get; set; }

        public List<Product> Products { get; set; }

        // Keep track of quantity per product
        public Dictionary<int, int> ProductsCount { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            var httpUser = _userManager.GetUserAsync(User).Result;

            if (httpUser != null)
            {
                Products = await _context.Cart
                .Where(c => c.AppUser.Id == httpUser.Id)
                .Select(c => c.Product)
                .ToListAsync();

                CountProducts();
                
            }

            return Page();
        }

        public async Task<IActionResult> OnPostEmptyAsync()
        {
            var httpUser = _userManager.GetUserAsync(User).Result;

            if(httpUser != null)
            {
                var cart = await _context.Cart.Where(c => c.AppUser.Id == httpUser.Id).ToListAsync();

                _context.Cart.RemoveRange(cart);

                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Index");

        }


        public async Task<IActionResult> OnPostAsync()
        {
            var httpUser = _userManager.GetUserAsync(User).Result;

            if (httpUser == null)
            {
                return RedirectToPage("/Index");
            }
            var user = await _context.Users.Where(u => u.Id == httpUser.Id).FirstOrDefaultAsync(); 

            Products = await _context.Cart
                .Where(c => c.AppUser.Id == httpUser.Id)
                .Select(c => c.Product)
                .ToListAsync();

            CountProducts();

            var order = new Order {
                User = user
            };

            foreach (var product in Products)
            {
                var orderProduct = new OrderProduct
                {
                    Product = product,
                    Order = order,
                    Quantity = ProductsCount[product.ID]
                };
                await _context.OrderProducts.AddAsync(orderProduct);
            }

            var cart = await _context.Cart.Where(c => c.AppUser.Id == httpUser.Id).ToListAsync();

            _context.Cart.RemoveRange(cart);            

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            Message = "Tack f�r ditt k�p!";

            return RedirectToPage("/Orders/Create", new { Message });
        }


        private void CountProducts()
        {
            ProductsCount = new Dictionary<int, int>();

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
            
            // Remove duplicate products that have now been counted
            Products = Products.DistinctBy(p => p.ID).ToList();

        }
    }
}