using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtSoup.Domain.Models
{
    public class SentMessageOptions : IOptions
    {
        public Position Position { get; set; } = Position.Right;
        public ColorEnum Color { get; set; } = ColorEnum.LightBlue;
        public string BorderRadius { get; set; } = "10, 10, 0, 10";
    }
}
