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
        public MainCategory MainCategory { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.MainCategories == null)
            {
                return NotFound();
            }

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

            _context.Attach(MainCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbMainCategoryExists(MainCategory.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            if (MainCategory is not null)
            {
                await DAL.MainCategoryManager.UpdateMainCategory(MainCategory);
            }

            return RedirectToPage("./Index");
        }

        private bool DbMainCategoryExists(int id)
        {
          return (_context.MainCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
