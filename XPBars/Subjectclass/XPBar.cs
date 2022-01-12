using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace XPBars
{
    public class XPBar : XPInfo
    {
        #region Members

        private XPBar parentbar;

        public XPBar Parentbar
        {
            get { return parentbar; }
            set { parentbar = value; OnPropertyChanged(); }
        }

        private ObservableCollection<XPBar> subbars = new ObservableCollection<XPBar>();

        public ObservableCollection<XPBar> Subbars
        {
            get { return subbars; }
            set { subbars = value; OnPropertyChanged(); }
        }

        private string lastValueAdded;

        public string LastValueAdded
        {
            get { return lastValueAdded; }
            set { lastValueAdded = value; OnPropertyChanged(); }
        }


        #endregion

        public XPBar()
        {

        }

        public XPBar(string description) : base(description)
        {

        }

        /// <summary>
        /// constructor necessary for reading of file
        /// </summary>
        public XPBar(string description, int currentValue, int level, bool done, bool frozen, int rest) : base(description, currentValue, level, done)
        {
            Freezed = frozen;
            Rest = rest;
        }
        public async void AddValue(Insertion insertion)
        {
            insertion.Date = DateTime.Now;
            Protocol.Add(insertion);
            insertion.PersistenceAction = PersistenceAction.Insert;
            List<int> values = OrbsToValues(insertion);
            int sum = 0;
            int newValue = 0;

            foreach (var value in values)
            {
                // For Desgin set value
                sum += value;
                LastValueAdded = $"+ {sum} (+{value})";

                newValue = CurrentValue + value;
                if (newValue >= MaxValue)
                {
                    while (newValue >= MaxValue)
                    {
                        newValue = newValue - MaxValue;

                        //shortly only for design
                        await Task.Run(() => SlowlyIncreaseTo(MaxValue));
                        await Task.Run(() => Thread.Sleep(50));

                        Level++;
                        CurrentValue = 0;
                    }
                    await Task.Run(() => SlowlyIncreaseTo(newValue));
                }
                else
                {
                    await Task.Run(() => SlowlyIncreaseTo(newValue));
                }
                await Task.Run(() => Thread.Sleep(500));
            }
            insertion.FlatXP = sum;
            if (Parentbar != null)
            {
                if (Parentbar.PersistenceAction != PersistenceAction.Insert)
                {
                    Parentbar.PersistenceAction = PersistenceAction.Update;
                }
                Parentbar.Rest = Parentbar.Rest + sum;
                int valueToAdd = Parentbar.Rest / Parentbar.Subbars.Count;
                int rest = Parentbar.Rest % Parentbar.Subbars.Count;
                Parentbar.Rest = rest;
                Parentbar.AddValue(new Insertion(insertion.Description.Trim(), valueToAdd, true));
            }
            ResetLastValueAdded();
        }

        public async Task SlowlyIncreaseTo(int value)
        {
            int steps = 400;
            steps = 200 + (int)(((double)value - (double)CurrentValue) / MaxValue * 800);
            double increase = ((double)value - (double)CurrentValue) / (double)steps;
            for (int i = 0; i < steps - 1; i++)
            {
                CurrentValueDisplay += increase;

                // Update shown number
                // dont use setter -> would also update currentvaluedisplay -> performance and visual
                currentValue = (int)CurrentValueDisplay;
                OnPropertyChanged("CurrentValue");

                await Task.Run(() => Thread.Sleep(1));
            }
            //CurrentValueDisplay = value;
            CurrentValue = value;
        }

        public async void ResetLastValueAdded()
        {
            await Task.Run(() => Thread.Sleep(6500));
            LastValueAdded = String.Empty;
        }

        public static void Save(IEnumerable<XPBar> xpbars)
        {
            XPBarCsv.Save(xpbars);
        }

        public static XPBar Read()
        {
            return XPBarCsv.Read();
        }
    }
}
