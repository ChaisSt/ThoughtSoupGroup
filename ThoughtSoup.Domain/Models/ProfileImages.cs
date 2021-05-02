using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtSoup.Domain.Models
{
	public enum ProfileImages
	{
		Cool,
		Happy, 
		Love,
		Mad,
		Sad,
		Shock,
		Sick,
		Smile,
		Wink
	}

	public class ProfilePictures
	{
		public static string GetPicturePath()
		{
			Array images = Enum.GetValues(typeof(ProfileImages));
			Random random = new Random();
			var imageName =  images.GetValue(random.Next(images.Length)).ToString();

			return $"Resources/ProfilePictures/{imageName}.jpg";
		}
	}
}
