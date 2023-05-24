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
            Subcategories = new();
        }

        public List<SubCategory> Subcategories { get; set; }
        public MainCategory Maincategory { get; set; }
        public List<SubCategory> DbSubCategories { get; set; }
        public async Task<IActionResult> OnGetAsync(int mainCategoryId)
        {
            var mainCategories = await DAL.MainCategoryManager.GetMainCategories();
            var subCategories = await SubCategoryManager.GetSubCategories();

            Maincategory = mainCategories.FirstOrDefault(x => x.Id == mainCategoryId);
            Subcategories.AddRange(subCategories.Where(x => x.MainCategoryId == mainCategoryId));

            return Page();
        }
    }
}
