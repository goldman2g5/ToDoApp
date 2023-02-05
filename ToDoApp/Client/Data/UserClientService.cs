using Newtonsoft.Json;
using ToDoApp.Client.Models;

namespace ToDoApp.Client.Data
{
    public class UserClientService
    {
        private static HttpClient client = new HttpClient();

        public static async Task<List<UserClient>> GetAll()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7124/api/User");
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<UserClient>>(json);
        }

    }
}
