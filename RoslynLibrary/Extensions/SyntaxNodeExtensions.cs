using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynLibrary.Extensions
{
    internal static class SyntaxNodeExtensions
    {
        public static string ToFormattedCodeLines(this SyntaxNode node)
        {
            var sourceText = node.SyntaxTree.GetText();
            var lines = new StringBuilder();
            var startLine = sourceText.Lines.GetLineFromPosition(node.SpanStart).LineNumber;
            var endLine = sourceText.Lines.GetLineFromPosition(node.Span.End).LineNumber;

            // Проходим по строкам, которые покрывает данный SyntaxNode
            for (int i = startLine; i <= endLine; i++)
            {
                var lineText = sourceText.Lines[i].ToString();
                lines.AppendLine($"{i + 1}: {lineText}");
            }

            return lines.ToString();
        }
    }
}
