using Microsoft.Extensions.Configuration;
using RoslynLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Core.Services.Interfaces;

namespace WpfApp.Core.Services
{
    internal class ConfigurationService
    {
        public AnalyzeConfigurationService AnalyzeConfiguration { get; set; }
        public IDiagnosticsAnalyzerConfigurationService DiagnosticsAnalyzerConfiguration { get; set; }
    }
}
