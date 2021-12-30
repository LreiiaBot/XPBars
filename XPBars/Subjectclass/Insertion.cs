using System;

namespace XPBars
{
    public class Insertion
    {
        public string Description { get; set; }
        public int Value { get; set; }
        public XPWeight Weight { get; set; }
        public DateTimeOffset Date { get; set; } = DateTimeOffset.UtcNow;
        public bool IgnoreWeight { get; set; }

        public Insertion(string description, int value, XPWeight weight)
        {
            Description = description;
            Value = value;
            Weight = weight;
        }
        public Insertion(string description, int value, XPWeight weight, DateTimeOffset date) : this(description, value, weight)
        {
            Date = date;
        }
        public Insertion(string description, int value, bool ignoreWeight)
        {
            Description = description;
            Value = value;
            IgnoreWeight = ignoreWeight;
        }
    }
}
