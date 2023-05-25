using GameForum1.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameForum1.Pages
{
    public class SubcategoriesModel : PageModel
    {
        private readonly GameForum1Context _context;

        public SubcategoriesModel(GameForum1Context context)
        {
            _context = context;
            SubCategories = new();
        }

        public List<SubCategory> SubCategories { get; set; }
        public MainCategory Maincategory { get; set; }
        public async Task<IActionResult> OnGetAsync(int mainCategoryId)
        {
            var apiMainCategories = await DAL.MainCategoryManager.GetMainCategories();
            var apiSubCategories = await SubCategoryManager.GetSubCategories();

            
            
            Maincategory = apiMainCategories.FirstOrDefault(x => x.Id == mainCategoryId);

           SubCategories.AddRange(apiSubCategories.Where(x => x.MainCategoryId == mainCategoryId));
            

            return Page();
        }
    }
}
