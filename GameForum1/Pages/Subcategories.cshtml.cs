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
        public List<DbSubCategory> DbSubCategories { get; set; }
        public async Task<IActionResult> OnGetAsync(int mainCategoryId)
        {
            var allSubCategories = await DAL.SubCategoryManager.GetSubCategories();

            DbSubCategories = _context.SubCategories.Where(x => x.MainCategoryId == mainCategoryId).ToList();
            var mainCategories = await DAL.MainCategoryManager.GetMainCategories();
            Maincategory = mainCategories.Where(x => x.Id == mainCategoryId).FirstOrDefault();
            //TODO: Lägga till så att frontend får med sig maincategory Id, så att vi kan lista det för ökad tydlighet

            for (int i = 0; i < DbSubCategories.Count; i++)
            {
                Subcategories.AddRange(allSubCategories.Where(x => x.Id == DbSubCategories[i].Id));
            }



            return Page();
        }
    }
}
