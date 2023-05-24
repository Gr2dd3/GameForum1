using GameForum1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Security.Claims;
using System.Security.Cryptography;

namespace GameForum1.Pages
{
    public class UserthreadsModel : PageModel
    {
        private readonly GameForum1Context _context;
        
        private readonly UserManager<GameForum1User> _userManager;

        private static UserThread _userThread;

        public UserthreadsModel(GameForum1Context context, UserManager<GameForum1User> userManager, UserThread scopedUserthread, List<UserThread> scopedUserThreads)
        {
            _context = context;
            //UserThreads = new();            // vi tappar userthread?
            _userManager = userManager;
            UserThread= scopedUserthread;
            UserThreads = scopedUserThreads;
        }

        public GameForum1User MyUser { get; set; }
        [BindProperty]
        public UserThread UserThread { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<UserThread> UserThreads { get; set; }
        public async Task<IActionResult> OnGetAsync(int subCategoryId)
        {
            

            MyUser = await _userManager.GetUserAsync(User);

            var allUserThreads = await DAL.UserThreadManager.GetUserThreads();

            DbUserThreads = _context.UserThreads.Where(x => x.SubCategoryId == subCategoryId).ToList();

            SubCategory = await DAL.SubCategoryManager.GetOneSubCategory(subCategoryId);


            for (int i = 0; i < DbUserThreads.Count; i++)
            {
                UserThreads.AddRange(allUserThreads.Where(x => x.Id == DbUserThreads[i].Id));
            }



            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int subCategoryId)
        {
            SubCategory = await DAL.SubCategoryManager.GetOneSubCategory(subCategoryId);




            SaveNewDbUserThreadToDataBase(subCategoryId);

            var userThreadId = _context.UserThreads.ToList().TakeLast(1).Select(x => x.Id).FirstOrDefault();

            UserThread.Id = userThreadId;
            UserThread.Date = DateTime.Now;
            // score +-
            await DAL.UserThreadManager.CreateUserThread(UserThread);





            //OnGetAsync(subCategoryId);
            //return RedirectToPage("/Userthreads" + subCategoryId);
            return RedirectToPage("/Userthreads", new { SubcategoryId = subCategoryId });
        }
        public void SaveNewDbUserThreadToDataBase(int subCategoryId)
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
