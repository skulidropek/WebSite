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
        _compilationErrors = await _pluginDiagnosticsAnalyzer.AnalyzeCompilationAsync(node.SyntaxTree);

        if (_compilationErrors.Count() == 0)
            return node;

        List<AnalyzeBaseModel> analyzeAllModels = new List<AnalyzeBaseModel>();
        List<AnalyzeBaseModel> analyzeBaseNotAllModels = new List<AnalyzeBaseModel>();

        foreach(var analyzeBaseModel in analyzeBaseModels)
        {
            if (analyzeBaseModel.AnalyzeType == AnalyzeType.All)
            {
                analyzeAllModels.Add(analyzeBaseModel);
                continue;
            }

            analyzeBaseNotAllModels.Add(analyzeBaseModel);
        }

        _analyzeBaseModels = analyzeAllModels;

        foreach(var analysisBaseModel in analyzeAllModels)
        {
            node = FixCompilationError(node, DeclarationType.All);
        }

        if(analyzeBaseNotAllModels == null || analyzeBaseNotAllModels.Count == 0)
            return node;

        _analyzeBaseModels = analyzeBaseNotAllModels;

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
            foreach(var analyze in error.AnalyzeBaseModels.Where(s => 
                                        declarationType == DeclarationType.All ||
                                        s.DeclarationType == declarationType)
                )
            {
                var regexPattern = analyze.RegexPattern;
                var regexReplacement = analyze.RegexReplacement;

                var match = Regex.Match(error.CompilationErrorModel.Text, analyze.ErrorText);

                for (int i = 0; i < match.Groups.Count; i++)
                {
                    regexPattern = regexPattern.Replace($"$errorGroup{i}", match.Groups[i].Value);
                    regexReplacement = regexReplacement.Replace($"$errorGroup{i}", match.Groups[i].Value);
                }

                switch (analyze.AnalyzeType)
                {
                    case AnalyzeType.Error:
                        {
                            var code = error.CompilationErrorModel.Location.ToCodeLocationString();

                            var lineCode = error.CompilationErrorModel.Location.ToCodeLineString();
                            nodeText = nodeText.Replace(lineCode, lineCode.Replace(code, Regex.Replace(code, regexPattern, regexReplacement)));
                        }
                        break;

                    case AnalyzeType.Line:
                        {
                            var code = error.CompilationErrorModel.Location.ToCodeLineString();
                            nodeText = nodeText.Replace(code, Regex.Replace(code, regexPattern, regexReplacement));
                        }
                        break;
                    
                    case AnalyzeType.Method:
                    case AnalyzeType.All:
                        nodeText = Regex.Replace(nodeText, regexPattern, regexReplacement);
                        break;

                }
            }
        }

        return ToSyntaxNode(nodeText);
    }

    private SyntaxNode ToSyntaxNode(string code)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);
        return syntaxTree.GetRoot();
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
