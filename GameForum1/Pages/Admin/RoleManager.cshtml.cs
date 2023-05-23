using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace GameForum1.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class RoleManagerModel : PageModel

    {
        public readonly UserManager<GameForum1User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleManagerModel(RoleManager<IdentityRole> roleManager, UserManager<GameForum1User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public List<GameForum1User> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }


        [BindProperty]
        public string RoleName { get; set; }


        [BindProperty(SupportsGet = true)]
        public string RemoveUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }

        public bool IsUser { get; set; }
        public bool IsAdmin { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();
            Users = await _userManager.Users.ToListAsync();

            if (AddUserId is not null)
            {
                var alterUser = await _userManager.FindByIdAsync(AddUserId);
                var roleResult = await _userManager.AddToRoleAsync(alterUser, Role);
            }

            if (RemoveUserId is not null)
            {
                var alterUser = await _userManager.FindByIdAsync(RemoveUserId);
                var roleResult = await _userManager.RemoveFromRoleAsync(alterUser, Role);
            }

            //Demo av roller (i OnGetAsync())
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser is not null)
            {
                IsUser = await _userManager.IsInRoleAsync(currentUser, "User");
                IsAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (RoleName is not null)
            {
                await CreateRole(RoleName);
            }


            return RedirectToPage("./RoleManager");
        }

        public async Task CreateRole(string roleName)
        {
            bool exist = await _roleManager.RoleExistsAsync(roleName);
            if (!exist)
            {
                var role = new IdentityRole
                {
                    Name = roleName
                };

                await _roleManager.CreateAsync(role);
            }
        }
    }
}
