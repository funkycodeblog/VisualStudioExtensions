using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioExtensions.GenerateClass
{


    public class ClassGeneratorEngine : IClassGeneratorEngine
    {
        private readonly IPropertyTypeResolver _propertyTypeResolver;

        public ClassGeneratorEngine(IPropertyTypeResolver propertyTypeResolver)
        {
            _propertyTypeResolver = propertyTypeResolver;
        }

        public List<string> Generate(string className, List<string> propertyNames)
        {

            var code = new List<string>();

            code.Add($"public class {className}");
            code.Add("{");

            foreach (var prop in propertyNames)
            {
                if (string.IsNullOrEmpty(prop)) continue;
                var nameType = _propertyTypeResolver.Resolve(prop);
                var line = $"\tpublic {nameType.Type} {nameType.Name} {{get; set;}}";
                code.Add(line);
            }

            code.Add("}");

            return code;

        }

    }
}
