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
	/// Interaction logic for ListItemContent.xaml
	/// </summary>
	public partial class ListItemContent : UserControl
	{
		public ListItemContent(string listItemText)
		{
			InitializeComponent();

			ListItemText = listItemText;
		}

		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(ListItemText), typeof(string), typeof(ListItemContent));

		public string ListItemText
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
	}
}
