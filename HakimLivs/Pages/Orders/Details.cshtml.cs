using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HakimLivs.Data;
using HakimLivs.Models;

namespace HakimLivs.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string SelectedStatus { get; set; }
        public double? Total { get; set; }
        public Order Order { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public AppUser? AppUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders.Include(o => o.User).FirstOrDefaultAsync(o => o.ID == id);
            OrderProducts = await _context.OrderProducts.Include(o => o.Product).Where(o => o.Order.ID == id).ToListAsync();
            AppUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == Order.User.Id);
            if (Order == null)
            {
                return NotFound();
            }

            Total = 0;

            foreach (var orderProduct in OrderProducts)
            {
                double? sumProducts;
                if (orderProduct.Product.DiscountPrice == null || orderProduct.Product.DiscountPrice == 0)
                {
                    sumProducts = (orderProduct.Product.Price * orderProduct.Quantity);
                    Total += sumProducts;
                }
                else
                {
                    sumProducts = (orderProduct.Product.DiscountPrice * orderProduct.Quantity);
                    Total += sumProducts;
                }
                
            }
            return Page();
        }
        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            Order = await _context.Orders.FirstOrDefaultAsync(o => o.ID == id);
            Order.Status = SelectedStatus;
            await _context.SaveChangesAsync();
            return RedirectToPage("/Orders/Details");
        }
    }
}