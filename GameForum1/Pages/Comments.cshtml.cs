using GameForum1.Models;
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
            UserThread = new();
            Comments = new();
            _userManager = userManager;
        }

        public GameForum1User MyUser { get; set; }
        [BindProperty]
        public Comment Comment { get; set; }
        public UserThread UserThread { get; set; }
        public List<Comment> Comments { get; set; }

        // GET COMMENTS FOR SELECTED USERTHREAD
        public async Task<IActionResult> OnGetAsync(int userThreadId)
        {
            MyUser = await _userManager.GetUserAsync(User);

            var allComments = await DAL.CommentManager.GetComments();

            UserThread = await DAL.UserThreadManager.GetOneUserThread(userThreadId);

            Comments.AddRange(allComments.Where(x => x.UserThreadId == userThreadId));
            return Page();

        }

        public async Task<IActionResult> OnPostAsync(int userThreadId)
        {
            UserThread = await DAL.UserThreadManager.GetOneUserThread(userThreadId);

            MyUser = await _userManager.GetUserAsync(User);

            Comment.UserThreadId = userThreadId;
            Comment.UserId = MyUser.Id;
            Comment.Date = DateTime.Now;
            // score +-
            await DAL.CommentManager.CreateComment(Comment);

            return RedirectToPage("/Comments", new { UserthreadId = userThreadId });
        }

    }
}
