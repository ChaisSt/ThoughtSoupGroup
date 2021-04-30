using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ThoughtSoup
{
	/// <summary>
	/// Interaction logic for WindowButtons.xaml
	/// </summary>
	public partial class WindowButtons : UserControl
	{

        //private WindowState WindowState;
        private Window applicationWindow = Window.GetWindow(Application.Current.MainWindow);

        public WindowButtons()
		{
			InitializeComponent();

            WindowImg.Source = new BitmapImage(new Uri(@"/Resources/MinWindow.png", UriKind.Relative));
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
           applicationWindow.WindowState = WindowState.Minimized;
        }

		private void CloseButton_Click(object sender, RoutedEventArgs e) => applicationWindow.Close();

		private void WindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (applicationWindow.WindowState != WindowState.Maximized)
            {
                applicationWindow.WindowState = WindowState.Maximized;
                WindowImg.Source = new BitmapImage(new Uri(@"/Resources/MinWindow.png", UriKind.Relative));
            }
            else
            {
                applicationWindow.WindowState = WindowState.Normal;
                WindowImg.Source = new BitmapImage(new Uri(@"/Resources/MaxWindow.png", UriKind.Relative));
            }
        }
    }
}
