using System.Text.Json.Serialization;

namespace GameForum1.Models.DbModels
{
    public class Comment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userThreadId")]
        public int UserThreadId { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }
    }

}
