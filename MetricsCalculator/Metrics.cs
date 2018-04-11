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
        #region Packages
        public static List<string> SearchPackages(string path, out int count)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException();

            List<string> packages = new List<string>();

            foreach(var file in SearchClasses(path))
            {
                string [] text = File.ReadAllLines(file);

                for (int i = 0; i < text.Length; i++)
                    if (text[i].StartsWith("package") && text[i].EndsWith(";") && !text[i].Contains(@"//"))
                        packages.Add(text[i]);
            }

            count = packages.Distinct().Count();

            return packages.Distinct().ToList();
        }

        private static List<string> SearchClasses(string path)
        {
            List<string> files = Directory.GetFiles(path + "/", "*.java").ToList();

            foreach (var directory in Directory.GetDirectories(path + "/"))
                foreach (var el in SearchClasses(directory + "/"))
                    files.Add(el);

            return files;
        }

        #endregion

        #region Inheritance

        public static double GetInheritance(string path, out List<ClassTemplate> listOfClasses)
        {
            List<ClassTemplate> classes = GetClassTemplates(path);
            double numberOfRootClasses = 0;
            double sumDeepOfInheritance = 0;

            foreach (var el in classes)
            {
                int dippest = 0;
                if (el.IsRoot)
                {
                    numberOfRootClasses++;
                    GetInheritance(el, 0, ref dippest);
                }
                sumDeepOfInheritance += dippest;
            }

            listOfClasses = classes;

            return (sumDeepOfInheritance) / (numberOfRootClasses); 
        }

        private static List<ClassTemplate> GetClassTemplates(string path)
        {
            List<ClassTemplate> classes = new List<ClassTemplate>();

            foreach (var file in SearchClasses(path))
            {
                string source = File.ReadAllText(file);
                Match m = Regex.Match(source, @"(class)\s(\p{Lu}.*?\w+)\s((extends)\s(\p{Lu}.*?\w+))?");

                if (m.Success)
                {
                    ClassTemplate template = new ClassTemplate();
                    template.Name = m.Groups[2].Value;

                    if (m.Groups[4].Value.Length > 1)
                        template.Parent = new ClassTemplate() { Name = m.Groups[5].Value };
                    else
                        template.IsRoot = true;

                    classes.Add(template);
                }
            }

            for (int i = 0; i < classes.Count; i++)
            {
                if (!classes[i].IsRoot)
                {
                    bool hasParent = false;
                    for (int j = 0; j < classes.Count; j++)
                    {
                        if (classes[i].Parent.Name == classes[j].Name)
                        {
                            hasParent = true;
                            classes[i].Parent = classes[j];
                            classes[j].Children.Add(classes[i]);
                        }
                    }
                    if (!hasParent)
                        classes[i].IsRoot = true;
                }
            }

            return classes;
        }

        private static void GetInheritance(ClassTemplate template, int level, ref int dippest)
        {
            dippest = dippest < level ? level : dippest;

            foreach (ClassTemplate el in template.Children)
                GetInheritance(el, level + 1, ref dippest);
        }
        #endregion
    }
}
