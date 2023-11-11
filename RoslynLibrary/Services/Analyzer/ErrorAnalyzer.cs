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
    internal class ErrorAnalyzer : IAnalyzer
    {
        public bool CanHandle(AnalyzeType analyzeType)
        {
            return analyzeType == AnalyzeType.Error;
        }

        public string Analyze(CompilationErrorModel error, string nodeText, string regexPattern, string regexReplacement)
        {
            var code = error.Location.ToCodeLocationString();

            //regexPattern = regexPattern.Replace("$this", code);
            regexReplacement = regexReplacement.Replace("$this", code);

            var lineCode = error.Location.ToCodeLineString();

            if (regexPattern == "$this")
            {
                nodeText = nodeText.Replace(lineCode,
                   lineCode.Replace(code, regexReplacement)
                   );
            }
            else
            {
                nodeText = nodeText.Replace(lineCode,
                   lineCode.Replace(code,
                           Regex.Replace(code, regexPattern, regexReplacement)
                       )
                   );
            }

            return nodeText;
        }
    }
}
