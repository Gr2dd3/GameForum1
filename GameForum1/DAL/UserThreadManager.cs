namespace GameForum1.DAL
{
    public class UserThreadManager
    {
        private static Uri _baseAdress = new Uri("https://gamersparadiseapi.azurewebsites.net/");

        public static List<UserThread> UserThreads { get; set; }
        public static UserThread UserThread { get; set; }


        // API

        // GET
        public static async Task<List<UserThread>> GetUserThreads()
        {
            UserThreads ??= new List<UserThread>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpResponseMessage response = await client.GetAsync("api/UserThread");

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    UserThreads = JsonSerializer.Deserialize<List<UserThread>>(responseString);
                }

            }
            return UserThreads;
        }

        // GET ONE (ID)
        public static async Task<UserThread> GetOneUserThread(int id)
        {
            UserThreads ??= new List<UserThread>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpResponseMessage response = await client.GetAsync("api/UserThread/" + id);

                UserThread = new UserThread();
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    UserThread = JsonSerializer.Deserialize<UserThread>(responseString);
                }

            }
            return UserThread;
        }

        // UPDATE
        public static async Task UpdateUserThread(UserThread existingUserThread)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                var json = JsonSerializer.Serialize(existingUserThread);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/UserThread/" + existingUserThread.Id, httpContent);
            }
            
        }

        // CREATE
        public static async Task CreateUserThread(UserThread existingUserThread)
        {
            UserThreads ??= await GetUserThreads();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                var json = JsonSerializer.Serialize(existingUserThread);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/UserThread/", httpContent);
            }
        }

        // DELETE
        public static async Task DeleteUserThread(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpResponseMessage response = await client.DeleteAsync("api/UserThread/" + id);
            }
        }
    }

}