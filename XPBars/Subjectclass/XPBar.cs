using System.Collections.Generic;

namespace XPBars
{
    class XPBar : XPInfo
    {
        public XPBar Parentbar { get; set; }
        public List<XPBar> Subbars { get; set; }
    }
}
