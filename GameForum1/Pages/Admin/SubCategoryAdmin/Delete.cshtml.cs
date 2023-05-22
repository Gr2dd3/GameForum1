using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameForum1.Data;
using GameForum1.Models.DbModels;

namespace GameForum1.Pages.Admin.SubCategoryAdmin
{
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// TODO: Delete funkar inte mot API, det tas bort från databas men inte API.
        /// </summary>
        private readonly GameForum1.Data.GameForum1Context _context;

        public DeleteModel(GameForum1.Data.GameForum1Context context)
        {
            _context = context;
        }

        //  [BindProperty]
        //public DbSubCategory DbSubCategory { get; set; } = default!;

        [BindProperty]
        public SubCategory SubCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }

            SubCategory = await DAL.SubCategoryManager.GetOneSubCategory((int)id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }
            var dbsubcategory = await _context.SubCategories.FindAsync(id);

            if (dbsubcategory != null)
            {
                _context.SubCategories.Remove(dbsubcategory);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception) { }
                await DAL.SubCategoryManager.DeleteSubCategory((int) id);
            }

            return RedirectToPage("./Index");
        }
    }
}
