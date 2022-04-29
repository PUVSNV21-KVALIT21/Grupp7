using HakimLivs.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HakimLivs.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext database;
        private readonly UserManager<IdentityUser> _userManager;
        public IndexModel(ApplicationDbContext database, UserManager<IdentityUser> userManager)
        {
            this.database = database;
            _userManager = userManager;
        }

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public async Task OnGetAsync()
        {
            await DbInitializer.InitializeAsync(database, _userManager);
        }
    }
}