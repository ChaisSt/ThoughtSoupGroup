using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ThoughtSoup.Domain.Models;


namespace ThoughtSoup.SignalR.Hubs
{
   public class ThoughtSoupHub : Hub
   {
      public async Task SendMessage(ChatMessage message)
      {
         await Clients.All.SendAsync("ReceiveMessage", message);
      }
   }
}
