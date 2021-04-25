using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ThoughtSoup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //TcpClient client = new TcpClient();
        //NetworkStream stream = default(NetworkStream);
        //string readData = null;

        public MainWindow()
        {
            InitializeComponent();
            WindowImg.Source = new BitmapImage(new Uri(@"/Resources/MinWindow.png", UriKind.Relative));
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
                WindowImg.Source = new BitmapImage(new Uri(@"/Resources/MinWindow.png", UriKind.Relative));
            }
            else
            {
                WindowState = WindowState.Normal;
                WindowImg.Source = new BitmapImage(new Uri(@"/Resources/MaxWindow.png", UriKind.Relative));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            //client.Connect("127.0.0.1", 8888);
            //stream = client.GetStream();
            //SetName();
            //byte[] outStream = System.Text.Encoding.ASCII.GetBytes(MainTextBox.Text + "$");
            //stream.Write(outStream, 0, outStream.Length);
            //stream.Flush();

            //Thread ctThread = new Thread(GetMessage);
            //ctThread.Start();
        }

        //private void SetName()
        //{
        //    byte[] outStream = System.Text.Encoding.ASCII.GetBytes("SomeUsername$");
        //    stream.Write(outStream, 0, outStream.Length);
        //    stream.Flush();
        //}

        //private void GetMessage()
        //{
        //    while (true)
        //    {
        //        stream = client.GetStream();
        //        int buffSize = 0;
        //        byte[] inStream = new byte[10024];
        //        buffSize = client.ReceiveBufferSize;
        //        stream.Read(inStream, 0, inStream.Length);
        //        string returndData = System.Text.Encoding.ASCII.GetString(inStream);
        //        readData = "" + returndData;
        //        msg();
        //    }
        //}

        //private void msg()
        //{
        //    if (this.Dispatcher.CheckAccess())
        //    {
        //        this.Dispatcher.Invoke(new MethodInvoker(msg));
        //    }
        //    else
        //    {
        //        MainOutput.Text = MainOutput.Text + Environment.NewLine + " >> " + readData;
        //    }
        //}

        //public const int WM_NCLBUTTONDOWN = 0xA1;
        //public const int HTCAPTION = 0x2;
        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //public static extern bool ReleaseCapture();
        //[System.Runtime.InteropServices.DllImport("user32.dll")]
        //public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        //private void pnlTitleBar1_OnMouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        ReleaseCapture();
        //        SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        //    }
        //}
    }
}
