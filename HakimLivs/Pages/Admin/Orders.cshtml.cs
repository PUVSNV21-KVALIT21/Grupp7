using HakimLivs.Data;
using HakimLivs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages.Admin
{
    public class OrdersModel : PageModel
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public OrdersModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public List<Order> Orders { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }
        public async Task OnGetAsync()
        {
            Orders = await _context.Orders.Include(o => o.User).ToListAsync();
        }
    }
}
