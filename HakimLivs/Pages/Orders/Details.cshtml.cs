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
        public double Total { get; set; }
        public Order Order { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Order = await _context.Orders.Include(o => o.User).FirstOrDefaultAsync(o => o.ID == id);
            OrderProducts = await _context.OrderProducts.Include(o => o.Product).Where(o => o.Order.ID == id).ToListAsync();

            if (Order == null)
            {
                return NotFound();
            }

            foreach (var product in OrderProducts)
            {
                var sumProducts = (product.Product.Price * product.Quantity);
                Total += sumProducts;
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
