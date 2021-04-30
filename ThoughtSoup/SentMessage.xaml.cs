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
using ThoughtSoup.Domain.Models;

namespace ThoughtSoup
{
    /// <summary>
    /// Interaction logic for SentMessage.xaml
    /// </summary>
    public partial class SentMessage : UserControl
    {
        public SentMessage(ChatMessage message)
        {
            InitializeComponent();

            MessageText = message.Message;
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(MessageText), typeof(string), typeof(SentMessage));

        public string MessageText
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
