using RoslynLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            regexReplacement = regexReplacement.Replace("$this", nodeText);

            if (regexPattern == "$this")
            {
                nodeText = nodeText.Replace(nodeText, regexReplacement);
            }
            else
            {
                nodeText = Regex.Replace(nodeText, regexPattern, regexReplacement);
            }

            return nodeText;
        }
    }
}
