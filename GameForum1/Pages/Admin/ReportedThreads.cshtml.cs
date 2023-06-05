using GameForum1.DAL;
using GameForum1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Threading.Tasks;

namespace GameForum1.Pages.Admin
{
    public class ReportedThreadsModel : PageModel
    {
        public List<UserThread> ReportedUserThreads { get; set; }
        public string ImageSrc { get; set; }
        public ReportedThreadsModel()
        {
            ReportedUserThreads = new();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var allThreads = await DAL.UserThreadManager.GetUserThreads();

            ReportedUserThreads.AddRange(allThreads.Where(x => x.Reported == true));

            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int reportId)
        {
            var allComments = await DAL.CommentManager.GetComments();

            if (reportId is not 0)
            {
                var comments = allComments.Where(x => x.UserThreadId == reportId).ToList();

                comments.ForEach(comment => DAL.CommentManager.DeleteComment(comment.Id).Wait());

                var userThread = await DAL.UserThreadManager.GetOneUserThread(reportId);
                if (userThread is not null)
                {
                    if (System.IO.File.Exists("./wwwroot/img/" + userThread.Image))
                    {
                        System.IO.File.Delete("./wwwroot/img/" + userThread.Image);
                    }
                }
                await UserThreadManager.DeleteUserThread(reportId);
            }

            return RedirectToPage("./ReportedThreads");
        }
        public async Task<IActionResult> OnPostRestoreAsync(int reportId)
        {
            var userThread = await DAL.UserThreadManager.GetOneUserThread(reportId);

            userThread.Reported = false;

            await DAL.UserThreadManager.UpdateUserThread(userThread);

            return RedirectToPage("./ReportedThreads");
        }
    }
}
