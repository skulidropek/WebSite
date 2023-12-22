using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoslynLibrary.Extensions;
using WpfApp.Core.Services;
using WpfApp.Models;
using WpfApp.View;
using RoslynLibrary.Sections;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WpfApp.Core.Services.Interfaces;

namespace WpfApp.Core
{
    internal class ServiceManager
    {
        public static IServiceProvider ServiceProvider { get; set; }

        static ServiceManager()
        {
            var services = new ServiceCollection();

            services.AddPluginAnalyzer();

            services.AddSingleton<PageService>();
            services.AddSingleton<LangService>();
            services.AddSingleton<IConfigurationService, ConfigurationService>();

            services.AddScoped<RegistryService>();
            services.PostConfigure<ManagedSection>(s =>
            {
                var overrideSection = new ManagedSectionOverride();
                s.Path = overrideSection.Path;
            });

            services.AddTransient<ChoicePluginsUserControl>();
            services.AddTransient<RoslynUserControl>();

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
