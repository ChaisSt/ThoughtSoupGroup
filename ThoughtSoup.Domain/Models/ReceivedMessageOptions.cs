using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtSoup.Domain.Models
{
    public class ReceivedMessageOptions : IOptions
    {
        public Position Position { get; set; } = Position.Left;
        public ColorEnum Color { get; set; } = ColorEnum.Purple;
        public string BorderRadius { get; set; } = "10, 10, 10, 0";

        public string ImageColumn { get; set; } = "0";

        public string MessageColumn { get; set; } = "1";

        public string FirstColumnWidth { get; set; } = "70";
        public string SecondColumnWidth { get; set; } = "*";
    }
}
