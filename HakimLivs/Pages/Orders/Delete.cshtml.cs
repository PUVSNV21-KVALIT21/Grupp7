using HakimLivs.Data;
using HakimLivs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public Order Order { get; set; }
        public string? Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Order = await _context.Orders.Include(u => u.User).FirstOrDefaultAsync(o => o.ID == id);

            return Page();

        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
             Order = await _context.Orders.FirstOrDefaultAsync(u => u.ID == id);
             _context.Orders.Remove(Order);
             _context.SaveChanges();
            Message = "Beställning nr:" + id + "är nu borttagen.";

            return RedirectToPage("../Admin/Orders", new { Message });
        }
    }
}
