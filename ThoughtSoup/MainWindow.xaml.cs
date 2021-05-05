using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
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
      private HubConnection _connection;
      private string        _connectionId;

      private readonly string _profilePicturePath;

      private readonly string _username;

      private List<UserProfile> _userProfiles;

      public MainWindow()
      {
         InitializeComponent();

         InitializeSignalR();

         _username = Users.GetUserName(_userProfiles);

         _profilePicturePath = ProfilePictures.GetPicturePath();

         ProfileInformationHeader.Children.Add(new ProfileHeader(_username, _profilePicturePath));
      }

      private async void SendButton_Click(object sender, RoutedEventArgs e)
      {
         ChatMessage message =
            new ChatMessage {Message = MessageInputBox.Text, ConnectionID = _connectionId, ProfilePic = _profilePicturePath};

         await _connection.InvokeAsync("SendMessage", message);

         MessageInputBox.Text = string.Empty;
      }

      private async void InitializeSignalR()
      {
         try
         {
            _connection = new HubConnectionBuilder()
                         .WithUrl("http://localhost:5000/ThoughtSoupChat")
                         .WithAutomaticReconnect()
                         .Build();

            _connection.Closed += async (error) =>
            {
               await Task.Delay(new Random().Next(0, 5) * 1000);
               await _connection.StartAsync();
            };

            _connection.On(
               "ReceiveMessage",
               (ChatMessage message) => Dispatcher.Invoke(
                  () =>
                  {
                     ChatBubble bubble = message.ConnectionID == _connectionId
                        ? new ChatBubble(new SentMessageOptions(), message)
                        : new ChatBubble(new ReceivedMessageOptions(), message);

                     MessageBubble messageBubble = new MessageBubble(bubble);

                     ChatWindow.Children.Add(messageBubble);
                  }
               )
            );

            _connection.On(
               "ReceiveUsers",
               (string profiles) => Dispatcher.Invoke(
                  () =>
                  {
                     UserProfile[] userProfiles = JsonConvert.DeserializeObject<UserProfile[]>(profiles);

                     _userProfiles = new List<UserProfile>(userProfiles ?? Array.Empty<UserProfile>());
                     FriendsAndRoomTabs.FriendsList.Items.Clear();

                     if (_userProfiles?.Count != 0)
                     {
                        _userProfiles.ForEach(
                           profile =>
                           {
                              ListItemContent listItem = new ListItemContent(profile.Username);

                              Binding binding = new Binding {Mode = BindingMode.OneWay, Source = listItem.Parent};
                              listItem.SetBinding(WidthProperty, binding);
                              listItem.ListItemTextbox.SetBinding(WidthProperty, binding);

                              if (profile.UserConnectionID != _connectionId)
                              {
                                 FriendsAndRoomTabs.FriendsList.Items.Add(listItem);
                              }
                           }
                        );
                     }
                  }
               )
            );

            try
            {
               await _connection.StartAsync();
               _connectionId = _connection.ConnectionId;
               await _connection.InvokeAsync(
                  "SendConnectionMessage",
                  new ChatMessage {Message = $"{_username} has joined.", ConnectionID = _connectionId, ProfilePic = _profilePicturePath}
               );
               UserProfile profile = new UserProfile(_username, _connectionId);
               await _connection.InvokeAsync("AddUserConnection", JsonConvert.SerializeObject(profile));
               await _connection.InvokeAsync("GetUsers");
            }
            catch (Exception ex)
            {
               MessageBubble errorMessage =
                  new MessageBubble(new ChatBubble(new SentMessageOptions(), new ChatMessage {Message = ex.Message}));
               ChatWindow.Children.Add(errorMessage);
            }
         }
         catch (Exception ex)
         {
            throw ex;
         }
      }

      private void MessageInputBox_KeyDown(object sender, KeyEventArgs e)
      {
         if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Enter)
         {
            int caret = MessageInputBox.CaretIndex;
            MessageInputBox.Text = MessageInputBox.Text.Insert(caret, Environment.NewLine);
            MessageInputBox.CaretIndex = caret + Environment.NewLine.Length;
            return;
         }
         if (e.Key == Key.Enter)
         {
            SendButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
         }
      }

      private void Window_MouseDown(object sender, MouseButtonEventArgs e)
      {
         DragMove();
      }
   }
}
