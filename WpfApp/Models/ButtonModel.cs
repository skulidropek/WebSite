using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Core;
using WpfApp.Core.Services;
using WpfApp.Core.Services.Interfaces;

namespace WpfApp.Models
{
    internal class ButtonModel : ViewModelBase
    {
        private readonly LangService _langService;
        private readonly RegistryService _registryService;
        private readonly AnalyzeConfigurationService _analyzeConfigurationService;

        public ButtonModel(AnalyzeConfigurationService analyzeConfigurationService)
        {
            _analyzeConfigurationService = analyzeConfigurationService;
            _langService = ServiceManager.ServiceProvider.GetRequiredService<LangService>();
            _registryService = ServiceManager.ServiceProvider.GetRequiredService<RegistryService>();
        }

        public AnalyzeConfigurationService AnalyzeConfigurationService => _analyzeConfigurationService;
        public string ButtonText => _langService.GetLang(AnalyzeConfigurationService.ConfigurationName + (string.IsNullOrWhiteSpace(_registryService.GetValue(AnalyzeConfigurationService.ConfigurationName + "RustReferensePath")) ? "SelectFolderManaged" : "Button"));
        public Visibility ButtonResetVisibility => string.IsNullOrWhiteSpace(_registryService.GetValue(AnalyzeConfigurationService.ConfigurationName + "RustReferensePath")) ? Visibility.Hidden : Visibility.Visible;
    
        public void UpdateUI()
        {
            OnPropertyChanged(nameof(ButtonText));
            OnPropertyChanged(nameof(ButtonResetVisibility));
        }
    }
}
