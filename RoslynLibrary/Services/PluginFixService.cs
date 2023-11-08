using Microsoft.CodeAnalysis;
using RoslynLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoslynLibrary.Services
{
    public class PluginFixService
    {
        private readonly PluginDiagnosticsAnalyzerService _pluginDiagnosticsAnalyzer;
        private readonly CodeErrorFixerService _codeErrorFixer;

        public PluginFixService(PluginDiagnosticsAnalyzerService pluginDiagnosticsAnalyzer, CodeErrorFixerService codeErrorFixer)
        {
            _pluginDiagnosticsAnalyzer = pluginDiagnosticsAnalyzer;
            _codeErrorFixer = codeErrorFixer;
        }

        public async Task<SyntaxNode> Fix(SyntaxTree tree, IEnumerable<AnalyzeBaseModel> analyzeBaseModels)
        {
            //await _pluginDiagnosticsAnalyzer.AnalyzeCompilation(tree);
            return await _codeErrorFixer.VisitAndFixErrors(tree.GetRoot(), analyzeBaseModels);
        }
    }
}
