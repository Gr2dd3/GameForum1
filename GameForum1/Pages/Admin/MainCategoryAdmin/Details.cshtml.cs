namespace GameForum1.Pages.Admin.MainCategoryAdmin
{
    public class DetailsModel : PageModel
    {
        private readonly GameForum1.Data.GameForum1Context _context;

        public DetailsModel(GameForum1.Data.GameForum1Context context)
        {
            _context = context;
        }

      public MainCategory MainCategory { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //if (id == null || _context.MainCategories == null)
            //{
            //    return NotFound();
            //}

            //var dbmaincategory = await _context.MainCategories.FirstOrDefaultAsync(m => m.Id == id);
            //if (dbmaincategory == null)
            //{
            //    return NotFound();
            //}
            //else 
            //{
            //    MainCategory = dbmaincategory;
            //}
            return Page();
        }
    }
}
