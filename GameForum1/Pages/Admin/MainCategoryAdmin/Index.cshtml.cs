namespace GameForum1.Pages.Admin.MainCategoryAdmin
{
    public class IndexModel : PageModel
    {
        private readonly GameForum1.Data.GameForum1Context _context;

        public IndexModel(GameForum1.Data.GameForum1Context context)
        {
            _context = context;
        }

        public IList<MainCategory> MainCategory { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.MainCategories != null)
            {
                MainCategory = await _context.MainCategories.ToListAsync();
            }
        }
    }
}
