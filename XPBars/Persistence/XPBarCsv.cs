using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPBars
{
    public class XPBarCsv
    {
        public static string Basedir { get; set; } = "data";
        public static char Separator { get; set; } = ';';
        public static List<XPBar> Read()
        {
            List<XPBar> xpbars = new List<XPBar>();
            using (var reader = new StreamReader(Basedir))
            {
                // TODO
            }
            return xpbars;
        }
        public static void Save(IEnumerable<XPBar> xpbars)
        {
            // create base dir
            Directory.CreateDirectory(Basedir);

            foreach (XPBar xpbar in xpbars)
            {
                SaveDirs(xpbar, Basedir);
            }
        }

        public static void SaveDirs(XPBar xpbar, string parentDir)
        {
            string thisDir = Path.Combine(parentDir, xpbar.Description);
            // create dir if not already existing
            Directory.CreateDirectory(thisDir);

            // open writer
            using (var writer = new StreamWriter(Path.Combine(thisDir, xpbar.Description + ".csv"), false))
            {
                // write sinlge line with info
                writer.WriteLine(FromBarToCsv(xpbar));
            }

            // write protocols
            // TODO

            // do the same for all subbars
            foreach (var subbar in xpbar.Subbars)
            {
                SaveDirs(subbar, Path.Combine(thisDir));
            }
        }

        public static XPBar FromCsvToBar(string csv)
        {
            string[] fields = csv.Split(Separator);
            XPBar xpbar = null;
            xpbar = new XPBar(fields[0], Convert.ToInt32(fields[1]), Convert.ToInt32(fields[2]), Convert.ToBoolean(fields[3]));

            return xpbar;
        }
        public static string FromBarToCsv(XPBar xpbar)
        {
            return $"{xpbar.Description}{Separator}{xpbar.CurrentValue}{Separator}{xpbar.Level}{Separator}{xpbar.Done}";
        }
    }
}
