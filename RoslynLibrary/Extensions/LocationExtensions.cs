using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using RoslynLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoslynLibrary.Extensions
{
    internal static class LocationExtensions
    {
        public static string ToCodeLocationString(this Location location)
        {
            SourceText sourceText = location.SourceTree.GetText();
            string lineText = sourceText.GetSubText(location.SourceSpan).ToString();
            return lineText;
        }
        public static string ToCodeLineString(this Location location)
        {
            SourceText sourceText = location.SourceTree.GetText();
            string lineText = sourceText.Lines.GetLineFromPosition(location.SourceSpan.Start).ToString();
            return lineText;
        }

        public static (int, int) GetStartAndEndLines(this Location location)
        {
            var syntaxTree = location.SourceTree;
            var span = syntaxTree.GetLineSpan(location.SourceSpan);
            var start = span.StartLinePosition;
            var end = span.EndLinePosition;
            var startLine = start.Line + 1;
            var endLine = end.Line + 1;

            return (startLine, endLine);
        }
    }
}
