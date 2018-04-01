using MetricsCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JavaMetricsCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "D:/КСЮ/Programs/Projects/Архіви/LeafPic-dev";
            var list = Metrics.SearchClasses(path);

            foreach (var el in list)
                Console.WriteLine(el);

            Console.WriteLine("\n\n " + Metrics.NumberOfFiles(path));

            Console.ReadKey();
        }
    }
}
