using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThoughtSoup.Domain.Models;

namespace ThoughtSoup.SignalR.Repository
{
	public class OnlineUsers
	{
		private static List<UserProfile> userProfiles = new List<UserProfile>();

		public void AddUser(UserProfile profile) {
			userProfiles.Add(profile);
		}

		public void RemoveUser(UserProfile profile) {
			userProfiles.Remove(profile);
		}

		public List<UserProfile> GetUserProfiles() {
			return userProfiles;
		}
	}


}
