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
            var foundMainCategory = await DAL.MainCategoryManager.GetMainCategories();

            MainCategory = foundMainCategory.FirstOrDefault(x => x.Id == id);

            if (id == null || foundMainCategory == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            if (MainCategory is not null)
            {
                await DAL.MainCategoryManager.UpdateMainCategory(MainCategory);
            }

            return RedirectToPage("./Index");
        }
    }
}
