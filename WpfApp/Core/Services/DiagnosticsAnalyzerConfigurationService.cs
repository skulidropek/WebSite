using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Core.Services
{
    internal class DiagnosticsAnalyzerConfigurationService
    {
        public string ConfigurationName { get; set; }

        public Dictionary<string, string> Diagnostics { get; set; }
    }
}
