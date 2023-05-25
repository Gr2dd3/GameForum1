using System.Drawing;

namespace GameForum1.Pages;

public class IndexModel : PageModel
{
    private readonly GameForum1Context _context;
    private readonly SignInManager<GameForum1User> _signInManager;
    private readonly UserManager<GameForum1User> _userManager;

    public IndexModel(GameForum1Context context, UserManager<GameForum1User> userManager, SignInManager<GameForum1User> signInManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public GameForum1User MyUser { get; set; }

    public AppFile ProfilePicture { get; set; }
    [BindProperty]
    public string ImageSrc { get; set; }

    [BindProperty]
    public Image MyImage { get; set; }
    public async Task OnGet()
    {
        MyUser = await _userManager.GetUserAsync(User);




        if (_signInManager.IsSignedIn(User))
        {
            ProfilePicture = _context.File.Where(x => x.UserId == MyUser.Id).FirstOrDefault();

            if (ProfilePicture is not null)
            {
                ImageSrc = string.Format("data:{0};base64,{1}", "jpg", Convert.ToBase64String(ProfilePicture.Content));
            }
        }

    }
}