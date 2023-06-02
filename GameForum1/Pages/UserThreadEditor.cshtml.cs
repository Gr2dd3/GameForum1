namespace GameForum1.Pages;

public class UserThreadEditorModel : PageModel
{
    private readonly UserManager<GameForum1User> _userManager;

    public UserThreadEditorModel(UserManager<GameForum1User> userManager)
    {
        UserThread = new();
        _userManager = userManager;
    }

    public GameForum1User MyUser { get; set; }
    public GameForum1User AuthorUser { get; set; }
    [BindProperty]
    public UserThread UserThread { get; set; }

    public SubCategory SubCategory { get; set; }
    public string ImageSrc { get; set; }

    public async Task<IActionResult> OnGetAsync(int editId)
    {
        UserThread = await DAL.UserThreadManager.GetOneUserThread(editId);

        return Page();
    }
    public async Task<IActionResult> OnPostEditAsync()
    {
        await DAL.UserThreadManager.UpdateUserThread(UserThread);

        return RedirectToPage("./Userthreads", new { SubCategoryId = UserThread.SubCategoryId });
    }
}
