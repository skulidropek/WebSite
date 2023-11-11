using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RoslynLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ICSharpCode.Decompiler.IL.Transforms.Stepper;

namespace RoslynLibrary.Services.Analyzer
{
    internal class MethodAnalyzer : IAnalyzer
    {
        public bool CanHandle(AnalyzeType analyzeType)
        {
            return analyzeType == AnalyzeType.Method || analyzeType == AnalyzeType.All;
        }

        public string Analyze(CompilationErrorModel error, string nodeText, string regexPattern, string regexReplacement)
        {
            if(regexPattern == "$this")
                return regexReplacement;

            string result = ParseAndExecute(regexPattern.Replace("$this", nodeText));

            if (string.IsNullOrEmpty(result))
            {
                return Regex.Replace(nodeText, regexPattern, regexReplacement);
            }

            return Regex.Replace(nodeText, result, regexReplacement);
        }

        public string ParseAndExecute(string input)
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

                    if (variables.ContainsKey(variableName) && PerformAction(variables[variableName], action, condition))
                    {
                        return regexPattern;
                    }
                }
            }

            return "";
        }

        private bool PerformAction(string variableValue, string action, string condition)
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