using GameForum1.DAL;
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
            CommentsAnswers = new();
            _userManager = userManager;
        }

        public GameForum1User MyUser { get; set; }
        public GameForum1User AuthorUser { get; set; }
        [BindProperty]
        public Comment Comment { get; set; }

        [BindProperty]
        public CommentAnswer Answer { get; set; }
        public UserThread UserThread { get; set; }
        public List<Comment> Comments { get; set; }
        public List<CommentAnswer> CommentsAnswers { get; set; }
        public string ImageSrc { get; set; }


        // GET COMMENTS FOR SELECTED USERTHREAD
        public async Task<IActionResult> OnGetAsync(int userThreadId)
        {
            MyUser = await _userManager.GetUserAsync(User);

            var allComments = await DAL.CommentManager.GetComments();
            var allCommentAnswers = await DAL.CommentAnswerManager.GetCommentAnswers();

            UserThread = await DAL.UserThreadManager.GetOneUserThread(userThreadId);
            AuthorUser = await _userManager.FindByIdAsync(UserThread.UserId);

            Comments.AddRange(allComments.Where(x => x.UserThreadId == userThreadId));
            CommentsAnswers.AddRange(allCommentAnswers);
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
        public async Task<IActionResult> OnPostAnswerAsync(int userThreadId, int commentId)
        {
            MyUser = await _userManager.GetUserAsync(User);

            //Answer.UserThreadId = userThreadId;
            Answer.UserId = MyUser.Id;
            Answer.Date = DateTime.Now;
            Answer.CommentId = commentId;

            await DAL.CommentAnswerManager.CreateCommentAnswer(Answer);

            return RedirectToPage("/Comments", new { UserthreadId = userThreadId });
        }
        public async Task<IActionResult> OnPostScoreAsync(int userThreadId, int commentId, int up, int down)
        {
            var comment = await CommentManager.GetOneComment(commentId);
            if (up is not 0)
            {
                comment.Score += 1;
                await CommentManager.UpdateComment(comment);
            }
            if (down is not 0)
            {
                comment.Score -= 1;
                await CommentManager.UpdateComment(comment);
            }

            return RedirectToPage("/Comments", new { UserThreadId = userThreadId });
        }

        public async Task<IActionResult> OnPostReport(int userThreadId, int reportId)
        {
            if (reportId is not 0)
            {
                await DAL.CommentManager.ReportComment(reportId);
            }

            return RedirectToPage("/Comments", new { UserthreadId = userThreadId });

        }
        public async Task<IActionResult> OnPostRemove(int userThreadId, int removeId)
        {
            if (removeId is not 0)
            {
                await DAL.CommentManager.DeleteComment(removeId);
            }

            return RedirectToPage("/Comments", new { UserthreadId = userThreadId });

        }
    }
}
