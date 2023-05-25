using GameForum1.DAL;
using System;
using System.Collections.Generic;
using System.Linq;


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
      public MainCategory MainCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null /*|| _context.MainCategories == null*/)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null /*|| _context.MainCategories == null*/)
            {
                return NotFound();
            }
            await MainCategoryManager.DeleteMainCategory((int) id);


            return RedirectToPage("./Index");
        }
    }
}
