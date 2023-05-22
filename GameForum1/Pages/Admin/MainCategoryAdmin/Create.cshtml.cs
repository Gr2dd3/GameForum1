namespace GameForum1.Pages.Admin.MainCategoryAdmin;

// TODO: MainCategoryManager (anrop till api) gjord. Gör nu CRUD för frontend
public class CreateModel : PageModel
{
    /// <summary>
    /// TODO: Mattias söndag - Skapat Bakomliggande kod till Create-page MainCategory
    /// </summary>
    private readonly GameForum1Context _context;

    public CreateModel(GameForum1Context context)
    {
        _context = context;
    }


    public List<MainCategory> MainCategories { get; set; }

    [BindProperty]
    public DbMainCategory DbMainCategory { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        MainCategories = await DAL.MainCategoryManager.GetMainCategories();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        MainCategories = await DAL.MainCategoryManager.GetMainCategories();
        var existingMainCategory = MainCategories.Where(x => x.Name.ToLower() == DbMainCategory.Name.ToLower()).FirstOrDefault();

        if (ModelState.IsValid && DbMainCategory is not null && existingMainCategory is null)
        {
            SaveNewMainCategoryIdToDatabase();
            var mainCategoryId = _context.MainCategories.ToList().TakeLast(1).Select(x => x.Id).FirstOrDefault();

            var mainCategory = new MainCategory
            {
                Id = mainCategoryId,
                Name = DbMainCategory.Name
            };
            await DAL.MainCategoryManager.CreateMainCategory(mainCategory);
        }

        return RedirectToPage("./Index");
    }

    public void SaveNewMainCategoryIdToDatabase()
    {
        if (DbMainCategory is not null)
        {
            var existingCategory = _context.MainCategories.Where(x => x.Id == DbMainCategory.Id).FirstOrDefault();

            if (existingCategory is null)
            {
                _context.MainCategories.Add(DbMainCategory);
                _context.SaveChanges();
            }
        }
    }
}
