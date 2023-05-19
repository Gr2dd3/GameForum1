using System.Text.Json.Serialization;

namespace GameForum1.Models
{
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
    }
}
