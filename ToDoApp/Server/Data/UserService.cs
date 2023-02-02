using Newtonsoft.Json;
using ToDoApp.Server.Models;
using System.Text;

namespace ToDoApp.Server.Data
{
    public class UserService
    {
            private static HttpClient client = new HttpClient();

            public static async Task<List<User>> GetAll()
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7124/api/User");
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<User>>(json);
            }


        public static async Task<User> Get(int id)
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7124/api/User/" + id);
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(json);
        }
        
        public static async Task Add(User user)
        {
            await client.PostAsJsonAsync("https://localhost:7124/api/User", user);
        }

    }
    }
