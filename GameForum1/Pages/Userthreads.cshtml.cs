using GameForum1.DAL;
using GameForum1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Drawing;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GameForum1.Pages
{
	public class UserthreadsModel : PageModel
	{
		private readonly UserManager<GameForum1User> _userManager;

		public UserthreadsModel(UserManager<GameForum1User> userManager)
		{
			_userManager = userManager;
			UserThreads = new();
		}

		public GameForum1User MyUser { get; set; }
		[BindProperty]
		public UserThread UserThread { get; set; }
		public SubCategory SubCategory { get; set; }
		public List<UserThread> UserThreads { get; set; }
		public string ImageSrc { get; set; }

        [BindProperty]
        public IFormFile UploadedImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int subCategoryId)
		{
			if (subCategoryId != 0)
			{

				MyUser = await _userManager.GetUserAsync(User);

				var allUserThreads = await DAL.UserThreadManager.GetUserThreads();

				SubCategory = await DAL.SubCategoryManager.GetOneSubCategory(subCategoryId);

				UserThreads.AddRange(allUserThreads.Where(x => x.SubCategoryId == subCategoryId));
            }

			return Page();
		}

		public async Task<IActionResult> OnPostRemoveAsync(int subCategoryId, int removeId)
		{
                var allComments = await DAL.CommentManager.GetComments();

                if (removeId is not 0)
                {
                    var comments = allComments.Where(x => x.UserThreadId == removeId).ToList();

                    comments.ForEach(comment => DAL.CommentManager.DeleteComment(comment.Id).Wait());

                    var userThread = await DAL.UserThreadManager.GetOneUserThread(removeId);
                    if (userThread is not null)
                    {
                        if (System.IO.File.Exists("./wwwroot/img/" + userThread.Image))
                        {
                            System.IO.File.Delete("./wwwroot/img/" + userThread.Image);
                        }
                    }
                    await UserThreadManager.DeleteUserThread(removeId);
                }
            

            return RedirectToPage("/Userthreads", new { SubcategoryId = subCategoryId });
        }

		public async Task<IActionResult> OnPostScoreAsync(int subCategoryId, int userThreadId, int up, int down)
		{
			var userThread = await UserThreadManager.GetOneUserThread(userThreadId);
			if (up is not 0)
			{
                userThread.Score += 1;
				await UserThreadManager.UpdateUserThread(userThread);
			}
			if (down is not 0)
			{
                userThread.Score -= 1;
				await UserThreadManager.UpdateUserThread(userThread);
            }

            return RedirectToPage("/Userthreads", new { SubcategoryId = subCategoryId });
        }
		public async Task<IActionResult> OnPostReport(int subCategoryId, int reportId)
		{
			await DAL.UserThreadManager.ReportThread(reportId);


            return RedirectToPage("/Userthreads", new { SubcategoryId = subCategoryId });

        }
        public async Task<IActionResult> OnPostAsync(int subCategoryId)
		{
			SubCategory = await DAL.SubCategoryManager.GetOneSubCategory(subCategoryId);

			MyUser = await _userManager.GetUserAsync(User);

            string fileName = string.Empty;

            if (UploadedImage != null)
            {
                Random rnd = new Random();
                fileName = rnd.Next(0, 100000).ToString() + UploadedImage.FileName;
                var file = "./wwwroot/img/" + fileName;
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(fileStream);
                }
				UserThread.Image = fileName;
            }
            UserThread.SubCategoryId = subCategoryId;
			UserThread.UserId = MyUser.Id;
			UserThread.Date = DateTime.Now;

			// score +-
			await DAL.UserThreadManager.CreateUserThread(UserThread);

			return RedirectToPage("/Userthreads", new { SubcategoryId = subCategoryId });
		}
		
	}
}
