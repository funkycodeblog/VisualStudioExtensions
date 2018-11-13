using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VisualStudioExtensions.GenerateClass
{

    public class NameType
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class PropertyTypeResolver : IPropertyTypeResolver
    {
        const string REGEX = "(?<propertyName>[a-zA-Z0-9_]*)(?<sufixes>.*)";

        public NameType Resolve(string input)
        {

            Match match = Regex.Match(input, REGEX);
            if (null == match)
                return null;

            Group propertyGroup = match.Groups["propertyName"];
            Group sufixesGroup = match.Groups["sufixes"];

            string propertyName = "";
            string sufixes = "";
            string propertyType = "object";
            bool isTypeResolved = false;


            if (null != propertyGroup && (propertyGroup.Success))
            {
                propertyName = propertyGroup.Value;
            }

            if (null != sufixesGroup && (sufixesGroup.Success))
            {
                sufixes = sufixesGroup.Value;
            }

            if (sufixes.Contains("^"))
            {
                propertyType = propertyName;
                isTypeResolved = true;
            }

            if (!isTypeResolved)
            {
                if (propertyName.StartsWithAny("Is", "Can") || sufixes.Contains("?"))
                {
                    propertyType = "bool";
                }

                if (propertyName.ContainsAny("Size", "Number", "Count", "Id") || sufixes.Contains("#"))
                {
                    propertyType = "int";
                }

                if (propertyName.ContainsAny("Name", "String", "Caption", "Text", "Sql", "Description") || sufixes.Contains("$"))
                {
                    propertyType = "string";
                }

                if (propertyName.ContainsAny("Amount") || sufixes.Contains("*"))
                {
                    propertyType = "decimal";
                }

                if (propertyName.ContainsAny("Coeff") || sufixes.Contains("-"))
                {
                    propertyType = "double";
                }

                if (propertyName.ContainsAny("Date", "Time"))
                {
                    propertyType = "DateTime";
                }
            }


            if (sufixes.Contains("<"))
                propertyType = GetAsList(propertyType);
            else if (sufixes.Contains("["))
                propertyType = GetAsArray(propertyType);

            return new NameType
            {
                Name = propertyName,
                Type = propertyType
            };

        }

        string GetAsArray(string type)
        {
            return $"{type}[]";
        }

        string GetAsList(string type)
        {
            return $"List<{type}>";
        }

        string GetValue(string input, string regex, string groupName)
        {
            Match match = Regex.Match(input, regex);
            if (null == match)
                return null;

            Group funGroup = match.Groups[groupName];
            if (null == funGroup || (!funGroup.Success))
                return null;

            string functionName = funGroup.Value;
            return functionName;
        }
    }
}
