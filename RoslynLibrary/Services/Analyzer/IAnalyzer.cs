using RoslynLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynLibrary.Services.Analyzer
{
    public interface IAnalyzer
    {
        bool CanHandle(AnalyzeType analyzeType);
        string Analyze(CompilationErrorModel error, string nodeText, string regexPattern, string regexReplacement);
    }
}
