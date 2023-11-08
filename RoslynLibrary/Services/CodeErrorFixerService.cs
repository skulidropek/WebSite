using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using RoslynLibrary.Models;
using RoslynLibrary.Extensions;
using RoslynLibrary.Services;
using System.Text.RegularExpressions;

public class CodeErrorFixerService : CSharpSyntaxRewriter
{
    private readonly PluginDiagnosticsAnalyzerService _pluginDiagnosticsAnalyzer;
    private IEnumerable<AnalyzeBaseModel> _analyzeBaseModels;
    private IEnumerable<CompilationErrorModel> _compilationErrors;
    public CodeErrorFixerService(PluginDiagnosticsAnalyzerService pluginDiagnosticsAnalyzer)
    {
        _pluginDiagnosticsAnalyzer = pluginDiagnosticsAnalyzer;
    }

    public async Task<SyntaxNode> VisitAndFixErrors(SyntaxNode node, IEnumerable<AnalyzeBaseModel> analyzeBaseModels)
    {
        _analyzeBaseModels = analyzeBaseModels.Where(s => s.IsRequiresAnalysis);

        if (_compilationErrors == null)
        {
            _compilationErrors = await _pluginDiagnosticsAnalyzer.AnalyzeCompilation(node.SyntaxTree);
        }

        return base.Visit(node);
    }

    public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node) => FixCompilationError(node, DeclarationType.Property);

    public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax method) => FixCompilationError(method, DeclarationType.Method);

    public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node) => FixCompilationError(node, DeclarationType.Field);

    public override SyntaxNode VisitConstructorDeclaration(ConstructorDeclarationSyntax node) => FixCompilationError(node, DeclarationType.Constructor);
    
    public SyntaxNode FixCompilationError(SyntaxNode node, DeclarationType declarationType)
    {
        var location = node.GetLocation().GetStartAndEndLines();

        if (!TryGetCompilationError(location, out List<AnalyzeCompilationErrorModel> errors))
            return node;

        var nodeText = node.ToFullString();

        foreach (var error in errors)
        {
            foreach(var analyze in error.AnalyzeBaseModels)
            {
                var match = Regex.Match(error.CompilationErrorModel.Text, analyze.ErrorText);

                var code = error.CompilationErrorModel.GetCode();
                var lineCode = error.CompilationErrorModel.Location.ToCodeLineString();
                Console.WriteLine(code + " " + Regex.Replace(code, analyze.RegexPattern, analyze.RegexReplacement));
                nodeText = nodeText.Replace(lineCode, lineCode.Replace(code, Regex.Replace(code, analyze.RegexPattern, analyze.RegexReplacement)));
            }
        }

        return ToSyntaxNode(nodeText);
    }

    private SyntaxNode ToSyntaxNode(string code)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
        var getRoot = syntaxTree.GetRoot().DescendantNodes().FirstOrDefault();
        return getRoot;
    }

    public bool TryGetCompilationError((int, int) location, out List<AnalyzeCompilationErrorModel> errorsOut)
    {
        errorsOut = new List<AnalyzeCompilationErrorModel>();

        foreach (var error in _compilationErrors)
        {
            var errorOut = new AnalyzeCompilationErrorModel()
            {
                CompilationErrorModel = error,
                AnalyzeBaseModels = new List<AnalyzeBaseModel>()
            };

            if (location.Item1 <= error.Line && error.Line <= location.Item2)
            {
                var analyzeBaseModel = _analyzeBaseModels.Where(s => Regex.IsMatch(error.Text, s.ErrorText));
                if (analyzeBaseModel != null)
                {
                    errorOut.AnalyzeBaseModels.AddRange(analyzeBaseModel);
                }
            }

            if(errorOut.AnalyzeBaseModels != null && errorOut.AnalyzeBaseModels.Count > 0)
            {
                errorsOut.Add(errorOut);
            }
        }

        return errorsOut.Count > 0;
    }
}
