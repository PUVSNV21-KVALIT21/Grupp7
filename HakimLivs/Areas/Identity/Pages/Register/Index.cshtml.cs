using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using HakimLivs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using HakimLivs.Data;

namespace HakimLivs.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> _userManager;
        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public AppUser? AppUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }
        public string? password { get; set; }
        public string? confirmPassword { get; set; }

        public async Task OnGet()
        {
        }

		public async Task<IActionResult> OnPostAddAsync(AppUser appUser, string password, string confirmPassword)
		{
			if (password == confirmPassword)
			{
                var user = new AppUser
                {
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    Address = new Address { Street = appUser.Address.Street, ZipCode = appUser.Address.ZipCode, City = appUser.Address.Street },
                    Email = appUser.Email,
                    EmailConfirmed = true,
                };

                IdentityUser User = user;
                await _userManager.CreateAsync(User, password);
                context.Users.Add(user);

                return RedirectToPage("./Index");
            }
			else
			{
                Message = "Lösenord matchar inte.";
                return RedirectToPage("./Index");
                
            }

        }
	}
}