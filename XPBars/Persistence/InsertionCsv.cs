using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPBars
{
    public class InsertionCsv
    {
        public static string Filename { get; set; } = "Insertions.csv";
        public static List<Insertion> Read(string path)
        {
            List<Insertion> insertions = new List<Insertion>();
            Insertion insertion = null;
            //string filename = new DirectoryInfo(path).Name;
            //filename += ".log";
            string filePath = Path.Combine(path, Filename);
            if (!File.Exists(filePath))
            {
                return insertions;
            }
            using (var reader = new StreamReader(filePath))
            {
                string row = reader.ReadLine();
                while (row != null)
                {
                    insertion = FromCsvToInsertion(row);
                    insertions.Add(insertion);
                    row = reader.ReadLine();
                }
            }

            return insertions;
        }
        public static List<Insertion> Read(XPBar xpbar)
        {
            string path = XPBarCsv.Basedir;

            // add path
            List<string> dirs = new List<string>();
            dirs.Add(xpbar.Description);
            Constructpath(dirs, xpbar.Parentbar);
            dirs.Insert(0, path);
            path = Path.Combine(dirs.ToArray());

            return Read(path);
        }
        public static void Constructpath(List<string> dirs, XPBar parentXPBar)
        {
            if (parentXPBar == null)
            {
                return;
            }
            dirs.Insert(0, parentXPBar.Description);
            Constructpath(dirs, parentXPBar.Parentbar);
        }
        public static void Save(XPBar xpbar)
        {
            string path = XPBarCsv.Basedir;

            // add path
            List<string> dirs = new List<string>();
            dirs.Add(xpbar.Description);
            Constructpath(dirs, xpbar.Parentbar);
            dirs.Insert(0, path);
            path = Path.Combine(dirs.ToArray());

            Directory.CreateDirectory(path);
            string filePath = Path.Combine(path, Filename);

            using (var writer = new StreamWriter(filePath, true))
            {
                foreach (var insertion in xpbar.Protocol)
                {
                    if (insertion.PersistenceAction == PersistenceAction.Insert)
                    {
                        writer.WriteLine(FromInsertionToCsv(insertion));
                    }
                }
            }
        }
        public static Insertion FromCsvToInsertion(string csv)
        {
            string[] fields = csv.Split(XPBarCsv.Separator);
            Insertion insertion = null;
            insertion = new Insertion(fields[0], Convert.ToInt32(fields[1]), (XPWeight)Convert.ToInt32(fields[2]), Convert.ToBoolean(fields[3]), Convert.ToDateTime(fields[4]), Convert.ToInt32(fields[5]));

            return insertion;
        }
        public static string FromInsertionToCsv(Insertion insertion)
        {
            return $"{insertion.Description}{XPBarCsv.Separator}{insertion.Value}{XPBarCsv.Separator}{(int)insertion.Weight}{XPBarCsv.Separator}{insertion.IgnoreWeight}{XPBarCsv.Separator}{insertion.Date}{XPBarCsv.Separator}{insertion.FlatXP}";
        }
    }
}
