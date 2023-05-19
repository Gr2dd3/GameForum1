namespace GameForum1.Pages.Admin.SubCategoryAdmin;

public class CreateModel : PageModel
{
    private readonly GameForum1Context _context;
    private readonly SubCategory _subCategory;

    public CreateModel(GameForum1.Data.GameForum1Context context, SubCategory subCategory)
    {
        _context = context;
        _subCategory = subCategory;
    }

    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public DbSubCategory DbSubCategory { get; set; } = default!;


    public IActionResult OnGet() { return Page(); }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid || _context.SubCategories == null || DbSubCategory == null)
        {
            _subCategory.Name = Name;
            if (Name is not null)
            {
                // DAL. Metod för att skicka infon till API POST om Ny
            }

            //Get all subcategories again
            return Page();
        }

        _context.SubCategories.Add(DbSubCategory);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Create");
    }
}
