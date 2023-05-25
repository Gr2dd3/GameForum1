using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameForum1.Data;
namespace GameForum1.Pages.Admin.SubCategoryAdmin
{
    public class DeleteModel : PageModel
    {
        private readonly GameForum1.Data.GameForum1Context _context;

        public DeleteModel(GameForum1.Data.GameForum1Context context)
        {
            _context = context;
        }

        [BindProperty]
        public SubCategory SubCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            SubCategory = await DAL.SubCategoryManager.GetOneSubCategory((int)id);
           

            if (id == null || SubCategory == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await DAL.SubCategoryManager.DeleteSubCategory((int)id);

            return RedirectToPage("./Index");
        }
    }
}
