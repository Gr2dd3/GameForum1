using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameForum1.Data;
using GameForum1.Models.DbModels;

namespace GameForum1.Pages.Admin.SubCategoryAdmin
{
    public class EditModel : PageModel
    {
        private readonly GameForum1.Data.GameForum1Context _context;

        public EditModel(GameForum1.Data.GameForum1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public DbSubCategory DbSubCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }

            var dbsubcategory =  await _context.SubCategories.FirstOrDefaultAsync(m => m.Id == id);
            if (dbsubcategory == null)
            {
                return NotFound();
            }
            DbSubCategory = dbsubcategory;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DbSubCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbSubCategoryExists(DbSubCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DbSubCategoryExists(int id)
        {
          return (_context.SubCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
