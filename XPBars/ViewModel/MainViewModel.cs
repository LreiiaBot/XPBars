using System.Collections.ObjectModel;

namespace XPBars
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<XPBar> LXPBars { get; set; } = new ObservableCollection<XPBar>();
        public MainViewModel()
        {
            var masterBar = new XPBar("Master", 14, 10, true);
            LXPBars.Add(masterBar);
            masterBar.Subbars.Add(new XPBar("Test"));
            //masterBar.Subbars.Add(new XPBar("Demo", 40, 10, false));
            //masterBar.Subbars.Add(new XPBar("Demo", 40, 20, false));
            //masterBar.Subbars.Add(new XPBar("Demo", 40, 30, false));
            //masterBar.Subbars.Add(new XPBar("Demo", 40, 40, false));
            //masterBar.Subbars.Add(new XPBar("Demo", 40, 49, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 40, 50, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 40, 51, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 40, 60, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 40, 90, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 40, 130, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 40, 170, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 40, 180, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 140, 198, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 140, 199, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 140, 200, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 140, 300, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 140, 500, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 140, 800, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 140, 900, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 140, 999, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 140, 1000, false));
            //masterBar.Subbars.Add(new XPBar("Demo1", 140, 1001, false));
            masterBar.Subbars.Add(new XPBar("Super Long Demo Name"));
            var school = new XPBar("School");
            school.Level = 3;
            school.Subbars.Add(new XPBar("Math", 9, 31, true));
            var english = new XPBar("English", 9, 5, false);
            school.Subbars.Add(english);
            english.Done = true;
            english.Subbars.Add(new XPBar("Vocabulary"));
            school.Subbars.Add(new XPBar("German", 1, 1, false));
            masterBar.Subbars.Add(school);
            school.CurrentValue = 3;
        }
    }
}
