using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using RoslynLibrary.Extensions;
using RoslynLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ICSharpCode.Decompiler.IL.Transforms.Stepper;

namespace RoslynLibrary.Services.Analyzer
{
    internal class AllOrMethodAnalyzer : IAnalyzer
    {
        public bool CanHandle(AnalyzeType analyzeType)
        {
            return analyzeType == AnalyzeType.Method || analyzeType == AnalyzeType.All;
        }

        public string Analyze(CompilationErrorModel error, string nodeText, string regexPattern, string regexReplacement)
        {
            if(regexPattern == "$this")
                return regexReplacement.Replace("$this", nodeText);

            regexPattern = regexPattern.Replace("$errorLine", error.Line.ToString());

            string result = CustomRegex.EvaluateInput(regexPattern.Replace("$this", nodeText));

            if (string.IsNullOrEmpty(result))
            {
                return Regex.Replace(nodeText, regexPattern, regexReplacement);
            }

            return Regex.Replace(nodeText, result, regexReplacement);
        }
    }
}