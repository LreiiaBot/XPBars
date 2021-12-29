using System.Collections.ObjectModel;

namespace XPBars
{
    public class MainViewModel : BaseViewModel
    {
        public string Text { get; set; } = "yikes";
        public string Abc { get; set; } = "das hat mit christen nichts zu tun";
        public ObservableCollection<XPBar> LXPBars { get; set; } = new ObservableCollection<XPBar>();
        public MainViewModel()
        {
            var masterBar = new XPBar("Master", 14, 10, true);
            LXPBars.Add(masterBar);
            masterBar.Subbars.Add(new XPBar("Test"));
            masterBar.Subbars.Add(new XPBar("Super Long Demo Name"));
            var school = new XPBar("School");
            school.Level = 3;
            school.Subbars.Add(new XPBar("Math", 9, 31, true));
            var english = new XPBar("English");
            school.Subbars.Add(english);
            english.Done = true;
            english.Subbars.Add(new XPBar("Vocabulary"));
            school.Subbars.Add(new XPBar("German", 1, 1, false));
            masterBar.Subbars.Add(school);
            school.CurrentValue = 3;
        }
    }
}
