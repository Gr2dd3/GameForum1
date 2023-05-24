namespace GameForum1.DAL
{
    public class CommentManager
    {
        private static Uri _baseAdress = new Uri("https://gamersparadiseapi.azurewebsites.net/");

        public static List<Comment> Comments { get; set; }
        public static Comment Comment { get; set; }


        // API

        // GET
        public static async Task<List<Comment>> GetComments()
        {
            Comments ??= new List<Comment>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpResponseMessage response = await client.GetAsync("api/Comment");

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    Comments = JsonSerializer.Deserialize<List<Comment>>(responseString);
                }

            }
            return Comments;
        }

        // GET ONE (ID)
        public static async Task<Comment> GetOneComment(int id)
        {
            Comments ??= new List<Comment>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpResponseMessage response = await client.GetAsync("api/Comment/" + id);

                Comment = new();
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    Comment = JsonSerializer.Deserialize<Comment>(responseString);
                }

            }
            return Comment;
        }

        // UPDATE
        public static async Task UpdatComment(Comment existingComment)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                var json = JsonSerializer.Serialize(existingComment);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/Comment/" + existingComment.Id, httpContent);
            }

        }

        // CREATE
        public static async Task CreateComment(Comment existingUserThread)
        {
            Comments ??= await GetComments();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                var json = JsonSerializer.Serialize(existingUserThread);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/Comment/", httpContent);
            }
        }

        // DELETE
        public static async Task DeleteComment(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpResponseMessage response = await client.DeleteAsync("api/Comment/" + id);
            }
        }

    }
}
