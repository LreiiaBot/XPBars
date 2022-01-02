using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace XPBars
{
    public class MainViewModel : BaseViewModel
    {
        #region Members

        private ObservableCollection<XPBar> lXPBars = new ObservableCollection<XPBar>();

        public ObservableCollection<XPBar> LXPBars
        {
            get { return lXPBars; }
            set { lXPBars = value; OnPropertyChanged(); }
        }

        private XPBar selectedXPBar;

        public XPBar SelectedXPBar
        {
            get { return selectedXPBar; }
            set { selectedXPBar = value; OnPropertyChanged(); }
        }

        private string barname;

        public string Barname
        {
            get { return barname; }
            set { barname = value; OnPropertyChanged(); }
        }

        #endregion

        public MainViewModel()
        {
            LXPBars.Add(XPBar.Read());
        }

        public void AddOrbs()
        {
            //SelectedXPBar.Level = SelectedXPBar.Level + SelectedXPBar.Insert.Value;
            //SelectedXPBar.CurrentValue = Convert.ToInt32(SelectedXPBar.Insert.Description);
            //return;
            var insertHlp = SelectedXPBar.Insert;
            if (insertHlp.Value <= 0)
            {
                return;
            }
            else if (String.IsNullOrWhiteSpace(insertHlp.Description))
            {
                return;
            }
            SelectedXPBar.Insert = new Insertion(String.Empty, 0, XPWeight.Small);
            SelectedXPBar.AddValue(insertHlp);
        }

        public void AddBar()
        {
            if (SelectedXPBar == null)
            {
                return;
            }
            if (String.IsNullOrWhiteSpace(Barname))
            {
                return;
            }
            foreach (var subbar in SelectedXPBar.Subbars)
            {
                if (subbar.Description.Trim() == Barname.Trim())
                {
                    return;
                }
            }
            XPBar xpbar = new XPBar(Barname.Trim());
            xpbar.Parentbar = SelectedXPBar;
            SelectedXPBar.Subbars.Add(xpbar);
            Barname = String.Empty;
        }

        public void Save()
        {
            XPBar.Save(LXPBars);
        }

        public void OrderAllAZ()
        {
            OrderSubbarsAZ(LXPBars[0]);
        }
        public void OrderAllZA()
        {
            OrderSubbarsZA(LXPBars[0]);
        }
        public void OrderAll1N()
        {
            OrderSubbars1N(LXPBars[0]);
        }
        public void OrderAllN1()
        {
            OrderSubbarsN1(LXPBars[0]);
        }

        public void OrderSubbarsAZ(XPBar xpbar)
        {
            xpbar.Subbars = xpbar.Subbars.OrderBy(bar => bar.Description).ThenBy(bar => bar.Level).ThenBy(bar => bar.CurrentValue).Convert();
            foreach (var subbar in xpbar.Subbars)
            {
                OrderSubbarsAZ(subbar);
            }
        }
        public void OrderSubbarsZA(XPBar xpbar)
        {
            xpbar.Subbars = xpbar.Subbars.OrderByDescending(bar => bar.Description).ThenBy(bar => bar.Level).ThenBy(bar => bar.CurrentValue).Convert();
            foreach (var subbar in xpbar.Subbars)
            {
                OrderSubbarsZA(subbar);
            }
        }
        public void OrderSubbars1N(XPBar xpbar)
        {
            xpbar.Subbars = xpbar.Subbars.OrderBy(bar => bar.Level).ThenBy(bar => bar.CurrentValue).ThenBy(bar => bar.Description).Convert();
            foreach (var subbar in xpbar.Subbars)
            {
                OrderSubbars1N(subbar);
            }
        }
        public void OrderSubbarsN1(XPBar xpbar)
        {
            xpbar.Subbars = xpbar.Subbars.OrderByDescending(bar => bar.Level).ThenByDescending(bar => bar.CurrentValue).ThenBy(bar => bar.Description).Convert();
            foreach (var subbar in xpbar.Subbars)
            {
                OrderSubbarsN1(subbar);
            }
        }
    }
}
