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

        private string _connectionID; 

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
         ChatMessage message = new ChatMessage {Message = MainTextBox.Text, ConnectionID = _connectionID};

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
               //(ChatMessage message) => Dispatcher.Invoke(() => { ChatWIndow.Text += $"{message.Message}\n"; })
               (ChatMessage message) => Dispatcher.Invoke(() => {
                   ChatBubble bubble;

                   if (message.ConnectionID == _connectionID)
                   {
                       bubble = new ChatBubble(new SentMessageOptions(), message);
                   }
                   else
                   {
                       bubble = new ChatBubble(new ReceivedMessageOptions(), message);
                   }

                   MessageBubble messageBubble = new MessageBubble(bubble);
                   
                   ChatWindow.Children.Add(messageBubble);
               })
            );

            try
            {
               await connection.StartAsync();
               _connectionID = connection.ConnectionId;
               await connection.InvokeAsync(
                  "SendMessage",
                  new ChatMessage {Message = $"User with connectionID of {_connectionID} has joined.", ConnectionID = _connectionID}
               );
            }
            catch (Exception ex)
            {
                    MessageBubble errorMessage = new MessageBubble(new ChatBubble(null, new ChatMessage { Message = ex.Message }));
                    ChatWindow.Children.Add(errorMessage);
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

       
   }
}
