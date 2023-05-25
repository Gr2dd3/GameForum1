using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameForum1.Data;

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
        public SubCategory SubCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            SubCategory = await DAL.SubCategoryManager.GetOneSubCategory((int)id);

            if (id == null || SubCategory == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || SubCategory is not null)
            {
                await DAL.SubCategoryManager.UpdateSubCategory(SubCategory);

                return RedirectToPage("./Index");
            }
            return Page();
        }

        private bool DbSubCategoryExists(int id)
        {
            return (_context.SubCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
