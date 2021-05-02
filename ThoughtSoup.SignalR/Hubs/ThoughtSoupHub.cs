using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ThoughtSoup.Domain.Models;
using ThoughtSoup.SignalR.Repository;
using Newtonsoft.Json;

namespace ThoughtSoup.SignalR.Hubs
{
	public class ThoughtSoupHub : Hub
	{
		private readonly OnlineUsers _onlineUsers;

		public ThoughtSoupHub(OnlineUsers onlineUsers)
		{
			this._onlineUsers = onlineUsers ?? throw new ArgumentNullException(nameof(onlineUsers));
		}

		public async Task SendMessage(ChatMessage message)
		{
			await Clients.All.SendAsync("ReceiveMessage", message);
		}

		[HubMethodName("GetUsers")]
		public async Task SendUsers(){
			var profiles = JsonConvert.SerializeObject(_onlineUsers.GetUserProfiles());
			await Clients.All.SendAsync("ReceiveUsers", profiles).ConfigureAwait(false);
		}

		public void AddUserConnection(string user){
			_onlineUsers.AddUser(JsonConvert.DeserializeObject<UserProfile>(user));
		}

		public async Task SendConnectionMessage(ChatMessage message)
		{
			await Clients.Others.SendAsync("ReceiveMessage", message);
		}
	}
}
