using GameForum1.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameForum1.Pages
{
    public class InboxModel : PageModel
    {
        private readonly GameForum1Context _context;
        private readonly UserManager<GameForum1User> _userManager;
        private readonly MessageManager _messageManager;

        public InboxModel(GameForum1Context context, UserManager<GameForum1User> userManager, MessageManager messageManager)
        {
            _context = context;
            _userManager = userManager;
            _messageManager = messageManager;
            AllUsers = new();
            Message = new();
        }

        [BindProperty]
        public DbPrivateMessage Message { get; set; }

        public List<GameForum1User> AllUsers { get; set; }

        public List<DbPrivateMessage> Inbox { get; set; }
        public List<DbPrivateMessage> Outbox { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            AllUsers = await _userManager.Users.ToListAsync();

            Inbox = await _messageManager.GetRecievedMessages(currentUser.Id);
            Outbox = await _messageManager.GetSentMessages(currentUser.Id);

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userId = Request.Form["userId"];
            Message.RecipientId = userId;
            Message.SenderId = currentUser.Id;
            Message.Date = DateTime.Now;

            await _messageManager.CreateMessage(Message);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int deleteId)
        {
            if (deleteId > 0)
            {
                await _messageManager.DeleteMessage(deleteId);
            }
            return Page();
        }
    }
}
