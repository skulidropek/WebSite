using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using RoslynLibrary.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Core;
using WpfApp.Core.Services;

namespace WpfApp.Models
{
    internal class ManagedSectionOverride : ManagedSection
    {
        public override string Path
        {
            get => ServiceManager.ServiceProvider.GetRequiredService<RegistryService>().GetValue("RustReferensePath") ?? "";
            set
            {
                var registryService = ServiceManager.ServiceProvider.GetRequiredService<RegistryService>();

                if (string.IsNullOrEmpty(registryService.GetValue("RustReferensePath")))
                {
                    registryService.AddValue("RustReferensePath", value);
                    return;
                }

                registryService.SetValue("RustReferensePath", value);
            }
        }
    }
}
