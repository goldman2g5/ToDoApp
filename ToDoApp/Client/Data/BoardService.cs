using Newtonsoft.Json;
using ToDoApp.Client.Models;
using System.Text;

namespace ToDoApp.Client.Data
{
    public class BoardService
    {
        private static HttpClient client = new HttpClient();

        public static async Task<List<BoardClient>> GetAll()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7124/api/Board");
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<BoardClient>>(json);
        }

        public static async Task<BoardClient> GetById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7124/api/Board/{id}");
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BoardClient>(json);
        }

        public static async Task<BoardClient> Update(BoardClient boardClient)
        {
            string json = JsonConvert.SerializeObject(boardClient);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"https://localhost:7124/api/Board/{boardClient.Id}", content);
            json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BoardClient>(json);
        }

        public static async Task<BoardClient> Create(BoardClient boardClient)
        {
            string json = JsonConvert.SerializeObject(boardClient);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:7124/api/Board", content);
            json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BoardClient>(json);
        }

        public static async Task<BoardClient> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7124/api/Board/{id}");
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<BoardClient>(json);
        }
    }
}
