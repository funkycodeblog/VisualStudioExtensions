using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioExtensions
{
    public static class CSharpExtensions
    {
        public static bool ContainsAny(this string str, params string[] values)
        {
            foreach (var val in values)
            {
                if (str.Contains(val))
                    return true;
            }

            return false;
        }

        public static bool StartsWithAny(this string str, params string[] values)
        {
            foreach (var val in values)
            {
                if (str.StartsWith(val))
                    return true;
            }

            return false;
        }

    }
}
