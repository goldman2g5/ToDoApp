using Newtonsoft.Json;
using ToDoApp.Client.Models;
using System.Text;

namespace ToDoApp.Client.Data
{
    public class ConnectionService
    {
        private static HttpClient client = new HttpClient();

        public static async Task<List<ConnectionClient>> GetAll()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7124/api/Connection");
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<ConnectionClient>>(json);
        }

        public static async Task<ConnectionClient> GetById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7124/api/Connection/{id}");
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ConnectionClient>(json);
        }

        public static async Task<ConnectionClient> Update(ConnectionClient connectionClient)
        {
            string json = JsonConvert.SerializeObject(connectionClient);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"https://localhost:7124/api/Connection/{connectionClient.Id}", content);
            json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ConnectionClient>(json);
        }

        public static async Task<ConnectionClient> Create(ConnectionClient connectionClient)
        {
            string json = JsonConvert.SerializeObject(connectionClient);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:7124/api/Connection", content);
            json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ConnectionClient>(json);
        }

        public static async Task<ConnectionClient> Delete(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7124/api/Connection/{id}");
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ConnectionClient>(json);
        }

        //public static async Task<ConnectionClient> GetByBoardId(int id)
        //{
        //    HttpResponseMessage response = await client.GetAsync($"https://localhost:7124/api/Connection/GetByBoardId/{id}");
        //    string json = await response.Content.ReadAsStringAsync();

        //    return JsonConvert.DeserializeObject<ConnectionClient>(json);
        //}

        //public static async Task<ConnectionClient> GetByUserId(int id)
        //{
        //    HttpResponseMessage response = await client.GetAsync($"https://localhost:7124/api/Connection/GetByUserId/{id}");
        //    string json = await response.Content.ReadAsStringAsync();

        //    return JsonConvert.DeserializeObject<ConnectionClient>(json);
        //}



    }
}
