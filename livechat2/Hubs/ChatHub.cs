using Microsoft.AspNetCore.SignalR;

namespace livechat2.Hubs
{
    public class ChatHub:Hub
    {
       public async Task SendMessage(string message)
        {
           // await Clients.All.SendAsync("ReceiveMsg",message);
        }
    }
}
