namespace GameForum1.Pages.Admin.SubCategoryAdmin;

public class CreateModel : PageModel
{
    /// <summary>
    /// TODO: Mattias söndag - Skapat bakomliggande kod till Create-page SubCategory.
    /// Även gjort i Frontend men måste dubbelkolla om det funkar.
    /// </summary>

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

        var existingSubCategory = subCategories.Where(x => x.Name.ToLower() == SubCategory.Name.ToLower()).FirstOrDefault();
        if (ModelState.IsValid && _context.SubCategories is not null && existingSubCategory is null)
        {
            if (SubCategory is not null)
            {
                SaveNewDbSubCategoryToDataBase(mainCategoryId);
                var subCategoryId = _context.SubCategories.ToList().TakeLast(1).Select(x => x.Id).FirstOrDefault();
                SubCategory.Id = subCategoryId;
                await DAL.SubCategoryManager.CreateSubCategory(SubCategory);
            }

        }

        return RedirectToPage("./Index");
    }

    public void SaveNewDbSubCategoryToDataBase(int mainCategoryId)
    {
        var existingCategory = _context.SubCategories.Where(x => x.Id == SubCategory.Id).FirstOrDefault();

        if (existingCategory is null)
        {
            var newDbSubCategory = new DbSubCategory
            {
                Id = SubCategory.Id,
                MainCategoryId = mainCategoryId
            };

            _context.SubCategories.Add(newDbSubCategory);
            _context.SaveChanges();
        }
    }

}
