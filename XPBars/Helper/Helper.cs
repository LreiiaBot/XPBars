using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPBars
{
    public static class Helper
    {
        public static ObservableCollection<T> Convert<T>(this IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }
    }
}
