using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ThoughtSoup
{
	/// <summary>
	/// Interaction logic for ProfileHeader.xaml
	/// </summary>
	public partial class ProfileHeader : UserControl
	{
		public ProfileHeader(string userName, string imagePath)
		{
			InitializeComponent();

			ProfilePicturePath = imagePath;

			UserName = userName;

		}

		public static readonly DependencyProperty UserNameProperty = DependencyProperty.Register(nameof(UserName), typeof(string), typeof(MessageBubble));

		public static readonly DependencyProperty ProfilePictureProperty = DependencyProperty.Register(nameof(ProfilePicturePath), typeof(string), typeof(MessageBubble));

		public string ProfilePicturePath
		{
			get { return (string)GetValue(ProfilePictureProperty); }
			set { SetValue(ProfilePictureProperty, value); }
		}

		public string UserName
		{
			get { return (string)GetValue(UserNameProperty); }
			set { SetValue(UserNameProperty, value); }
		}
	}
}
