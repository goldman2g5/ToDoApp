using Microsoft.AspNetCore.SignalR;

namespace ToDoApp.Server.Hubs
{
    public class AppointmentHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string json)
        {
            await Clients.All.SendAsync("ReciveSyncRequest", json);
        }
    }
}
