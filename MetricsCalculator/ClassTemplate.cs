using System.Collections.Generic;

namespace MetricsCalculator
{
    public class ClassTemplate
    {
        public string Name { get; set; }
        public ClassTemplate Parent { get; set; }
        public bool IsRoot { get; set; }
        public List<ClassTemplate> Children { get; set; }

        public ClassTemplate()
        {
            Children = new List<ClassTemplate>();
        }
    }
}