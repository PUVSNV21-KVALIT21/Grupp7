using HakimLivs.Data;
using HakimLivs.Models;
using HakimLivs.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages.Admin
{
    public class ProductsModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;


        public ProductsModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            database = context;
            _logger = logger;
            _userManager = userManager;
        }

        public List<Product> Products { get; set; }
        public List<string> Categories { get; set; }
        public AppUser? AppUser { get; set; }
        public async Task OnGetAsync(string id)
        {
            var httpUser = _userManager.GetUserAsync(User).Result;
            if (httpUser != null)
            {
                AppUser = await database.Users.FirstOrDefaultAsync(u => u.Id == httpUser.Id);
            }
            else
            {
                AppUser = new AppUser();
            }

            Categories = await database.Products.Select(c => c.Category).Distinct().ToListAsync();
            if (id == null)
            {
                Products = await database.Products.ToListAsync();
            }
            else
            {
                Products = await database.Products.Where(c => c.Category == id).ToListAsync();
            }
        }
    }
}
