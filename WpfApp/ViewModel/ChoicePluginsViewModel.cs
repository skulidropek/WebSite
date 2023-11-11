using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Win32;
using RoslynLibrary.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp.Core;
using WpfApp.Core.Services;
using WpfApp.Models;

namespace RustErrorsFix.ViewModel
{
    internal class ChoicePluginsViewModel : ViewModelBase
    {
        private readonly LangService _langService;
        private readonly PageService _pageService;
        private readonly ManagedSection _managedSection;

        private bool _roslynReferenseHave => !string.IsNullOrEmpty(_managedSection.Path);

        public string RoslynButtonText => _roslynReferenseHave ? _langService.GetLang("Select") : _langService.GetLang("SelectFolderManaged");

        public string SelectPluginText => _langService.GetLang("SelectPlugin");

        public ICommand RoslynPageOpenCommand { get; private set; }
        public ICommand ResetManagedFolderCommand { get; private set; }

        public ChoicePluginsViewModel()
        {
            _managedSection = ServiceManager.ServiceProvider.GetRequiredService<IOptions<ManagedSection>>().Value;
            _pageService = ServiceManager.ServiceProvider.GetRequiredService<PageService>();
            _langService = ServiceManager.ServiceProvider.GetRequiredService<LangService>();

            RoslynPageOpenCommand = new RelayCommand(RoslynPageOpenCommandExecute);
            ResetManagedFolderCommand = new RelayCommand(ResetManagedFolderCommandExecute, (obj) => !string.IsNullOrEmpty(_managedSection.Path));

            _langService.Subscribe(OnLangChanged);

            _langService.OnLangChangedInvoke();
        }

        private void ResetManagedFolderCommandExecute(object obj)
        {
            _managedSection.Path = "";
            OnPropertyChanged("RoslynButtonText");
        }

        ~ChoicePluginsViewModel()
        {
            _langService.UnSubscribe(OnLangChanged);
        }

        public void OnLangChanged(bool en)
        {
            OnPropertyChanged("SelectPluginText");
            OnPropertyChanged("RoslynButtonText");
        }

        public void RoslynPageOpenCommandExecute(object obj)
        {
            var path = _managedSection.Path;
            if (string.IsNullOrEmpty(path))
            {
                path = GetPathOpenFolderDialog();

                if(string.IsNullOrEmpty(path))
                {
                    MessageBox.Show("Need select Managed folder");
                    return;
                }

                _managedSection.Path = path;
                OnPropertyChanged("RoslynButtonText");
                return;
            }

            _pageService.OpenRoslyn();
        }

        private string GetPathOpenFolderDialog()
        {
            var folderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

            if(folderDialog.ShowDialog() == false)
                return "";

            return folderDialog.SelectedPath;
        }
    }
}
