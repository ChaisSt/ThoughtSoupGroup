using System;
using System.Collections.Generic;
using System.Text;

namespace ThoughtSoup.Domain.Models
{
    public class ColorEnum
    {
        private ColorEnum(string value) { Value = value; }
        public string Value { get; set; }

        public static ColorEnum LightBlue { get { return new ColorEnum("#3483eb"); } }
        public static ColorEnum Purple { get { return new ColorEnum("Purple"); } }
    }
}
