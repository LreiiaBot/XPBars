using System;

namespace XPBars
{
    public class Insertion
    {
        public string Description { get; set; }
        public int Value { get; set; }
        public XPWeight Weight { get; set; }
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
    }
}
