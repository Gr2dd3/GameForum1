namespace GameForum1.Pages.Admin.SubCategoryAdmin;

public class CreateModel : PageModel
{
    private readonly GameForum1Context _context;

    public CreateModel(GameForum1.Data.GameForum1Context context)
    {
        _context = context;
    }

    [BindProperty]
    public SubCategory SubCategory { get; set; }


    public async Task<IActionResult> OnGetAsync()
    {
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
