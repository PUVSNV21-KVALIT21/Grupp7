using HakimLivs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HakimLivs.Data;
using System.Text.RegularExpressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages.Register
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext database;
        private readonly UserManager<IdentityUser> _userManager;
        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this.database = context;
            _userManager = userManager;
        }

        [BindProperty]
        public AppUser AppUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }
        public string? password { get; set; }
        public string? confirmPassword { get; set; }

        public async Task OnGet()
        {
        } 

		public async Task<IActionResult> OnPostAddAsync(AppUser appUser, string password, string confirmPassword)
		{
            //code from https://stackoverflow.com/a/6415638
            string AllowedChars = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";
            var emailList = await database.Users.Select(e => e.Email).ToListAsync();

            if (!emailList.Contains(appUser.Email))
            {
                if (Regex.IsMatch(password, AllowedChars))
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
                            UserName = appUser.Email
                        };
                        IdentityUser User = user;
                        await _userManager.CreateAsync(User, password);
                        //database.Users.Add(user);

                        await database.SaveChangesAsync();
                        return RedirectToPage("../Index");
                    }
                    else
                    {
                        Message = "Lösenord matchar inte.";
                        return RedirectToPage("./Index", new { Message, appUser });

                    }
                }
                else
                {
                    Message = "Lösenord måste innehålla versaler, gemener, specialtecken(#$^+=!*()@%&) och nummer samt vara längre än 6 tecken";
                    return RedirectToPage("./Index", new { Message });
                }
            }
            else
            {
                Message = "Email existerar redan, försök med en ny.";
                return RedirectToPage("./Index", new { Message });
            }
        }
	}
}