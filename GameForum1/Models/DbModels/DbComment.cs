namespace GameForum1.Models.DbModels;

public class DbComment
{
    public int Id { get; set; }

    public int UserThreadId { get; set; }

    public string UserId { get; set; }
}
