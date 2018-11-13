using System.Collections.Generic;

namespace VisualStudioExtensions.GenerateClass
{
    public interface IClassGeneratorEngine
    {
        List<string> Generate(string className, List<string> propertyNames);
    }
}