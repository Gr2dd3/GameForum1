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
        public List<DbComment> DbComments { get; set; }
        public async Task<IActionResult> OnGetAsync(int userThreadId)
        {

            MyUser = await _userManager.GetUserAsync(User);

            var allComments = await DAL.CommentManager.GetComments();

            DbComments = _context.Comments.Where(x => x.UserThreadId == userThreadId).ToList();

            UserThread = await DAL.UserThreadManager.GetOneUserThread(userThreadId);


            for (int i = 0; i < DbComments.Count; i++)
            {
                Comments.AddRange(allComments.Where(x => x.Id == DbComments[i].Id));
            }



            return Page();
        }
        //public async Task<IActionResult> OnPostAsync(int userThreadId)
        //{
        //    UserThread = await DAL.UserThreadManager.GetOneUserThread(userThreadId);




        //    SaveNewDbCommentToDataBase(userThreadId);

        //    var userThreadId = _context.UserThreads.ToList().TakeLast(1).Select(x => x.Id).FirstOrDefault();

        //    UserThread.Id = userThreadId;
        //    UserThread.Date = DateTime.Now;
        //    // score +-
        //    await DAL.UserThreadManager.CreateUserThread(UserThread);





        //    //OnGetAsync(subCategoryId);
        //    return Page();
        //}
        public void SaveNewDbCommentToDataBase(int subCategoryId)
        {
            var existingDbUserThread = _context.UserThreads.Where(x => x.Id == UserThread.Id).FirstOrDefault();

            if (existingDbUserThread is null)
            {
                var _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var newDBUserThread = new DbUserThread()
                {
                    Id = UserThread.Id,
                    SubCategoryId = subCategoryId,
                    UserId = _userId

                };
                _context.UserThreads.Add(newDBUserThread);
                _context.SaveChanges();
            }
        }

    }
}
