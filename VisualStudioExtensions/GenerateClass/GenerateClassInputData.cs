using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioExtensions.GenerateClass
{
    public class GenerateClassInputData
    {
        public string ClassName { get; set; }
        public List<string> PropertyNames { get; set; }
    }
}
