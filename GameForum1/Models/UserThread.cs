namespace GameForum1.Models;

public class UserThread
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("header")]
    public string Header { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("subcategoryid")]
    public int SubCategoryId { get; set; }

    [JsonPropertyName("userid")]
    public string UserId { get; set; }
}
