using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.AspNetCore.SignalR.Client;
using ThoughtSoup.Domain.Models;


namespace ThoughtSoup
{
   /// <summary>
   ///    Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private HubConnection connection;

      public MainWindow()
      {
         InitializeComponent();

         InitializeSignalR();

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

      private async void SendButton_Click(object sender, RoutedEventArgs e)
      {
         ChatMessage message = new ChatMessage {Message = MainTextBox.Text};

         await connection.InvokeAsync("SendMessage", message);

         MainTextBox.Text = string.Empty;
      }

      private async void InitializeSignalR()
      {
         try
         {
            connection = new HubConnectionBuilder()
                        .WithUrl("http://localhost:5000/ThoughtSoupChat")
                        .WithAutomaticReconnect()
                        .Build();

            #region snippet_ClosedRestart

            connection.Closed += async (error) =>
            {
               await Task.Delay(new Random().Next(0, 5) * 1000);
               await connection.StartAsync();
            };

            #endregion

            connection.On(
               "ReceiveMessage",
               (ChatMessage message) => Dispatcher.Invoke(() => { ChatWIndow.Text += $"{message.Message}\n"; })
            );

            try
            {
               await connection.StartAsync();
               await connection.InvokeAsync(
                  "SendMessage",
                  new ChatMessage {Message = $"User with connectionID of {connection.ConnectionId} has joined.\n"}
               );
            }
            catch (Exception ex)
            {
               ChatWIndow.Text += ex.Message;
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }
   }
}
