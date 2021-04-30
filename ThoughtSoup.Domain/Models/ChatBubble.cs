using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace ThoughtSoup.Domain.Models
{
    public class ChatBubble 
    {
        private readonly IOptions _options;
        public ChatBubble(IOptions options, ChatMessage message)
        {
            _options = options;
            Message = message;
        }

        public IOptions Options => _options;

        public ChatMessage Message { get; }
    }
}
