namespace GameForum1.Models;

public class Comment
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("userthreadid")]
    public int UserThreadId { get; set; }

    [JsonPropertyName("userid")]
    public string UserId { get; set; }
}
