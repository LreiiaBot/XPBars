using System;
using System.Collections.Generic;

namespace XPBars
{
    class XPInfo
    {
        public int CurrentValue { get; } // within the current level
        public int MaxValue { get; } // of the current level
        public string Description { get; set; }
        public int Level { get; set; }
        public bool Done { get; set; }

        public XPInfo()
        {

        }

        public XPInfo(string description)
        {
            Description = description;
            Level = 0; // start at 0
            MaxValue = DetermineMaxValueOfLevel(Level);
        }

        /// <summary>
        /// constructor necessary for reading of file
        /// </summary>
        public XPInfo(string description, int currentValue, int level, bool done) : this(description)
        {
            CurrentValue = currentValue;
            Level = level;
            Done = done;
            MaxValue = DetermineMaxValueOfLevel(level);
        }

        public void AddValue(Insertion insertion)
        {
            List<int> values = OrbsToValues(insertion.Value, insertion.Weight);
            // calculate sum
        }


        public static int DetermineMaxValueOfLevel(int level)
        {
            int value = 0;
            if (level <= 15)
            {
                value = 2 * level + 7;
            }
            else if (level <= 30)
            {
                value = 5 * level - 38;
            }
            else
            {
                value = 9 * level - 158;
            }
            return value;
        }

        public static int DetermineTotalXPOfLevel(int level)
        {
            double value = 0;
            if (level <= 16)
            {
                value = Math.Pow(level, 2) + 6 * level;
            }
            else if (level <= 31)
            {
                value = 2.5 * Math.Pow(level, 2) - level * 40.5 + 360;
            }
            else
            {
                value = 4.5 * Math.Pow(level, 2) - level * 162.5 + 2220;
            }
            return (int)value;
        }

        public static int DetermineTotalXPOfLevel(int level, int currentValue)
        {
            return DetermineTotalXPOfLevel(level) + currentValue;
        }


        public static List<int> OrbsToValues(int number, XPWeight weight) // TODO dont calculate every orb at a time, each by each -> slow process Thread sleep + show applied value
        {
            List<int> values = new List<int>();
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < number; i++)
            {
                switch (weight)
                {
                    case XPWeight.Small:
                        values.Add(r.Next(1, 3)); // 1 - 2
                        break;
                    case XPWeight.Medium:
                        values.Add(r.Next(3, 7)); // 3 - 6
                        break;
                    case XPWeight.Great:
                        values.Add(r.Next(7, 17)); // 7 - 16
                        break;
                    case XPWeight.Big:
                        values.Add(r.Next(17, 37)); // 17 - 36
                        break;
                    case XPWeight.Large:
                        values.Add(r.Next(37, 73)); // 37 - 72
                        break;
                    default:
                        values.Add(0);
                        break;
                }
            }
            return values;
        }
    }
}
