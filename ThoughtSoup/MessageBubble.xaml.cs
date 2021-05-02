using System.Windows;
using System.Windows.Controls;
using ThoughtSoup.Domain.Models;

namespace ThoughtSoup
{
    /// <summary>
    /// Interaction logic for RecievedMessage.xaml
    /// </summary>
    public partial class MessageBubble : UserControl
    {
        public MessageBubble(ChatBubble message)
        {
            InitializeComponent();

            MessageText = message.Message.Message;

            BackgroundColor = message.Options.Color.Value;

            Position = message.Options.Position.Value;

            Radius = message.Options.BorderRadius;


        }

        public MessageBubble(ReceivedMessageOptions receivedMessageOptions)
		{
            InitializeComponent();
		}

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(MessageText), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(BackgroundColor), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register(nameof(Position), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(string), typeof(MessageBubble));

        public string BackgroundColor
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public string MessageText
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public string Position
        {
            get { return (string)GetValue(AlignmentProperty); }
            set { SetValue(AlignmentProperty, value); }
        }
        public string Radius
        {
            get { return (string)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); }
        }
    }
}
