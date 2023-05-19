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
    public class DetailsModel : PageModel
    {
        private readonly GameForum1.Data.GameForum1Context _context;

        public DetailsModel(GameForum1.Data.GameForum1Context context)
        {
            _context = context;
        }

      public DbSubCategory DbSubCategory { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SubCategories == null)
            {
                return NotFound();
            }

            var dbsubcategory = await _context.SubCategories.FirstOrDefaultAsync(m => m.Id == id);
            if (dbsubcategory == null)
            {
                return NotFound();
            }
            else 
            {
                DbSubCategory = dbsubcategory;
            }
            return Page();
        }
    }
}
