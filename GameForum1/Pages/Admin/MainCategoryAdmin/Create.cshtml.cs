namespace GameForum1.Pages.Admin.MainCategoryAdmin;

// TODO: MainCategoryManager (anrop till api) gjord. Gör nu CRUD för frontend
public class CreateModel : PageModel
{
    private readonly GameForum1Context _context;

    public CreateModel(GameForum1Context context)
    {
        _context = context;
    }

    [BindProperty]
    public MainCategory MainCategory { get; set; }
    public List<MainCategory> MainCategories { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        MainCategories = await DAL.MainCategoryManager.GetMainCategories();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        MainCategories = await DAL.MainCategoryManager.GetMainCategories();

        if (ModelState.IsValid)
        {

            await DAL.MainCategoryManager.CreateMainCategory(MainCategory);
        }

        return RedirectToPage("./Index");
    }
}
