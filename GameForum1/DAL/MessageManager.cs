namespace GameForum1.DAL
{
    public class MessageManager
    {
        private readonly GameForum1Context _context;
        public MessageManager(GameForum1Context context)
        {
            _context = context;
        }

        public async Task<List<DbPrivateMessage>> GetRecievedMessages(string userId)
        {
            var allMessages = await GetMessages();
            var userMessages = allMessages.Where(x => x.RecipientId == userId).ToList();

            return userMessages;
        }

        public async Task<List<DbPrivateMessage>> GetSentMessages(string userId)
        {
            var allMessages = await GetMessages();
            var userMessages = allMessages.Where(x => x.SenderId == userId).ToList();

            return userMessages;
        }

        public async Task<DbPrivateMessage> GetOneMessage(int messageId)
        {
            var allMessages = await GetMessages();
            var oneMessage = allMessages.FirstOrDefault(x => x.Id == messageId);

            return oneMessage;
        }

        public async Task<List<DbPrivateMessage>> GetMessages()
        {
            var messages = await _context.PrivateMessages.ToListAsync();
            return messages;
        }

        public async Task CreateMessage(DbPrivateMessage privateMessage)
        {
            await _context.PrivateMessages.AddAsync(privateMessage);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { }
        }

        public async Task DeleteMessage(int id)
        {
            var deleteMessage = await _context.PrivateMessages.FirstOrDefaultAsync(x => x.Id == id);

            if (deleteMessage != null)
            {
                _context.PrivateMessages.Remove(deleteMessage);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex) { }
            }
        }


    }
}
