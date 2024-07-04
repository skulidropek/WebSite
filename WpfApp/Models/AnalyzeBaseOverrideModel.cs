using RoslynLibrary.Models;
using Newtonsoft.Json;

namespace WpfApp.Models
{
    internal class AnalyzeBaseOverrideModel : AnalyzeBaseModel
    {
        [JsonIgnore]
        public bool IsActive { get; set; }
    }
}
