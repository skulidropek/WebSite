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
    internal class ConfigurationService : IConfigurationService
    {
        IEnumerable<AnalyzeBaseOverrideModel> IConfigurationService.AnalyzeBaseOverrideModels { get; } = new List<AnalyzeBaseOverrideModel>()
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
                ErrorText = @"Оператор ""(.{1,2})"" невозможно применить к операнду типа ""(NetworkableId|ItemId|ItemContainerId)\??"" и ""(int|ulong|uint)\??""",
                AnalyzeType = AnalyzeType.Error,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.(subEntity|net??\.ID|uid)",
                RegexReplacement = "$1.Value",
                Description = @"Заменяет \.(subEntity|net??\.ID|uid) на $1.Value",
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
//            new AnalyzeBaseOverrideModel()
//            {
//                ErrorText = @"""ItemCraftTask"" не содержит определения ""owner"".+",
//                AnalyzeType = AnalyzeType.Line,
//                DeclarationType = DeclarationType.All,
//                RegexPattern = @"
//var code = ""$this"";
//if(code.Contains(""itemCrafterOwner""))
//{
//  net.ID
//}
//",
//                RegexReplacement = "GetIdealContainer($1, true)",
//                Description = "Добавляет в метод GetIdealContainer булево true",
//            },

            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = "Невозможно присвоить значение свойству или индексатору \"BasePlayer.serverInput\" — доступ только для чтения",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"(.+)\.serverInput\s=\s(.+);",
                RegexReplacement = @"
$1.serverInput.current = $2.current;
$1.serverInput.previous = $2.previous;
 ",
                Description = "Заменяет присваивание чего либо в serverInput на элементы serverInput. Например serverInput.current = serverInput1.current;",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = "\"BaseRidableAnimal\" не содержит определения \"inventory\", и не удалось найти доступный метод расширения",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @".inventory",
                RegexReplacement = @".storageInventory",
                Description = "Заменяет присваивание чего либо в serverInput на элементы serverInput. Например serverInput.current = serverInput1.current;",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = "\"BaseProjectile.Magazine\" не содержит определения \"TryReload\", и не удалось найти доступный метод расширения",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @".primaryMagazine.TryReload",
                RegexReplacement = @".ServerTryReload",
                Description = @"Заменяет в коде "".primaryMagazine.TryReload"" на "".ServerTryReload""",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = "не удается преобразовать из \"ItemContainer\" в \"BaseEntity\"",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.TakeFrom\(([^.]+)\.inventory",
                RegexReplacement = @".TakeFrom($1, $1.inventory",
                Description = @"inventory обычно берёться из типа который явлеятся baseEntity значит надо взять baseEntity.inventory и подставить в TakeFrom как первый элемент baseEntity",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = "\"Layer\" не содержит определение для \"Debris\"",
                AnalyzeType = AnalyzeType.Error,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"Debris",
                RegexReplacement = @"Physics_Debris",
                Description = @"Заменяет Debris на Physics_Debris",
            },  
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = "\"BaseProjectile.Magazine\" не содержит определения \"SwitchAmmoTypesIfNeeded\", и не удалось найти доступный метод расширения",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @".primaryMagazine.",
                RegexReplacement = @".",
                Description = @"Нужно удалить primaryMagazine из кода",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Отсутствует аргумент, соответствующий требуемому параметру ""dt"" из ""AutoTurret.UpdateAiming\(float\)""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"UpdateAiming\(\)",
                RegexReplacement = @"UpdateAiming(1)",
                Description = @"Если UpdateAiming не имеет параметров то добавляет значение 1 для параметра dt",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"не удается преобразовать из ""bool"" в ""ItemContainer""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"(GiveItem\(.+,)(.+),([^)]+)\)",
                RegexReplacement = @"$1$3,$2)",
                Description = @"",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Не удается неявно преобразовать тип ""ulong"" в ""string""",
                AnalyzeType = AnalyzeType.Error,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"$this",
                RegexReplacement = @"$this.ToString()",
                Description = @"Добавляет к текущему коду .ToString()",
            },    
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""KeyValuePair<ulong, ApprovedSkinInfo>"" не содержит определения ""(.+)"", и не удалось найти доступный метод расширения",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"(\.$errorGroup1)",
                RegexReplacement = @".Value$1",
                Description = @"",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"не удается преобразовать из ""string"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @".ToString\(\)",
                RegexReplacement = @"",
                Description = @"",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"не удается преобразовать из ""string"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @".ToString\(\)",
                RegexReplacement = @"",
                Description = @"Удаляет .ToString()",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Требуется "";""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"$this",
                RegexReplacement = @"$this;",
                Description = @"Добавляет "";""",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Требуется "";""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"$this",
                RegexReplacement = @"$this;",
                Description = @"Добавляет "";""",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Аргументы типа для метода ""Array.Resize<T>\(ref T\[\], int\)"" не могут определяться по использованию",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"$this",
                RegexReplacement = @"",
                Description = @"Удаляет данный код",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""List<.+>"" не содержит определения ""Length"", и не удалось найти доступный метод расширения",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.Length",
                RegexReplacement = @".Count",
                Description = @"Заменяет .Length на .Count",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Не удается неявно преобразовать тип ""ListHashSet<.+>"" в ""System.Collections.Generic.List<(.+)>""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"([\d\w]+\s*=)\s*(.+);",
                RegexReplacement = @"$1new List<{$errorGroup1}>($2);",
                Description = @"",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Не удалось найти тип или имя пространства имен ""BaseCar""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"BaseCar",
                RegexReplacement = @"$BasicCar",
                Description = @"Заменяет BaseCar на $BasicCar",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""ListHashSet<.+>"" не содержит определения ""Find""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.Find(\(.+\));",
                RegexReplacement = @".FirstOrDefault$1;",
                Description = @"",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Не удалось найти тип или имя пространства имен ""NPCPlayerApex""",
                AnalyzeType = AnalyzeType.All,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"NPCPlayerApex",
                RegexReplacement = @"BaseNpc",
                Description = @"Заменяет NPCPlayerApex на BaseNpc",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Имя ""NPCPlayerApex"" не существует в текущем контексте.",
                AnalyzeType = AnalyzeType.All,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"NPCPlayerApex",
                RegexReplacement = @"BaseNpc",
                Description = @"Заменяет NPCPlayerApex на BaseNpc",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""BaseBoat"" не содержит определения ""myRigidBody""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"myRigidBody",
                RegexReplacement = @"rigidBody",
                Description = @"Заменяет myRigidBody на rigidBody",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""TriggerRadiation"" не содержит определения ""radiationSize""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"$this",
                RegexReplacement = @"",
                Description = @"Удаляет строчку",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Не удалось найти тип или имя пространства имен ""PlantEntity""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"PlantEntity",
                RegexReplacement = @"GrowableEntity",
                Description = @"Заменяет PlantEntity на GrawebleEntity",
            },   
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""ListHashSet<.+>"" не содержит определения ""ToArray""",
                AnalyzeType = AnalyzeType.All,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"$this",
                RegexReplacement = @"using System.Linq;\n$this",
                Description = @"Добавляет System.Linq",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""RelationshipManager"" не содержит определения ""playerGangs""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"playerGangs",
                RegexReplacement = @"playerToTeam",
                Description = @"Заменить playerGangs на playerToTeam",
            },   
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""RelationshipManager"" не содержит определения ""playerGangs""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"playerGangs",
                RegexReplacement = @"playerToTeam",
                Description = @"Заменить playerGangs на playerToTeam",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Не удается преобразовать группу методов ""Value"" в тип, не являющийся делегатом ""ulong"". Предполагалось вызывать этот метод?",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.Value\.Value",
                RegexReplacement = @".Value",
                Description = @"Убирает лишний .Value",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""ulong"" является тип, который недопустим в данном контексте",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"(.+=) \(new ([\d\w]+)\(UInt64\)\)([^;]+);",
                RegexReplacement = @"$1 new $2($3);",
                Description = @"",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""ulong"" является тип, который недопустим в данном контексте",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"(.+=) \(new ([\d\w]+)\(UInt64\)\)([^;]+);",
                RegexReplacement = @"$1 new $2($3);",
                Description = @"",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"не удается преобразовать из ""ulong"" в ""(NetworkableId|ItemId|ItemContainerId)""",
                AnalyzeType = AnalyzeType.Error,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.Value",
                RegexReplacement = @"",
                Description = @"Удаляет лишний Value",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Не удалось изменить возвращаемое значение "".+.uid"", т. к. оно не является переменной",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.uid\.Value\s*=\s*(.+);",
                RegexReplacement = @".uid = new NetworkableId($1);",
                Description = @"",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Не удается преобразовать тип ""System.Collections.Generic.KeyValuePair<.+, BaseNetworkable>"" в ""System.Collections.Generic.KeyValuePair<ulong, BaseNetworkable>""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"KeyValuePair<.+,\s*BaseNetworkable>",
                RegexReplacement = @"var;",
                Description = @"Заменяет тип на var",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Не удается преобразовать тип ""System.Collections.Generic.KeyValuePair<.+, BaseNetworkable>"" в ""System.Collections.Generic.KeyValuePair<ulong, BaseNetworkable>""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"KeyValuePair<.+,\s*BaseNetworkable>",
                RegexReplacement = @"var;",
                Description = @"Заменяет тип на var",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Недопустимый термин ""(.+)"" в выражении",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\(\({$errorGroup1}\)(.+)\)",
                RegexReplacement = @"($1)",
                Description = @"",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Ни одна из перегрузок метода ""Add"" не принимает 2 аргументов",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"Add\((.+),.+\)",
                RegexReplacement = @"Add($1)",
                Description = @"",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""MotorRowboat"" не содержит определения ""dying""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @".dying.",
                RegexReplacement = @".IsDying",
                Description = @"Заменяет .dying. на .IsDying",
            },  
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""ulong"" не содержит определения ""Value""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"(^|\s)ID\.Value",
                RegexReplacement = @"ID",
                Description = @"Удаляет .Value",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""TrainEngine"" не содержит определения ""decayDuration""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.decayDuration",
                RegexReplacement = @".decayingFor",
                Description = @"Заменяет .decayDuration на .decayingFor",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""JunkPile"" не содержит определения ""PlayersNearby""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @".*PlayersNearby.*",
                RegexReplacement = @"",
                Description = @"Удаляет PlayersNearby",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @".е удается (неявно\s)?преобразовать (из|тип) ""BaseHelicopter"" в ""PatrolHelicopter""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"BaseHelicopter",
                RegexReplacement = @"PatrolHelicopter",
                Description = @"Заменяет BaseHelicopter на PatrolHelicopter",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""Minicopter"" является тип, который недопустим в данном контексте",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"(Minicopter,",
                RegexReplacement = @"(MiniCopter,",
                Description = @"Заменяет ""(Minicopter,"" на ""(MiniCopter,""",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""Имя ""Net"" не существует в текущем контексте",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"(?<!Network\.)Net\.sv",
                RegexReplacement = @"Network.Net.sv",
                Description = @"Заменяет ""(?<!Network\.)Net\.sv"" на ""Network.Net.sv""",
            },  
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Ни одна из перегрузок метода ""CanMoveTo"" не принимает 3 аргументов",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"(\.CanMoveTo\([^,)]+,[^,)]+),[^,)]+\)",
                RegexReplacement = @"$1)",
                Description = @"Заменяет ""(\.CanMoveTo\([^,)]+,[^,)]+),[^,)]+\)"" на ""$1)""",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""Minicopter"" не содержит определения ""waterSample""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.waterSample\.",
                RegexReplacement = @".engineController.waterloggedPoint.",
                Description = @"Заменяет ""\.waterSample\."" на "".engineController.waterloggedPoint.""",
            },
            new AnalyzeBaseOverrideModel()
            {
                ErrorText = @"""SupplyDrop"" не содержит определения ""parachute""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.parachute",
                RegexReplacement = @".ParachuteRoot.ToBaseEntity()",
                Description = @"Заменяет "".parachute"" на "".ParachuteRoot.ToBaseEntity()""",
            },
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""BaseProjectile.Magazine"" не содержит определения ""Reload""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"\.Reload",
                RegexReplacement = @".TryReload",
                Description = @"Заменяет "".Reload"" на "".TryReload""",
            }, 
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"Не удается неявно преобразовать тип ""PatrolHelicopter"" в ""BaseHelicopter""",
                AnalyzeType = AnalyzeType.Line,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"BaseHelicopter",
                RegexReplacement = @"PatrolHelicopter",
                Description = @"Заменяет ""BaseHelicopter"" на ""PatrolHelicopter""",
            },  
            new AnalyzeBaseOverrideModel()
            { 
                ErrorText = @"""BaseHelicopter"" не содержит определения "".+"", и не удалось найти доступный метод расширения",
                AnalyzeType = AnalyzeType.All,
                DeclarationType = DeclarationType.All,
                RegexPattern = @"BaseHelicopter",
                RegexReplacement = @"PatrolHelicopter",
                Description = @"Заменяет ""BaseHelicopter"" на ""PatrolHelicopter""",
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
