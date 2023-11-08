using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace RoslynLibrary.Models
{
    public class AnalyzeBaseModel
    {
        public string ErrorText { get; set; }
        public bool IsRequiresAnalysis { get; set; }
        //public AnalyzeType AnalyzeType { get; set; }
        public string RegexPattern { get; set; }
        public string RegexReplacement { get; set; }

        public string Description { get; set; }

        public DeclarationType DeclarationType { get; set; }
    }
}
