using HakimLivs.Data;
using HakimLivs.Models;
using HakimLivs.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }
        public AppUser? AppUser { get; set; }
        public Product? Product { get; set; }
        public List<Product>? Products { get; set; }
        public List<string>? Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(string? category)
        {
            var httpUser = _userManager.GetUserAsync(User).Result;
            if (httpUser != null)
            {
                AppUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == httpUser.Id);
            }

            Categories = await _context.Products.Select(c => c.Category).Distinct().ToListAsync();

            if (category == null)
            {
                Products = await _context.Products.ToListAsync();
            }
            else
            {
                Products = await _context.Products.Where(c => c.Category == category).ToListAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Product = await _context.Products.Include(a => a.AppUsers).SingleAsync(p => p.ID == id);

            Categories = await _context.Products.Select(c => c.Category).Distinct().ToListAsync();
            var httpUser = _userManager.GetUserAsync(User).Result;
            Products = await _context.Products.ToListAsync();

            var user = await _context.Users.Include(p => p.Products).SingleAsync(u => u.Id == httpUser.Id);
            if (user != null)
            {
                user.Products.Add(Product);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Index");
        }
    }
}