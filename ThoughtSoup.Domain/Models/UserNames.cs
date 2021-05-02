using System;
using System.Collections.Generic;
using System.Text;

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
		public static string GetUserName()
		{
			Array usernames = Enum.GetValues(typeof(UserNames));
			Random random = new Random();
			return usernames.GetValue(random.Next(usernames.Length)).ToString();
		}
	}

}
