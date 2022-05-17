#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HakimLivs.Data;
using HakimLivs.Models;
using Microsoft.EntityFrameworkCore;
using HakimLivs.Utilities;

namespace HakimLivs.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public SelectList Categories { get; set; }
        public SelectList UnitTypes { get; set; }
        [BindProperty]
        public string SelectedCategory { get; set; }
        [BindProperty]
        public Product Product { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<string> SelectListItems { get; set; }

        public async Task OnGetAsync()
        {
            if (SelectListItems.Count() == 0)
            {
                SelectListItems = await _context.Products.Select(p => p.Category).Distinct().ToListAsync();
                Categories = new SelectList(SelectListItems);
            }
            else
            {
                Categories = new SelectList(SelectListItems);
            }
            UnitTypes = new SelectList(Utils.UnitTypes);
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Admin/Index");
        }
    }
}