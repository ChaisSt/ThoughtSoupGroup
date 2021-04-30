using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtSoup.Domain.Models
{
    public class Position
    {
        private Position(string value) { Value = value; }
        public string Value { get; set; }

        public static Position Left { get { return new Position("Left"); } }
        public static Position Center { get { return new Position("Center"); } }
        public static Position Right { get { return new Position("Right"); } }

    }

}
