using HakimLivs.Data;
using HakimLivs.Models;
using HakimLivs.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;


        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            database = context;
            _logger = logger;
            _userManager = userManager;
        }

        public AppUser? appUser { get; set; }

        public async Task OnGetAsync()
        {
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