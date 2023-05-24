namespace GameForum1.Models;

public class SubCategory
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("maincategoryid")]
    public int MainCategoryId { get; set; }
}
