using System;
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

            BorderColor = message.Options.Color.Value;

            Position = message.Options.Position.Value;

            Radius = message.Options.BorderRadius;

            ImageColumn = message.Options.ImageColumn;

            MessageColumn = message.Options.MessageColumn;

            PicturePath = message.Message.ProfilePic;
            FirstColumnWidth = message.Options.FirstColumnWidth;
            SecondColumnWidth = message.Options.SecondColumnWidth;
            TimeStamp = DateTime.Now.ToString();
            
        }

        public MessageBubble(ReceivedMessageOptions receivedMessageOptions)
		{
            InitializeComponent();
		}

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(MessageText), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof(BorderColor), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty ImageSource = DependencyProperty.Register(nameof(PicturePath), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register(nameof(Position), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register(nameof(Radius), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty ImageColumnProperty = DependencyProperty.Register(nameof(ImageColumn), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty MessageColumnProperty = DependencyProperty.Register(nameof(MessageColumn), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty FirstColumnWidthProperty = DependencyProperty.Register(nameof(FirstColumnWidth), typeof(string), typeof(MessageBubble));
        public static readonly DependencyProperty SecondColumnWidthProperty = DependencyProperty.Register(nameof(SecondColumnWidth), typeof(string), typeof(MessageBubble));

        public static readonly DependencyProperty TimeStampProperty = DependencyProperty.Register(nameof(TimeStamp), typeof(string), typeof(MessageBubble));


        public string PicturePath
        {
            get { return (string)GetValue(ImageSource); }
            set { SetValue(ImageSource, value); }
        }

        public string BorderColor
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
        public string MessageColumn
        {
            get { return (string)GetValue(MessageColumnProperty); }
            set { SetValue(MessageColumnProperty, value); }
        }
        public string ImageColumn
        {
            get { return (string)GetValue(ImageColumnProperty); }
            set { SetValue(ImageColumnProperty, value); }
        }

        public string FirstColumnWidth
        {
            get { return (string)GetValue(FirstColumnWidthProperty); }
            set { SetValue(FirstColumnWidthProperty, value); }
        }
        public string SecondColumnWidth
        {
            get { return (string)GetValue(SecondColumnWidthProperty); }
            set { SetValue(SecondColumnWidthProperty, value); }
        }
        public string TimeStamp
        {
            get { return (string)GetValue(TimeStampProperty); }
            set { SetValue(TimeStampProperty, value); }
        }
    }
}
