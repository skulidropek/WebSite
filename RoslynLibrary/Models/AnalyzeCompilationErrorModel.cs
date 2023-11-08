using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RoslynLibrary.Models
{
    public class AnalyzeCompilationErrorModel
    {
        public CompilationErrorModel CompilationErrorModel { get; set; }
        public List<AnalyzeBaseModel> AnalyzeBaseModels { get; set; }
    } 
}
