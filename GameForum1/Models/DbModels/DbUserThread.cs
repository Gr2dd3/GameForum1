using System.Text.Json.Serialization;

namespace GameForum1.Models.DbModels
{
    public class UserThread
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("subCategoryId")]
        public int SubCategoryId { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }
    }


}
