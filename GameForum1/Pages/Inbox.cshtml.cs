using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameForum1.Pages
{
    public class InboxModel : PageModel
    {
        private readonly GameForum1Context _context;
        private readonly UserManager<GameForum1User> _userManager;

        public InboxModel(GameForum1Context context, UserManager<GameForum1User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public DbPrivateMessage Message { get; set; }


        // Om man väljer från inbox-sidan
        public GameForum1User Recipient { get; set; }

        // Om man kommer från ett inlägg/kommentar
        public GameForum1User RecipientUser { get; set; }

        public List<GameForum1User> AllUsers { get; set; }


        // Om man väljer från inbox-sidan
        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            AllUsers = await _userManager.Users.ToListAsync();

            return Page();
        }

        //// Om man kommer från ett inlägg/kommentar
        //public async Task<IActionResult> OnPostAsync(string userName)
        //{
        //    var currentUser = await _userManager.GetUserAsync(User);
        //    RecipientUser = await _userManager.FindByNameAsync(userName);



        //    return Page();
        //}
    }
}
