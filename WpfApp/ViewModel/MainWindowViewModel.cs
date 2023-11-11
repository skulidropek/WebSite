using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Input;
using WpfApp.Core;
using WpfApp.Core.Services;
using WpfApp.Models;

namespace WpfApp.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly PageService _pageService;
        private readonly LangService _langService;
        private string _langImagePath;

        public ICommand BoostyCommand { get; private set; }
        public ICommand DiscordCommand { get; private set; }
        public ICommand YoutubeCommand { get; private set; }
        public ICommand QuitCommand { get; private set; }
        public ICommand ChangeLanguageCommand { get; private set; }

        public string LangImagePath
        {
            get => _langImagePath;
            set
            {
                _langImagePath = value;
                OnPropertyChanged("LangImagePath");
            }
        }

        public MainWindowViewModel()
        {
            _langService = ServiceManager.ServiceProvider.GetRequiredService<LangService>();
            _pageService = ServiceManager.ServiceProvider.GetRequiredService<PageService>();
            _pageService.OpenChocePlugins();

            BoostyCommand = new RelayCommand(BoostyCommandExecute);
            DiscordCommand = new RelayCommand(DiscordCommandExecute);
            YoutubeCommand = new RelayCommand(YoutubeCommandExecute);
            QuitCommand = new RelayCommand(QuitCommandExecute);
            ChangeLanguageCommand = new RelayCommand(ChangeLanguageCommandExecute);

            _langService.Subscribe((lang) =>
            {
                LangImagePath = lang ? LangService.EnPathLang : LangService.RuPathLang;
            });

            _langService.OnLangChangedInvoke();
        }

        public void BoostyCommandExecute(object sender)
        {
            Process.Start("https://boosty.to/skulidropek");
        }

        public void DiscordCommandExecute(object sender)
        {
            Process.Start("https://discord.gg/CBqDuqDWvS");
        }

        public void YoutubeCommandExecute(object sender)
        {
            Process.Start("https://www.youtube.com/@skulidropek607");
        }

        public void QuitCommandExecute(object sender)
        {
            Environment.Exit(0);
        }

        public void ChangeLanguageCommandExecute(object sender)
        {
            _langService.Change();
        }
    }
}
