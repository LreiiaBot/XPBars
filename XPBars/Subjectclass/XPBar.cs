using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XPBars
{
    public class XPBar : XPInfo
    {
        public XPBar Parentbar { get; set; }
        public ObservableCollection<XPBar> Subbars { get; set; } = new ObservableCollection<XPBar>();

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

        public static void Save(IEnumerable<XPBar> xpbars)
        {
            XPBarCsv.Save(xpbars);
        }
    }
}
