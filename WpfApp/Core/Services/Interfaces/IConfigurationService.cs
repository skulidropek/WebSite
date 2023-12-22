using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Models;

namespace WpfApp.Core.Services.Interfaces
{
    internal interface IConfigurationService
    {
        IEnumerable<AnalyzeBaseOverrideModel> AnalyzeBaseOverrideModels { get; }
    }
}
