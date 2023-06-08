using GameForum1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;

namespace GameForum1.Pages
{
    public class CommentDetailsModel : PageModel
    {
        private readonly UserManager<GameForum1User> _userManager;
        public CommentDetailsModel(UserManager<GameForum1User> userManager)
        {
            CommentAnswer = new();
            CommentUser = new();
            _userManager = userManager;
        }
        public string ImageSrc { get; set; }
		public Comment Comment { get; set; }
        public GameForum1User CommentUser { get; set; }

        [BindProperty]
        public CommentAnswer CommentAnswer { get; set; }
        public async Task<IActionResult> OnGetAsync(int commentId, int userThreadId)
        {
            Comment = await DAL.CommentManager.GetOneComment(commentId);
            CommentUser = await _userManager.FindByIdAsync(Comment.UserId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int userThreadId, int commentId)
        {
            CommentAnswer.Reported = false;
            CommentAnswer.Date = DateTime.Now;
            CommentAnswer.CommentId = commentId;
            await DAL.CommentAnswerManager.CreateCommentAnswer(CommentAnswer);

            return RedirectToPage("/Comments", new { UserThreadId = userThreadId });
        }
    }
}
