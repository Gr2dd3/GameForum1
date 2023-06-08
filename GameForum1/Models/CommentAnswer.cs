namespace GameForum1.Models
{
    public class CommentAnswer
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("score")]
        public int Score { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("createdDate")]
        public DateTime Date { get; set; }

        [JsonPropertyName("commentid")]
        public int CommentId { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("reported")]
        public bool Reported { get; set; }
    }
}
