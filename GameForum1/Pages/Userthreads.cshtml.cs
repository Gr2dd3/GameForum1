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

        public UserthreadsModel(GameForum1Context context, UserManager<GameForum1User> userManager)
        {
            _context = context;
            _userManager = userManager;
            UserThreads = new();
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

            SubCategory = await DAL.SubCategoryManager.GetOneSubCategory(subCategoryId);

            UserThreads.AddRange(allUserThreads.Where(x => x.SubCategoryId == subCategoryId));

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int subCategoryId)
        {
            SubCategory = await DAL.SubCategoryManager.GetOneSubCategory(subCategoryId);

            MyUser = await _userManager.GetUserAsync(User);

            UserThread.SubCategoryId = subCategoryId;
            UserThread.UserId = MyUser.Id;
            UserThread.Date = DateTime.Now;
            // score +-
            await DAL.UserThreadManager.CreateUserThread(UserThread);

            return RedirectToPage("/Userthreads", new { SubcategoryId = subCategoryId });
        }
    }
}
