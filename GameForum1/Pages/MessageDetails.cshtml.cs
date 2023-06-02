using GameForum1.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameForum1.Pages
{
    public class MessageDetailsModel : PageModel
    {
        private readonly MessageManager _messageManager;
        private readonly UserManager<GameForum1User> _userManager;

        public MessageDetailsModel(MessageManager messageManager, UserManager<GameForum1User> userManager)
        {
            _messageManager = messageManager;
            _userManager = userManager;
        }
        public DbPrivateMessage OpenedMessage { get; set; }
        public GameForum1User Sender { get; set; }
        public GameForum1User Recipient { get; set; }

        public string ImageSrc { get; set; }
        public async Task<IActionResult> OnGetAsync(int messageId)
        {
            if (messageId is not 0)
            {
                await GetMessage(messageId);
            }



            return Page();
        }

        public async Task GetMessage(int id)
        {
            OpenedMessage = new();
            OpenedMessage = await _messageManager.GetOneMessage(id);
			Recipient = await _userManager.FindByIdAsync(OpenedMessage.RecipientId);
			Sender = await _userManager.FindByIdAsync(OpenedMessage.SenderId);
        }
    }
}
