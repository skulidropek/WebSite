using RoslynLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Core.Services.Interfaces;
using WpfApp.Models;

namespace WpfApp.Core.Services
{
    internal class AnalyzeConfigurationService236DevBlog : IAnalyzeConfigurationService
    {
        private readonly Lazy<List<AnalyzeBaseOverrideModel>> _analyzeBaseOverrideModels =
            new Lazy<List<AnalyzeBaseOverrideModel>>(() => new List<AnalyzeBaseOverrideModel>()
            {
                new AnalyzeBaseOverrideModel()
                {
                    ErrorText = @""".+"" не содержит определение для ""ServerInstance""",
                    AnalyzeType = AnalyzeType.Error,
                    DeclarationType = DeclarationType.All,
                    RegexPattern = "ServerInstance",
                    RegexReplacement = "Instance",
                    Description = "Заменяет ServerInstance на Instance",
                },
                new AnalyzeBaseOverrideModel()
                {
                    ErrorText = @"""FileStorage"" не содержит определения ""RemoveExact"", и не удалось найти доступный метод расширения ""RemoveExact"", принимающий тип ""FileStorage"" в качестве первого аргумента",
                    AnalyzeType = AnalyzeType.Error,
                    DeclarationType = DeclarationType.All,
                    RegexPattern = "RemoveExact",
                    RegexReplacement = "Remove",
                    Description = "Заменяет RemoveExact на Remove",
                } 
            });

        public string ConfigurationName => nameof(AnalyzeConfigurationService236DevBlog);

        public Lazy<List<AnalyzeBaseOverrideModel>> AnalyzeBaseOverrideModels => _analyzeBaseOverrideModels;

    }
}
