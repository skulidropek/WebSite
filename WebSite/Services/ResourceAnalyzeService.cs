using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using RoslynLibrary.Models;
using RoslynLibrary.Services;
using System.Text;



namespace WebSite.Services
{
    public class ResourceAnalyzeService
    {
        private readonly PluginDiagnosticsAnalyzerService _pluginAnalyzer;

        public ResourceAnalyzeService(PluginDiagnosticsAnalyzerService pluginAnalyzer)
        {
            _pluginAnalyzer = pluginAnalyzer;
        }

        public async Task<string> GetAnalyzeText(string path)
        {
            var text = Encoding.UTF8.GetString(await Extension.Resource.ReadAllBytesAsync(path));
            return await GetAnalyzeText(CSharpSyntaxTree.ParseText(text));
        } 
        
        public async Task<string> GetAnalyzeText(SyntaxTree tree)
        {
            var errors = await _pluginAnalyzer.AnalyzeCompilationAsync(tree);

            return GetAnalyzeText(errors);
        }
        
        public string GetAnalyzeText(List<CompilationErrorModel> compilationErrorModels)
        {
            if (compilationErrorModels == null || compilationErrorModels.Count == 0)
                return "В плагине нет ошибок";
            
            return string.Join("<br>", compilationErrorModels.Select(s => "<span style='color:red;'>" + $"[{s.Line},{s.Symbol}] " + s.Text + "</span>" + " Код:" + s.GetCode()));
        }
    }
}
