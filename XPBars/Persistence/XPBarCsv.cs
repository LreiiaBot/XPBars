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
        public static XPBar Read()
        {
            if (!Directory.Exists(Basedir))
            {
                return new XPBar("Master");
            }
            XPBar xpbar = null;
            xpbar = ReadDirs(Basedir, xpbar);
            return xpbar;
        }
        public static XPBar ReadDirs(string parentDir, XPBar parent)
        {
            XPBar xpbar = null;
            string thisDir = String.Empty;
            string[] dirs = Directory.GetDirectories(parentDir);
            foreach (var dir in dirs)
            {
                using (var reader = new StreamReader(Path.Combine(dir, new DirectoryInfo(dir).Name + ".csv")))
                {
                    string row = reader.ReadLine();
                    xpbar = FromCsvToBar(row);
                    xpbar.Parentbar = parent;
                    if (parent != null)
                    {
                        parent.Subbars.Add(xpbar);
                    }
                    thisDir = Path.Combine(parentDir, xpbar.Description);
                }
                // read protocol
                xpbar.Protocol = InsertionCsv.Read(xpbar).Convert();
                ReadDirs(thisDir, xpbar);
            }
            return xpbar;
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
            InsertionCsv.Save(xpbar);

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
