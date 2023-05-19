using System.Text.Json.Serialization;

namespace GameForum1.Models.DbModels
{
    public class MainCategory
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public int Name { get; set; }
    }
}
