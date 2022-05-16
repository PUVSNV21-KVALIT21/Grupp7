using HakimLivs.Data;
using HakimLivs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace HakimLivs.Pages.Admin
{
    public class CustomersModel : PageModel
    {

        private readonly ApplicationDbContext database;
        private readonly UserManager<IdentityUser> _userManager;


        public CustomersModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            database = context;
            _userManager = userManager;
        }

        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }
        public AppUser? appUser { get; set; }
        public List<AppUser> appUsers { get; set; }

        public async Task OnGetAsync()
        {
            appUsers = await database.Users.ToListAsync();
            var httpUser = _userManager.GetUserAsync(User).Result;
            if (httpUser != null)
            {
                appUser = await database.Users.FirstOrDefaultAsync(u => u.Id == httpUser.Id);
            }
            else
            {
                appUser = new AppUser();
            }
        }
    }
}
