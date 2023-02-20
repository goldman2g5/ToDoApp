using Microsoft.AspNetCore.SignalR;

namespace ToDoApp.Server.Hubs
{
    public class ChatHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message, string chatid)
        {
            await Clients.All.SendAsync("ReciveMessage", user, message, chatid);
        }

        public async Task SendSyncRequest(string json)
        {
            Console.WriteLine($"Беброчка вкусная {json}");
            await Clients.All.SendAsync("ReciveSyncRequest", json);
        }


    }
}
