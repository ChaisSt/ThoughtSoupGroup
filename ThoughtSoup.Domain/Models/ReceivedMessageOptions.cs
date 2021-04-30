using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtSoup.Domain.Models
{
    public class ReceivedMessageOptions : IOptions
    {
        public Position Position { get; set; } = Position.Left;
        public ColorEnum Color { get; set; } = ColorEnum.Purple;
    }
}
