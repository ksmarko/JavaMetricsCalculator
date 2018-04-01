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
           
            foreach (var el in Metrics.SearchPackages(path))
                Console.WriteLine(el);

            Console.WriteLine("\n\n " + Metrics.NumberOfPackages(path) + "\n\n");

            Console.ReadKey();
        }
    }
}
