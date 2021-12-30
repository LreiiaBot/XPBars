using System;
using System.Collections.ObjectModel;

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

        #endregion

        public MainViewModel()
        {
            LXPBars.Add(XPBar.Read());
        }

        public void AddOrbs()
        {
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

        public void Save()
        {
            XPBar.Save(LXPBars);
        }
    }
}
