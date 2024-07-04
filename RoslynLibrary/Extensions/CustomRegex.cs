using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoslynLibrary.Extensions
{
    public static class CustomRegex
    {
        public static string EvaluateInput(string input)
        {
            var variables = new Dictionary<string, string>();
            var ifMatches = Regex.Matches(input.Replace("\n", " "), @"var (\w+) = ""(.*?)"";|if\((\w+)\.(\w+)\(""(.*?)""\)\)\s*?\{([\s\S]*?)\}");

            foreach (Match match in ifMatches)
            {
                if (match.Groups[1].Success) // Variable declaration
                {
                    variables[match.Groups[1].Value] = match.Groups[2].Value;
                }
                else // if condition
                {
                    string variableName = match.Groups[3].Value;
                    string action = match.Groups[4].Value;
                    string condition = match.Groups[5].Value;
                    string regexPattern = match.Groups[6].Value.Trim();

                    if (variables.ContainsKey(variableName) && CheckCondition(variables[variableName], action, condition))
                    {
                        return regexPattern;
                    }
                }
            }

            return "";
        }
        private static bool CheckCondition(string variableValue, string action, string condition)
        {
            return action.ToLower() switch
            {
                "contains" => variableValue.Contains(condition),
                "startswith" => variableValue.StartsWith(condition),
                "endswith" => variableValue.EndsWith(condition),
                _ => throw new NotImplementedException($"Действие '{action}' не реализовано.")
            };
        }
    }
}
