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
        public int Line;
        public int Symbol;
        public string Text;
        public Location Location;

        public string GetCode()
        {
            return Location.ToCodeLineString();
        }
    }
}
