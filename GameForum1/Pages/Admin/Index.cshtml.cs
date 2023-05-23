using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace GameForum1.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
