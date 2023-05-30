using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GameForum1.Data;

namespace GameForum1.Pages.Admin.SubCategoryAdmin
{
    public class IndexModel : PageModel
    {
        public List<SubCategory> SubCategories { get; set; }

        public async Task OnGetAsync()
        {
            SubCategories = await DAL.SubCategoryManager.GetSubCategories();
        }
    }
}
