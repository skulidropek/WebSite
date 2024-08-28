using Microsoft.CodeAnalysis;
using RoslynLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoslynLibrary.Models
{
    public class CompilationErrorModel
    {
        private string[] _extractedUniqueElements;
        private string _text;

        public int Line { get; set; }
        public int Symbol { get; set; }
        public string Text { get => _text; set { _text = value; _extractedUniqueElements = UniqueElementsExtractor.ExtractUniqueElements(value); } }
        public Location Location { get; set; }
        public string[] ExtractedUniqueElements => _extractedUniqueElements;

        public string GetCode()
        {
            return Location.ToCodeLineString();
        }
    }
}
