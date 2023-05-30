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
    public class DetailsModel : PageModel
    {
        private readonly GameForum1Context _context;

        public DetailsModel(GameForum1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            return Page();
        }
    }
}
