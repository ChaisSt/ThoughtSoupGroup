using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtSoup.Domain.Models
{
	public class UserProfile
	{
		public UserProfile(string username, string connectionID) {
			Username = username;
			UserConnectionID = connectionID;
		}

		public string Username { get; set; }
		public string UserConnectionID { get; set; }
	}
}
