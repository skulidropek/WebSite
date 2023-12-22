using Microsoft.CodeAnalysis.CSharp;
using RoslynLibrary.Models;
using RoslynLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class TestService
    {
        private PluginDiagnosticsAnalyzerService _pluginDiagnosticsAnalyzerService;
        private PluginFixService _pluginFixService;

        public TestService(PluginDiagnosticsAnalyzerService pluginDiagnosticsAnalyzerService, PluginFixService pluginFixService)
        {
            _pluginDiagnosticsAnalyzerService = pluginDiagnosticsAnalyzerService;
            _pluginFixService = pluginFixService;
        }

        public async void Start()
        {
            var text = File.ReadAllText("D:\\Download\\AirEvent (3).cs");

            var tree = CSharpSyntaxTree.ParseText(text);

            var node = await _pluginFixService.Fix(tree, new List<AnalyzeBaseModel>()
            {
                //$errorGroup1
//                new AnalyzeBaseModel()
//                {
//                    ErrorText = "Аргумент 1: не удается преобразовать из \"(NetworkableId)\" в \"uint\".",
//                    AnalyzeType = AnalyzeType.All,
//                    DeclarationType = DeclarationType.All,
//                    RegexPattern = @"
//var code = ""$this"";
//if(code.Contains(""uint""))
//{
//  net.ID
//}",
//                    RegexReplacement = "ulong"
//                }
            });

            tree = CSharpSyntaxTree.ParseText(node.ToFullString());
            //Console.WriteLine(tree.ToString());

            var errors = await _pluginDiagnosticsAnalyzerService.AnalyzeCompilationAsync(tree);

            foreach (var error in errors)
            {
                Console.WriteLine($"[{error.Line}:б{error.Symbol}] " + error.Text + " " + error.GetCode());
            }
        }
    }
}
