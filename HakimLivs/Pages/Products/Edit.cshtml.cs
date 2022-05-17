#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HakimLivs.Data;
using HakimLivs.Models;
using System.Globalization;
using HakimLivs.Utilities;

namespace HakimLivs.Pages.Products
{
    public class EditModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
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
        [BindProperty(SupportsGet = true)]
        public int? ProductId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (ProductId == null)
            {
                if (id == null)
                {
                    return NotFound();
                }

                Product = await _context.Products.FirstOrDefaultAsync(m => m.ID == id);
                if (SelectListItems.Count() == 0)
                {
                    SelectListItems = await _context.Products.Select(p => p.Category).ToListAsync();
                    SelectListItems.Insert(0, Product.Category);
                    Categories = new SelectList(SelectListItems.Distinct());
                }
                else
                {
                    Categories = new SelectList(SelectListItems);
                }
            }
            else
            {
                Product = await _context.Products.FirstOrDefaultAsync(m => m.ID == ProductId);
                if (SelectListItems.Count() == 0)
                {
                    SelectListItems = await _context.Products.Select(p => p.Category).ToListAsync();
                    SelectListItems.Insert(0, Product.Category);
                    Categories = new SelectList(SelectListItems.Distinct());
                }
                else
                {
                    Categories = new SelectList(SelectListItems);
                }
            }
            UnitTypes = new SelectList(Utils.UnitTypes);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();

        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            
            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Admin/Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
