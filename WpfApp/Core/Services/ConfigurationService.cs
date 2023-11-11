using RoslynLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Core.Services
{
    internal class ConfigurationService
    {
        public List<AnalyzeBaseOverrideModel> AnalyzeBaseOverrideModels = new List<AnalyzeBaseOverrideModel>()
        {
            //new AnalyzeBaseOverrideModel()
            //{
            //    ErrorText = ".е удается (неявно\\s)?преобразовать (тип|из) \"(uint|ulong)\" в \"(NetworkableId|ItemId|ItemContainerId)\"",
            //    AnalyzeType = AnalyzeType.All,
            //    DeclarationType = DeclarationType.All,
            //    RegexPattern = "(uint|UInt32)",
            //    RegexReplacement = "ulong",
            //    Description = "",
            //},

            new AnalyzeBaseOverrideModel()
            {
                ErrorText = @"Не удалось найти тип или имя пространства имен ""Apex""",
                AnalyzeType = AnalyzeType.All,
                DeclarationType = DeclarationType.All,
                RegexPattern = "using Apex;",
                RegexReplacement = "",
                Description = "Удаляет using Apex;",
            },
            new AnalyzeBaseOverrideModel()
            {
                ErrorText = @""".+<.+>"" не содержит определения ""ForEach""",
                AnalyzeType = AnalyzeType.All,
                DeclarationType = DeclarationType.All,
                RegexPattern = "$this",
                RegexReplacement = "using System.Linq;\n$this",
                Description = "Добавляет using System.Linq в начало кода",
            },
            new AnalyzeBaseOverrideModel()
            {
                ErrorText = @".е удается (неявно\s)?преобразовать (из|тип) ""uint"" в ""(NetworkableId|ItemId|ItemContainerId|ulong)""",
                AnalyzeType = AnalyzeType.All,
                DeclarationType = DeclarationType.All,
                RegexPattern = "(uint|UInt32)",
                RegexReplacement = "ulong",
                Description = "Заменяет uint на ulong",
            },
            new AnalyzeBaseOverrideModel()
            {
                ErrorText = ".е удается (неявно\\s)?преобразовать (тип|из) \"(NetworkableId|ItemId|ItemContainerId|ulong)\" в \"uint\"",
                AnalyzeType = AnalyzeType.All,
                DeclarationType = DeclarationType.All,
                RegexPattern = "(uint|UInt32)",
                RegexReplacement = "ulong",
                Description = "Заменяет uint на ulong",
            },
            new AnalyzeBaseOverrideModel()
            {
                ErrorText = @"Оператор ""\?\?"" невозможно применить к операнду типа ""(NetworkableId|ItemId|ItemContainerId)\??"" и ""(int|uint|ulong)""",
                AnalyzeType = AnalyzeType.All,
                DeclarationType = DeclarationType.All,
                RegexPattern = "\\.net\\.ID",
                RegexReplacement = ".net.ID.Value",
                Description = "Заменяет .net.ID на .net.ID.Value",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Оператор ""(.{1,2})"" невозможно применить к операнду типа ""(NetworkableId|ItemId|ItemContainerId)\??"" и ""(ulong|uint)\??"".",
                AnalyzeType = AnalyzeType.Error,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.net??\.ID",
                RegexReplacement = ".net.ID.Value",
                Description = "Заменяет .net.ID на .net.ID.Value",
            },
            new AnalyzeBaseOverrideModel()
            {
                ErrorText = @"Ни одна из перегрузок метода ""(Factor|Test|GetWaterDepth|GetOverallWaterDepth|GetWaterInfo)"" не принимает \d аргументов",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"$errorGroup1\((.+)\)",
                RegexReplacement = "$errorGroup1($1, false)",
                Description = "Добавляет false в метод класса WaterLevel",
            }, 
            new AnalyzeBaseOverrideModel()
            {
                ErrorText = @"Отсутствует аргумент, соответствующий требуемому параметру ""(waves|volumes)"" из ""WaterLevel\.(Factor|Test|GetWaterDepth|GetOverallWaterDepth|GetWaterInfo)",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"$errorGroup1\((.+)\)",
                RegexReplacement = "$errorGroup1($1, false)",
                Description = "Добавляет false в метод класса WaterLevel",
            },
            new AnalyzeBaseOverrideModel()
            {
                ErrorText = @"Отсутствует аргумент, соответствующий требуемому параметру ""altMove"" из "".+\.GetIdealContainer\(BasePlayer, Item, bool\)""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"GetIdealContainer\((.+)\)",
                RegexReplacement = "GetIdealContainer($1, true)",
                Description = "Добавляет в метод GetIdealContainer булево true",
            }, 
            //new AnalyzeBaseOverrideModel()
            //{
            //    ErrorText = @""".+"": не все пути к коду возвращают значение.",
            //    AnalyzeType = AnalyzeType.Method,
            //    DeclarationType = DeclarationType.All,
            //    RegexPattern = @"GetIdealContainer\((.+)\)",
            //    RegexReplacement = "GetIdealContainer($1, true)",
            //    Description = "Добавляет в метод GetIdealContainer булево true",
            //},  
            new AnalyzeBaseOverrideModel()
            {
                ErrorText = @"""ItemCraftTask"" не содержит определения ""owner"".+",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"GetIdealContainer\((.+)\)",
                RegexReplacement = "GetIdealContainer($1, true)",
                Description = "Добавляет в метод GetIdealContainer булево true",
            },

            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = "Не удается неявно преобразовать тип \".+<(NetworkableId|ItemId|ItemContainerId)>\" в \".+<(uint|ulong)>\"",
                AnalyzeType = AnalyzeType.Error,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.net\.ID",
                RegexReplacement = ".net.ID.Value",
                Description = "Заменяет .net.ID на .net.ID.Value",
            },
        };
    }
}
