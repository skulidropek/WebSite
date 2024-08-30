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
                return regexReplacement;

            regexPattern = regexPattern.Replace("$errorLine", error.Line.ToString());

            string result = CustomRegex.EvaluateInput(regexPattern.Replace("$this", nodeText));

            if (string.IsNullOrEmpty(result))
            {
                return ReplaceLineUsingRegex(nodeText, regexPattern, regexReplacement);
            }

            return ReplaceLineUsingRegex(nodeText, result, regexReplacement);
        }


        static string ReplaceLineUsingRegex(string originalCode, string pattern, string replacement)
        {
            // Генерируем нумерованный текст
            string numberedCode = GenerateNumberedCodeUsingRoslyn(originalCode);

            // Проверяем, нужно ли искать по номеру строки
            bool isLineNumberSearch = Regex.IsMatch(pattern, @"^(\d+|\\d\+|\\d|\\d\*):");

            if (isLineNumberSearch)
            {
                pattern = Regex.Replace(pattern, @"^(\d+|\\d\+|\\d|\\d\*):(.+)", "^($1):$2");
                // Ищем строки по номеру с возможностью использования группировок
                MatchCollection matches = Regex.Matches(numberedCode, pattern, RegexOptions.Multiline);
                var originalLines = originalCode.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        var localReplacement = replacement;

                        var lineNumber = int.Parse(match.Groups[1].ToString());

                        localReplacement = localReplacement.Replace("$line", lineNumber.ToString());

                        for (int i = 2; i < match.Groups.Count; i++)
                        {
                            localReplacement = localReplacement.Replace($"${i - 1}", match.Groups[i].Value);
                        }

                        originalLines[lineNumber - 1] = localReplacement;
                    }
                }

                return string.Join(Environment.NewLine, originalLines);
            }
            else
            {
                var originalLines = originalCode.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                for (int i = 0; i < originalLines.Length; i++)
                {
                    originalLines[i] = Regex.Replace(originalLines[i], pattern, replacement);
                }

                return string.Join(Environment.NewLine, originalLines);
            }
        }
        static string GenerateNumberedCodeUsingRoslyn(string code)
        {
            var tree = CSharpSyntaxTree.ParseText(code);
            var text = tree.GetText();

            var numberedCode = new StringBuilder();
            foreach (var line in text.Lines)
            {
                var lineNumber = line.LineNumber + 1; // LineNumber is zero-based, so add 1
                var lineText = line.ToString();
                numberedCode.AppendLine($"{lineNumber}: {lineText}");
            }

            return numberedCode.ToString();
        }
    }
}