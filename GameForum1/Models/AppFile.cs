namespace GameForum1.Models
{
    public class AppFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string UserId { get; set; }
    }

}
