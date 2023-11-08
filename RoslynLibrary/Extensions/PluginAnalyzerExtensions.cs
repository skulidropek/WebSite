using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoslynLibrary.Sections;
using RoslynLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynLibrary.Extensions
{
    public static class PluginAnalyzerExtensions
    {
        public static IServiceCollection AddPluginAnalyzer(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var managedSection = new ManagedSection();

            services.AddScoped<PluginDiagnosticsAnalyzerService>();
            services.AddScoped<PluginFixService>();
            services.AddScoped<CodeErrorFixerService>();

            services.AddScoped<CSharpDecompileService>();

            return services;
        }
    }
}
