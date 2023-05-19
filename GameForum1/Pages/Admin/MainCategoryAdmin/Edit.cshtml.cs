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

namespace GameForum1.Pages.Admin.MainCategoryAdmin
{
    public class EditModel : PageModel
    {
        private readonly GameForum1.Data.GameForum1Context _context;

        public EditModel(GameForum1.Data.GameForum1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public DbMainCategory DbMainCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MainCategories == null)
            {
                return NotFound();
            }

            var dbmaincategory =  await _context.MainCategories.FirstOrDefaultAsync(m => m.Id == id);
            if (dbmaincategory == null)
            {
                return NotFound();
            }
            DbMainCategory = dbmaincategory;
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

            _context.Attach(DbMainCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbMainCategoryExists(DbMainCategory.Id))
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

        private bool DbMainCategoryExists(int id)
        {
          return (_context.MainCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
