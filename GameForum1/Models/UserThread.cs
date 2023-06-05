using System.ComponentModel.DataAnnotations;

namespace GameForum1.Models;

public class UserThread
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("score")]
    public int Score { get; set; }

    [JsonPropertyName("header")]
    [MaxLength(80)]
    public string Header { get; set; }

    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("subCategoryId")]
    public int SubCategoryId { get; set; }

    [JsonPropertyName("userId")]
    public string UserId { get; set; }

    [JsonPropertyName("reported")]
    public bool Reported { get; set; }

    [JsonPropertyName("image")]
    public string? Image { get; set; }

}
