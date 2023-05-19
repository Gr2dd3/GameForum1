using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GameForum1.Data;
using GameForum1.Models.DbModels;

namespace GameForum1.Pages.Admin.MainCategoryAdmin
{
    // TODO: MainCategoryManager (anrop till api) gjord. Gör nu CRUD för frontend
    public class CreateModel : PageModel
    {
        private readonly GameForum1.Data.GameForum1Context _context;

        public CreateModel(GameForum1.Data.GameForum1Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public DbMainCategory DbMainCategory { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.MainCategories == null || DbMainCategory == null)
            {
                return Page();
            }

            _context.MainCategories.Add(DbMainCategory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
