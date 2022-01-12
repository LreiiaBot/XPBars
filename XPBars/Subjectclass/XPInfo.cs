using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace XPBars
{
    public class XPInfo : BaseViewModel
    {
        #region Memebers

        protected int currentValue;

        public int CurrentValue // within the current level; DONT USE SETTER
        {
            get { return currentValue; }
            set { currentValue = value; OnPropertyChanged(); CurrentValueDisplay = currentValue; }
        }

        protected double currentValueDisplay;

        public double CurrentValueDisplay
        {
            get { return currentValueDisplay; }
            set { currentValueDisplay = value; OnPropertyChanged(); }
        }

        private int maxValue;

        public int MaxValue // of the current level; DONT USE SETTER
        {
            get { return maxValue; }
            set { maxValue = value; OnPropertyChanged(); }
        }

        private int rest;

        public int Rest
        {
            get { return rest; }
            set { rest = value; OnPropertyChanged(); }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged(); }
        }

        private int level;

        public int Level
        {
            get { return level; }
            set { level = value; MaxValue = DetermineMaxValueOfLevel(level); OnPropertyChanged(); }
        }

        private bool done;

        public bool Done
        {
            get { return done; }
            set { done = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Insertion> protocol = new ObservableCollection<Insertion>();

        public ObservableCollection<Insertion> Protocol
        {
            get { return protocol; }
            set { protocol = value; OnPropertyChanged(); }
        }

        private bool selected;

        public bool Selected
        {
            get { return selected; }
            set { selected = value; OnPropertyChanged(); }
        }


        private Insertion insert = new Insertion(String.Empty, 0, XPWeight.Small);

        public Insertion Insert
        {
            get { return insert; }
            set { insert = value; OnPropertyChanged(); }
        }

        public PersistenceAction PersistenceAction { get; set; } = PersistenceAction.None;

        private bool freezed;

        public bool Freezed
        {
            get { return freezed; }
            set { freezed = value; OnPropertyChanged(); OnPropertyChanged("Active"); OnPropertyChanged("Opacity"); }
        }

        public bool Active
        {
            get { return !freezed; }
            set { freezed = !value; OnPropertyChanged(); OnPropertyChanged("Freezed"); OnPropertyChanged("Opacity"); }
        }

        public double Opacity
        {
            get { return Freezed ? 0.6 : 1; }
            set { OnPropertyChanged(); }
        }

        #endregion


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


        public static List<int> OrbsToValues(Insertion insertion) // TODO dont calculate every orb at a time, each by each -> slow process Thread sleep + show applied value
        {
            List<int> values = new List<int>();
            if (insertion.IgnoreWeight)
            {
                values.Add(insertion.Value);
                return values;
            }
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < insertion.Value; i++)
            {
                switch (insertion.Weight)
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
