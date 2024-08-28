using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoslynLibrary.Extensions
{
    public class UniqueElementsExtractor
    {
        public static string[] ExtractUniqueElements(string input)
        {
            var uniqueElements = new HashSet<string>();
            string pattern = @"(""|')([^'""]+)(""|')";

            // Поиск совпадений по регулярному выражению
            var matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                for (int i = 1; i < match.Groups.Count; i++)
                {
                    var value = Regex.Replace(match.Groups[i].Value, "(\"|')", "");
                    if (!string.IsNullOrEmpty(value))
                    {
                        uniqueElements.Add(value);
                    }
                }
            }

            // Преобразование HashSet в массив
            return new List<string>(uniqueElements).ToArray();
        }
    }
}
