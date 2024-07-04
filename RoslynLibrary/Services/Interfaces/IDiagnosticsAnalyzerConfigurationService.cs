using RoslynLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynLibrary.Services.Interfaces
{
    public interface IDiagnosticsAnalyzerConfigurationService
    {
        List<DiagnosticAnalyzerModel> AnalyzeBaseOverrideModels { get; }
    }
}
