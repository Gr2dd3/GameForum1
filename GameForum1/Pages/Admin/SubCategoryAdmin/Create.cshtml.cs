namespace GameForum1.Pages.Admin.SubCategoryAdmin;

public class CreateModel : PageModel
{
    private readonly GameForum1Context _context;

    public CreateModel(GameForum1.Data.GameForum1Context context)
    {
        _context = context;
    }

    [BindProperty]
    public MainCategory MainCategory { get; set; }
    [BindProperty]
    public SubCategory SubCategory { get; set; }
    public List<SubCategory> SubCategories { get; set; }    

    public async Task<IActionResult> OnGetAsync(int mainCategoryId)
    {
		SubCategories = await DAL.SubCategoryManager.GetSubCategories();
		var mainCategories = await DAL.MainCategoryManager.GetMainCategories();
        MainCategory = mainCategories.FirstOrDefault(x => x.Id == mainCategoryId);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int mainCategoryId)
    {
        var subCategories = await DAL.SubCategoryManager.GetSubCategories();

        if (ModelState.IsValid)
        {
            SubCategory.MainCategoryId = mainCategoryId;
            await DAL.SubCategoryManager.CreateSubCategory(SubCategory);
        }

        return RedirectToPage("./Index");
    }
}
