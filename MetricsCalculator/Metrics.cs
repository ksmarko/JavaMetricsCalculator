using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MetricsCalculator
{
    public class Metrics
    {
        public static List<string> SearchPackages(string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException();

            List<string> packages = new List<string>();

            foreach(var file in SearchClasses(path))
            {
                string [] text = File.ReadAllLines(file);

                for (int i = 0; i < text.Length; i++)
                    if (text[i].Contains("package") && text[i].Contains(GetProjectId(path)))
                        packages.Add(text[i]);
            }

            return packages.Distinct().ToList();
        }

        private static List<string> DetectProjectIdFile(string path)
        {
            List<string> files = Directory.GetFiles(path + "/", "build.gradle").ToList();
            
            foreach (var directory in Directory.GetDirectories(path + "/"))
                foreach (var el in DetectProjectIdFile(directory + "/"))
                    files.Add(el);

            return files;
        }

        public static string GetProjectId(string path)
        {
            List<string> files = DetectProjectIdFile(path);
            string[] text = null;
            string appId = "#";

            for (int i = 0; i < files.Count; i++)
            {
                text = File.ReadAllLines(files[i]);

                for (int k = 0; k < text.Length; k++)
                    if (text[k].Contains(" applicationId "))
                        appId = text[k];
            }

            var result = from Match match in Regex.Matches(appId, "\"([^\"]*)\"") select match;
            return result.FirstOrDefault().ToString().Replace("\"", String.Empty);
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

        public static int NumberOfPackages(string path)
        {
            return SearchPackages(path).Count();
        }
    }
}
