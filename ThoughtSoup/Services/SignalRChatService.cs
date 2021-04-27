using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using ThoughtSoup.Domain.Models;


namespace ThoughtSoup.Services
{
   public class SignalRChatService
   {
      private readonly HubConnection _connection;

      public SignalRChatService(HubConnection connection)
      {
         _connection = connection;

         _connection.On<ChatMessage>("ReceiveMessage", (message) => ChatMessageReceived?.Invoke(message));
      }

      public event Action<ChatMessage> ChatMessageReceived;

      public async Task Connect()
      {
         await _connection.StartAsync();
      }

      public async Task SendMessage(ChatMessage message)
      {
         await _connection.SendAsync("SendMessage", message);
      }
   }
}
