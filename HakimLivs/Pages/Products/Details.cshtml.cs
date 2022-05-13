using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HakimLivs.Data;
using HakimLivs.Models;
using Microsoft.AspNetCore.Identity;

namespace HakimLivs.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DetailsModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Product Product { get; set; }
        public AppUser AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var httpUser = _userManager.GetUserAsync(User).Result;
            if (httpUser != null)
            {
                AppUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == httpUser.Id);
            }

            Product = await _context.Products.FirstOrDefaultAsync(m => m.ID == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Product = await _context.Products.SingleAsync(p => p.ID == id);

            var httpUser = _userManager.GetUserAsync(User).Result;

            var user = await _context.Users.SingleAsync(u => u.Id == httpUser.Id);
            if (user != null)
            {
                var cart = new Cart();
                cart.Product = Product;
                cart.AppUser = user;

                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("../Index");
        }
    }
}