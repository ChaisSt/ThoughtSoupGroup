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
using System.Linq;
using System.Windows.Data;

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

        private string _profilePicturePath;

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
            ChatMessage message = new ChatMessage { Message = MessageInputBox.Text, ConnectionID = _connectionID, ProfilePic = _profilePicturePath };

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
                   (ChatMessage message) => Dispatcher.Invoke(() =>
                   {
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
                    (string profiles) => Dispatcher.Invoke(() =>
                    {

                        var userProfiles = JsonConvert.DeserializeObject<UserProfile[]>(profiles);

                        _userProfiles = new List<UserProfile>(userProfiles);
                        FriendsAndRoomTabs.FriendsList.Items.Clear();

                        _userProfiles.ForEach(profile =>
                        {
                            ListItemContent listItem = new ListItemContent(profile.Username);

                            Binding binding = new Binding();
                            binding.Mode = BindingMode.OneWay;
                            binding.Source = listItem.Parent;
                            listItem.SetBinding(FrameworkElement.WidthProperty, binding);
                            listItem.ListItemTextbox.SetBinding(FrameworkElement.WidthProperty, binding);

                            if(profile.UserConnectionID != _connectionID){
                                FriendsAndRoomTabs.FriendsList.Items.Add(listItem);
							}
                        });
                    })
                );



                try
                {
                    await connection.StartAsync();
                    _connectionID = connection.ConnectionId;
                    await connection.InvokeAsync(
                       "SendConnectionMessage",
                       new ChatMessage { Message = $"{_username} has joined.", ConnectionID = _connectionID, ProfilePic = _profilePicturePath }
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
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Control && e.Key == Key.Enter)
            {
                int caret = MessageInputBox.CaretIndex;
                MessageInputBox.Text = MessageInputBox.Text.Insert(caret, Environment.NewLine);
                MessageInputBox.CaretIndex = caret + Environment.NewLine.Length;
                return;
            }
            if (e.Key == Key.Enter)
            {
                SendButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
