using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioExtensions.GenerateClass
{
    public interface IGenerateClassInputDataProvider
    {
        GenerateClassInputData GetInputData(string selection);
    }
}
