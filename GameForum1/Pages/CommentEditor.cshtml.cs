using GameForum1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameForum1.Pages
{
    public class CommentEditorModel : PageModel
    {
        private readonly UserManager<GameForum1User> _userManager;

        public CommentEditorModel(UserManager<GameForum1User> userManager)
        {
            Comment = new();
            _userManager = userManager;
        }

        public GameForum1User MyUser { get; set; }
        public GameForum1User AuthorUser { get; set; }
        [BindProperty]
        public Comment Comment { get; set; }
        public UserThread UserThread { get; set; }
        public string ImageSrc { get; set; }

        public async Task<IActionResult> OnGetAsync(int editId)
        {
            Comment = await DAL.CommentManager.GetOneComment(editId);




            return Page();

        }
        public async Task<IActionResult> OnPostEditAsync()
        {
           



            await DAL.CommentManager.UpdateComment(Comment);




            return RedirectToPage("./Comments", new { UserThreadId = Comment.UserThreadId });

        }

    }
}
