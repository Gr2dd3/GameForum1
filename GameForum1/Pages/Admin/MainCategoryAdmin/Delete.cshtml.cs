using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameForum1.Data;
using GameForum1.Models.DbModels;

namespace GameForum1.Pages.Admin.MainCategoryAdmin
{
    public class DeleteModel : PageModel
    {
        private readonly GameForum1.Data.GameForum1Context _context;

        public DeleteModel(GameForum1.Data.GameForum1Context context)
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

            var dbmaincategory = await _context.MainCategories.FirstOrDefaultAsync(m => m.Id == id);

            if (dbmaincategory == null)
            {
                return NotFound();
            }
            else 
            {
                DbMainCategory = dbmaincategory;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.MainCategories == null)
            {
                return NotFound();
            }
            var dbmaincategory = await _context.MainCategories.FindAsync(id);

            if (dbmaincategory != null)
            {
                DbMainCategory = dbmaincategory;
                _context.MainCategories.Remove(DbMainCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
