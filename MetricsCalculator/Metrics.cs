using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsCalculator
{
    public class Metrics
    {
        public static int NumberOfPackages(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException();

            return 0;
        }

        public static List<string> SearchClasses(string path)
        {
            List<string> files = Directory.GetFiles(path + "/", "*.java").ToList();

            foreach (var directory in Directory.GetDirectories(path + "/"))
                foreach (var el in SearchClasses(directory + "/"))
                    files.Add(el);

            return files;
        }

        public static int NumberOfFiles(string path)
        {
            return SearchClasses(path).Count;
        }
    }
}
