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
    public class IndexModel : PageModel
    {
        private readonly GameForum1.Data.GameForum1Context _context;

        public IndexModel(GameForum1.Data.GameForum1Context context)
        {
            _context = context;
        }

        public IList<DbSubCategory> DbSubCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.SubCategories != null)
            {
                DbSubCategory = await _context.SubCategories.ToListAsync();
            }
        }
    }
}
