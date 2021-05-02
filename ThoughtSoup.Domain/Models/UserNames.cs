using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ThoughtSoup.Domain.Models
{
	public enum UserNames
	{ 
		LuckMadHatter,
		XamCypher,
		CagedNebula,
		ChrispierRice,
		OompaLoopa,
		DrSquatch
	}


	public class Users
	{
		public static string GetUserName(List<UserProfile> userProfiles)
		{
			Array usernames = Enum.GetValues(typeof(UserNames));
			Random random = new Random();
			if (userProfiles is null)
			{
				return usernames.GetValue(random.Next(usernames.Length)).ToString();
			}
			else
			{
				UserProfile user = userProfiles.FirstOrDefault(u => u.Username != (string)usernames.GetValue(random.Next(usernames.Length)));
				return user.Username;

			}
		}
	}

}
