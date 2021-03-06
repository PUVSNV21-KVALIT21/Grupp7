using HakimLivs.Data;
using HakimLivs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HakimLivs.Pages.MyPages
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

        public AppUser? AppUser { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }
        public bool CanEdit { get; set; } = true;
        public async Task OnGetAsync(string id)
        {
            AppUser = await database.Users.FirstOrDefaultAsync(u => u.Id == id);

            var orderList = await database.Orders.Where(o => o.User.Id == AppUser.Id).ToListAsync();

            foreach (var order in orderList)
            {
                if (order.Status != "Betald")
                {
                    CanEdit = false;
                    break;
                }
            }
        }

        public async Task<IActionResult> OnPostAsync(AppUser appUser)
        {
            var user = await database.Users.FirstOrDefaultAsync(x => x.Id == appUser.Id);
            var emailList = await database.Users.Select(e => e.Email).ToListAsync();
            var email = appUser.Email;

            if (email != user.Email && !emailList.Contains(email) || email == user.Email)
            {
                user.FirstName = appUser.FirstName;
                user.LastName = appUser.LastName;
                user.Address = new Address { Street = appUser.Address.Street, ZipCode = appUser.Address.ZipCode, City = appUser.Address.City };
                user.Email = appUser.Email;
                user.NormalizedEmail = appUser.Email.ToUpper();
                user.UserName = appUser.Email;
                user.NormalizedUserName = appUser.Email.ToUpper();

                await database.SaveChangesAsync();
                Message = "??ndringar sparade.";
                return RedirectToPage("./Index", new { Message, appUser.Id });
            }
            else
            {
                Message = "Email existerar redan, f??rs??k med en ny.";
                return RedirectToPage("./Index", new { Message });
            }
        }

        public async Task<IActionResult> OnPostPassword(AppUser appUser, string Password, string ConfirmPassword)
        {
            //code from https://stackoverflow.com/a/6415638
            string AllowedChars = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";


            if (Regex.IsMatch(Password, AllowedChars))
            {
                if (Password == ConfirmPassword)
                {
                    // code from https://stackoverflow.com/a/45715804

                    var user = await _userManager.FindByIdAsync(appUser.Id);
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, Password);

                    Message = "Nytt l??senord sparat.";
                    return RedirectToPage("./Index", new { Message, appUser });
                }
                else
                {
                    Message = "L??senord matchar inte.";
                    return RedirectToPage("./Index", new { Message, appUser });

                }
            }
            else
            {
                Message = "L??senord m??ste inneh??lla versaler, gemener, specialtecken(#$^+=!*()@%&) och nummer samt vara l??ngre ??n 6 tecken";
                return RedirectToPage("./Index", new { Message });
            }
        }

        //public async Task<IActionResult> OnPostDelete(AppUser appUser)
        //{
        //    var user = await database.Users.FirstOrDefaultAsync(u => u.Id == appUser.Id);

        //    database.Users.Remove(user);
        //    await database.SaveChangesAsync();


        //    return RedirectToPage("../Index");
        //}
    }
}