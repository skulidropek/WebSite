using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Core.Services.Interfaces
{
    class AnalyzeConfigurationService
    {
        public string ConfigurationName {get; set;}

        public List<AnalyzeBaseOverrideModel> AnalyzeBaseModels { get; set; }
    }
}
