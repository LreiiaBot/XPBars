using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace XPBars
{
    public static class Helper
    {
        public static ObservableCollection<T> Convert<T>(this IEnumerable<T> original)
        {
            return new ObservableCollection<T>(original);
        }

        public static bool ContainsChar(this string sequence, string characters)
        {
            bool contains = false;

            foreach (var character in characters)
            {
                if (sequence.Contains(character))
                {
                    contains = true;
                    break;
                }
            }
            return contains;
        }
    }
}
