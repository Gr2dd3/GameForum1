using GameForum1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameForum1.Pages.Admin
{
    public class ReportedCommentsModel : PageModel
    {
        public List<Comment> ReportedComments { get; set; }
        public string ImageSrc { get; set; }

        public ReportedCommentsModel()
        {
            ReportedComments = new();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var allComments = await DAL.CommentManager.GetComments();


            ReportedComments.AddRange(allComments.Where(x => x.Reported == true));
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int reportId)
        {
            await DAL.CommentManager.DeleteComment(reportId);

            return RedirectToPage("./ReportedComments");
        }
        public async Task<IActionResult> OnPostRestoreAsync(int reportId)
        {
            var comment = await DAL.CommentManager.GetOneComment(reportId);

            comment.Reported = false;

            await DAL.CommentManager.UpdateComment(comment);

            return RedirectToPage("./ReportedComments");
        }
    }
}
