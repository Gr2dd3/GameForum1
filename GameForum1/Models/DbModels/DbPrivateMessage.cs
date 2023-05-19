namespace GameForum1.Models.DbModels;

public class DbPrivateMessage
{
    public int Id { get; set; }
    public string Header { get; set; }
    public string Content { get; set; }
    public string RecipientId { get; set; }
    public string SenderId { get; set; }
}
