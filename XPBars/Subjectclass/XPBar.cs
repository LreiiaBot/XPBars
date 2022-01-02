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
        public XPBar(string description, int currentValue, int level, bool done) : base(description, currentValue, level, done)
        {

        }
        public async void AddValue(Insertion insertion)
        {
            insertion.Date = DateTime.Now;
            Protocol.Add(insertion);
            List<int> values = OrbsToValues(insertion);
            int sum = 0;
            int newValue = 0;

            int counter = 0;
            foreach (var value in values)
            {
                counter++;
                // For Desgin set value
                sum += value;
                LastValueAdded = $"+ {sum} (+{value})";
                if (counter == values.Count)
                {
                    ResetLastValueAdded();
                }

                newValue = CurrentValue + value;
                if (newValue >= MaxValue)
                {
                    while (newValue >= MaxValue)
                    {
                        newValue = newValue - MaxValue;

                        //shortly only for design
                        await Task.Run(() => SlowlyIncreaseTo(MaxValue));
                        await Task.Run(() => Thread.Sleep(800));

                        Level++;
                        CurrentValue = 0;
                        await Task.Run(() => SlowlyIncreaseTo(newValue));
                    }
                }
                else
                {
                    await Task.Run(() => SlowlyIncreaseTo(newValue));
                }
                await Task.Run(() => Thread.Sleep(1200));
                //StringBuilder sb = new StringBuilder();
                //for (int i = 0; i < LastValueAdded.Length + 5; i++)
                //{
                //    // unbreakable Space
                //    sb.Append("\u00A0");
                //}
                //LastValueAdded = sb.ToString();
                //await Task.Run(() => Thread.Sleep(400));
            }
            insertion.FlatXP = sum;
            if (Parentbar != null)
            {
                int valueToAdd = (int)Math.Round((double)sum / (double)Parentbar.Subbars.Count);
                Parentbar.AddValue(new Insertion(insertion.Description.Trim(), valueToAdd, true));
            }
        }

        public async Task SlowlyIncreaseTo(int value)
        {
            int steps = 500;
            double increase = ((double)value - (double)CurrentValue) / (double)steps;
            for (int i = 0; i < steps - 1; i++)
            {
                CurrentValueDisplay += increase;
                await Task.Run(() => Thread.Sleep(1));
            }
            //CurrentValueDisplay = value;
            CurrentValue = value;
        }

        public async void ResetLastValueAdded()
        {
            await Task.Run(() => Thread.Sleep(2500));
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
