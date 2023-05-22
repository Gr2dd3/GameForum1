namespace GameForum1.DAL
{
    public class SubCategoryManager
    {
        /// <summary>
        /// TODO: Mattias söndag - Skapat SubCategoryManager (anrop till API)
        /// Genom att ha kopierat över från MainCategoryManager. 
        /// Också lagt till en DeleteSubCategory
        /// </summary>
        /// <returns></returns>
        
        private static Uri _baseAdress = new Uri("https://localhost:44342/");

        public static List<SubCategory> SubCategories { get; set; }
        public static SubCategory SubCategory { get; set; }


        // API

        // GET
        public static async Task<List<SubCategory>> GetSubCategories()
        {
            SubCategories ??= new List<SubCategory>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpResponseMessage response = await client.GetAsync("api/SubCategory");

                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    SubCategories = JsonSerializer.Deserialize<List<SubCategory>>(responseString);
                }

            }
            return SubCategories;
        }

        // GET ONE (ID)
        public static async Task<SubCategory> GetOneSubCategory(int id)
        {
            SubCategories ??= new List<SubCategory>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpResponseMessage response = await client.GetAsync("api/SubCategory/" + id);

                SubCategory = new SubCategory();
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    SubCategory = JsonSerializer.Deserialize<SubCategory>(responseString);
                }

            }
            return SubCategory;
        }

        // UPDATE
        public static async Task UpdateSubCategory(SubCategory existingSubCategory)
        {
            var updateSubCategory = (await GetSubCategories()).Where(p => p.Id == existingSubCategory.Id).FirstOrDefault();

            if (updateSubCategory is not null)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = _baseAdress;
                    var json = JsonSerializer.Serialize(updateSubCategory);
                    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync("api/SubCategory/" + updateSubCategory.Id, httpContent);
                }
            }
        }

        // CREATE
        public static async Task CreateSubCategory(SubCategory existingSubCategory)
        {
            SubCategories ??= await GetSubCategories();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                var json = JsonSerializer.Serialize(existingSubCategory);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/SubCategory/", httpContent);
            }
        }

        // DELETE
        public static async Task DeleteSubCategory(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAdress;
                HttpResponseMessage response = await client.DeleteAsync("api/SubCategory/" + id);
            }
        }
    }
}
