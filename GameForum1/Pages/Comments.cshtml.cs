using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace GameForum1.Pages
{
    public class CommentsModel : PageModel
    {
        private readonly GameForum1Context _context;

        private readonly UserManager<GameForum1User> _userManager;

        public CommentsModel(GameForum1Context context, UserManager<GameForum1User> userManager)
        {
            _context = context;
            UserThread = new();            // vi tappar userthread?
            _userManager = userManager;
        }

        public GameForum1User MyUser { get; set; }
        [BindProperty]
        public Comment Comment { get; set; }
        public UserThread UserThread { get; set; }
        public List<Comment> Comments { get; set; }
        //public List<DbComment> DbComments { get; set; }
        public async Task<IActionResult> OnGetAsync(int userThreadId)
        {

            MyUser = await _userManager.GetUserAsync(User);

            Comments = await DAL.CommentManager.GetComments();

            UserThread = await DAL.UserThreadManager.GetOneUserThread(userThreadId);

            return Page();
        }
    }
}
