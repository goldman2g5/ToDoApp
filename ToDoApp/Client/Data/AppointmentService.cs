using Newtonsoft.Json;
using ToDoApp.Client.Models;
using System.Text;

namespace ToDoApp.Client.Data
{
    public static class AppointmentService
    {
        private static HttpClient client = new HttpClient();

        public static async Task<List<AppointmentData>> GetAll()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7124/api/AppointmentData");
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<AppointmentData>>(json);
        }

        public static async Task<AppointmentData> GetById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"https://localhost:7124/api/AppointmentData/{id}");
            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AppointmentData>(json);
        }

        public static async Task<AppointmentData> Update(AppointmentData appointmentData)
        {
            string json = JsonConvert.SerializeObject(appointmentData);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"https://localhost:7124/api/AppointmentData/{appointmentData.Id}", content);
            json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AppointmentData>(json);
        }

        public static async Task<AppointmentData> Create(AppointmentData appointmentData)
        {
            string json = JsonConvert.SerializeObject(appointmentData);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:7124/api/AppointmentData", content);
            json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AppointmentData>(json);
        }

    }
}
