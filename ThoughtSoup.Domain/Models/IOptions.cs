using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtSoup.Domain.Models
{
    public interface IOptions
    {
        Position Position { get; set; }
        ColorEnum Color { get; set; }
        string BorderRadius { get; set; }
        string ImageColumn { get; set; }
        string MessageColumn { get; set; }
        string FirstColumnWidth { get; set; }
        string SecondColumnWidth { get; set; }

    }
}
