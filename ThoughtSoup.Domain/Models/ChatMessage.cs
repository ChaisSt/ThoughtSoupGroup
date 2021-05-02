﻿namespace ThoughtSoup.Domain.Models
{
   public class ChatMessage
   {
      public string Message { get; set; }
        public string ProfilePic { get; set; } = "Resources/neonCool1.jpg";
      public string UserName { get; set; }
      public string ConnectionID { get; set; }
   }
}
