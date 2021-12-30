using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            insertion.Date = DateTimeOffset.UtcNow;
            Protocol.Add(insertion);
            List<int> values = OrbsToValues(insertion);
            int sum = 0;
            int newValue = 0;
            int leftValue = 0;

            foreach (var value in values)
            {
                newValue = CurrentValue + value;
                sum += value;
                leftValue = 0;
                if (newValue >= MaxValue)
                {
                    while (newValue >= MaxValue)
                    {
                        leftValue = newValue - MaxValue;

                        //shortly only for design
                        CurrentValue = MaxValue;
                        await Task.Run(() => Thread.Sleep(400));

                        Level++;
                        CurrentValue = leftValue;
                    }
                }
                else
                {
                    CurrentValue = newValue;
                }
                await Task.Run(() => Thread.Sleep(600));
            }
            if (Parentbar != null)
            {
                Parentbar.AddValue(new Insertion(insertion.Description, sum / Parentbar.Subbars.Count, true));
            }
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
