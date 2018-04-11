using MetricsCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace JavaMetricsCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = "D:/КСЮ/Programs/Projects/Архіви/LeafPic-dev"; //21 +
            //string path = @"D:\КСЮ\Programs\Projects\Архіви\santa-tracker-android-master"; //38
            string path = @"D:\КСЮ\Programs\Projects\Архіви\RedReader-master"; //35

            var packages = Metrics.SearchPackages(path, out int count);

            foreach (var el in packages)
                Console.WriteLine(el);

            Console.WriteLine("\nNumber of packages (NOP): " + count + "\n");
            Console.ReadKey();

            //==========================================================================

            var hit = Metrics.GetInheritance(path, out List<ClassTemplate> list);
            PrintClasses(list);
            Console.WriteLine("\n\nHeight of inheritance tree (HIT): " + hit);

            Console.ReadKey();
        }

        private static void PrintClasses(List<ClassTemplate> list)
        {
            foreach (var el in list)
            {
                string parent;

                if (el.Parent != null)
                    parent = el.Parent.Name;
                else parent = "";

                Console.Write("\nclass " + el.Name);

                if (string.IsNullOrEmpty(parent))
                    Console.Write(" has no parent");
                else
                    Console.Write(" extends " + parent);
            }
        }
    }
}
