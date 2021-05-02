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

        public string ImageColumn { get; set; } = "1";

        public string MessageColumn { get; set; } = "0";

        public string FirstColumnWidth { get; set; } = "*";
        public string SecondColumnWidth { get; set; } = "70";
    }
}
