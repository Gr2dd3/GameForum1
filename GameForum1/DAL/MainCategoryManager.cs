namespace GameForum1.DAL;

public class MainCategoryManager
{
    // API

    private static Uri _baseAdress = new Uri("https://localhost:44342/");

    public static List<MainCategory> MainCategories { get; set; }

    // GET
    public static async Task<List<MainCategory>> GetMainCategories()
    {
        MainCategories ??= new List<MainCategory>();

        using (var client = new HttpClient())
        {
            client.BaseAddress = _baseAdress;
            HttpResponseMessage response = await client.GetAsync("api/MainCategory");

            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                MainCategories = JsonSerializer.Deserialize<List<MainCategory>>(responseString);
            }

        }
        return MainCategories;
    }

    // UPDATE
    public static async Task UpdateMainCategory(MainCategory existingMainCategory)
    {
        var updateMainCategory = (await GetMainCategories()).Where(p => p.Id == existingMainCategory.Id).FirstOrDefault();

        if (updateMainCategory != null)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                var json = JsonSerializer.Serialize(updateMainCategory);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync("api/MainCategory/" + updateMainCategory.Id, httpContent);
            }
        }
    }

    // CREATE
    public static async Task CreateMainCategory(MainCategory existingMainCategory)
    {
        MainCategories ??= await GetMainCategories();

        using (var client = new HttpClient())
        {
            client.BaseAddress = _baseAdress;
            var json = JsonSerializer.Serialize(existingMainCategory);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/MainCategory/", httpContent);
        }
    }

    // DELETE
    public static async Task DeleteMainCategory(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = _baseAdress;
            HttpResponseMessage response = await client.DeleteAsync("api/MainCategory/" + id);
        }
    }
}
