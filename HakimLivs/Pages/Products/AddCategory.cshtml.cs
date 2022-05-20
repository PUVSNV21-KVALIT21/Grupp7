using HakimLivs.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HakimLivs.Pages.Products
{
    public class AddCategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public AddCategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string Category { get; set; }
        public async Task OnGetAync()
        {
        }

        public async Task<IActionResult> OnPostAsync(string Category, string id, int ProductId)
        {
            var selectListItems = await _context.Products.Select(p => p.Category).Distinct().ToListAsync();

            selectListItems.Insert(0, Category);
            if (id == "create")
            {
                return RedirectToPage("/Products/Create", new { selectListItems });
            }
            else
            {
                return RedirectToPage("/Products/Edit", new { selectListItems, ProductId });
            }
        }
    }
}
