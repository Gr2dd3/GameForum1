using System.Text.Json.Serialization;

namespace GameForum1.Models.DbModels
{
    public class SubCategory
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("mainCategoryId")]
        public int MainCategoryId { get; set; }
    }
}
