using HakimLivs.Data;
using HakimLivs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages.Admin
{
    public class OrdersModel : PageModel
    {

        private readonly ApplicationDbContext database;
        private readonly UserManager<IdentityUser> _userManager;


        public OrdersModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            database = context;
            _userManager = userManager;
        }

        public List<Order> Orders { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }
        public AppUser? appUser { get; set; }

        public async Task OnGetAsync()
        {
            Orders = await database.Orders.Include(o => o.User).ToListAsync();
            var httpUser = _userManager.GetUserAsync(User).Result;
            appUser = await database.Users.FirstOrDefaultAsync(u => u.Id == httpUser.Id);
        }
    }
}
