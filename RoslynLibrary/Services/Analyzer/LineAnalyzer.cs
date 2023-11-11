using RoslynLibrary.Extensions;
using RoslynLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoslynLibrary.Services.Analyzer
{
    internal class LineAnalyzer : IAnalyzer
    {
        public bool CanHandle(AnalyzeType analyzeType)
        {
            return analyzeType == AnalyzeType.Line;
        }

        public string Analyze(CompilationErrorModel error, string nodeText, string regexPattern, string regexReplacement)
        {
            var code = error.Location.ToCodeLineString();

            regexReplacement = regexReplacement.Replace("$this", code);

            if (regexPattern == "$this")
            {
                nodeText = nodeText.Replace(code, regexReplacement);
            }
            else
            {
                nodeText = nodeText.Replace(code,
                    Regex.Replace(code, regexPattern, regexReplacement)
                );
            }

            return nodeText;
        }
    }
}
