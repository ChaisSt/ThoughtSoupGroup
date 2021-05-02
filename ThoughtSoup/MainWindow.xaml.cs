using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
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

        private string _username;



      public MainWindow()
      {
         InitializeComponent();

         InitializeSignalR();

         GetUserName();

		}

        private void GetUserName(){
            Array usernames = Enum.GetValues(typeof(UserNames));
            Random random = new Random();
            _username = usernames.GetValue(random.Next(usernames.Length)).ToString();
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
      {
         ChatMessage message = new ChatMessage {Message = MessageInputBox.Text, ConnectionID = _connectionID};

         await connection.InvokeAsync("SendMessage", message);

         MessageInputBox.Text = string.Empty;
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


            
            connection.On(
                "ReceiveUsers",
                (string profiles) => Dispatcher.Invoke(() => {

                    var userProfiles = JsonConvert.DeserializeObject<UserProfile[]>(profiles);

                    FriendsAndRoomTabs.FriendsList.Items.Clear();

                    Array.ForEach(userProfiles, (profile) =>
                    {
                    FriendsAndRoomTabs.FriendsList.Items.Add(profile.Username);
                    });

				})
            );



            try
            {
               await connection.StartAsync();
               _connectionID = connection.ConnectionId;
               await connection.InvokeAsync(
                  "SendMessage",
                  new ChatMessage {Message = $"{_username} has joined.", ConnectionID = _connectionID}
               );
                    UserProfile profile = new UserProfile(_username, _connectionID);
               await connection.InvokeAsync("AddUserConnection", JsonConvert.SerializeObject(profile));
               await connection.InvokeAsync("GetUsers");
            }
            catch (Exception ex)
            {
                    MessageBubble errorMessage = new MessageBubble(new ChatBubble(new SentMessageOptions(), new ChatMessage { Message = ex.Message }));
                    ChatWindow.Children.Add(errorMessage);
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

        private void MessageInputBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Enter)
            {
                int caret = MessageInputBox.CaretIndex;
                MessageInputBox.Text = MessageInputBox.Text.Insert(caret, Environment.NewLine);
                MessageInputBox.CaretIndex = caret + Environment.NewLine.Length;
                return;
            }
            if(e.Key == Key.Enter)
            {
                SendButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
	}
}
