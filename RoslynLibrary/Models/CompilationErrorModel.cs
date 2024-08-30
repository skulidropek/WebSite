using Microsoft.CodeAnalysis;
using RoslynLibrary.Extensions;

namespace RoslynLibrary.Models
{
    public class CompilationErrorModel
    {
        public string Text { get; set; }
        public Location Location { get; private set; }

        public int Line
        {
            get
            {
                var lineSpan = Location.GetLineSpan();
                return lineSpan.StartLinePosition.Line + 1;
            }
        }

        public int Symbol
        {
            get
            {
                var lineSpan = Location.GetLineSpan();
                return lineSpan.StartLinePosition.Character + 1;
            }
        }

        public CompilationErrorModel(Location location, string text)
        {
            Location = location;
            Text = text;
        }

        public string GetCode()
        {
            return Location.ToCodeLineString();
        }
    }
}
