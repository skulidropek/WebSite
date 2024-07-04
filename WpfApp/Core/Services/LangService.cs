using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Extensions;

namespace WpfApp.Core.Services
{
    internal class LangService
    {
        private Dictionary<string, string> _langsRu = new Dictionary<string, string>()
        {
            { "Select", "ВЫБРАТЬ" },
            { "SelectPlugin", "ВЫБЕРИТЕ ПЛАГИН" },
            { "AnalyzeConfigurationService236DevBlogSelectFolderManaged", "ВЫБЕРИТЕ ПАПКУ MANAGED 236 DEV BLOG" },
            { "AnalyzeConfigurationServiceLastDevBlogSelectFolderManaged", "ВЫБЕРИТЕ ПАПКУ MANAGED ПОСЛЕДНИЙ DEV BLOG" },
            { "OurFriends", "НАШИ ДРУЗЬЯ" },
            { "Back", "НАЗАД" },
            { "NotSelectFile", "Вы не выбрали файл" },
            { "PluginReady", "Плагин готов!" },
            { "ErrorsPlugin", "ОШИБКИ В ПЛАГИНЕ" },
            { "FixSelection", "ВЫБОР ФИКСА" },
            { "Fix", "ФИКС" },
            { "Roslyn", "Рослин" },
            { "AnalyzeConfigurationService236DevBlogButton", "Фикс 236 dev blog" },
            { "AnalyzeConfigurationServiceLastDevBlogButton", "Фикс последний dev blog" }
        };

        private Dictionary<string, string> _langsEn = new Dictionary<string, string>()
        {
            { "Select", "CHOOSE" },
            { "SelectPlugin", "CHOOSE PLUGIN" },
            { "AnalyzeConfigurationService236DevBlogSelectFolderManaged", "CHOOSE FOLDER MANAGED 236 DEV BLOG" },
            { "AnalyzeConfigurationServiceLastDevBlogSelectFolderManaged", "CHOOSE FOLDER MANAGED LAST DEV BLOG" },
            { "OurFriends", "OUR FRIENDS" },
            { "Back", "BACK" },
            { "NotSelectFile", "You have not selected a file" },
            { "PluginReady", "Plugin is ready!" },
            { "ErrorsPlugin", "ERRORS IN THE PLUGIN" },
            { "FixSelection", "FIX SELECTION" },
            { "Fix", "FIX" },
            { "AnalyzeConfigurationService236DevBlogButton", "Fix 236 dev blog" },
            { "AnalyzeConfigurationServiceLastDevBlogButton", "Fix last dev blog" }
        };

        private bool _en;

        private Action<bool> OnLangChanged = delegate { };

        public const string RuPathLang = "/wwwroot/Images/BtnLanguage.png";
        public const string EnPathLang = "/wwwroot/Images/eng.png";

        public LangService()
        {
            var registryService = ServiceManager.ServiceProvider.GetRequiredService<RegistryService>();

            if (!Directory.Exists("Lang"))
            {
                Directory.CreateDirectory("Lang");
            }

            if (System.IO.File.Exists("Lang\\Ru.json"))
                _langsRu = JsonFileSerializer.Deserialize<Dictionary<string, string>>("Lang\\Ru.json");
            else
                JsonFileSerializer.Serialize("Lang\\Ru.json", _langsRu);

            if (System.IO.File.Exists("Lang\\En.json"))
                _langsRu = JsonFileSerializer.Deserialize<Dictionary<string, string>>("Lang\\En.json");
            else
                JsonFileSerializer.Serialize("Lang\\En.json", _langsRu);

            var lang = registryService.GetValue("Lang");

            if (string.IsNullOrEmpty(lang))
            {
                registryService.AddValue("Lang", "En");
            }

            lang = registryService.GetValue("Lang");

            _en = lang == "En";
        }

        public void OnLangChangedInvoke()
        {
            OnLangChanged.Invoke(_en);
        }

        public void Subscribe(Action<bool> action)
        {
            OnLangChanged += action;
        }

        public void UnSubscribe(Action<bool> action)
        {
            OnLangChanged -= action;
        }

        public void Change()
        {
            _en = !_en;

            var registryService = ServiceManager.ServiceProvider.GetRequiredService<RegistryService>();
            registryService.SetValue("Lang", _en ? "En" : "Ru");
            OnLangChanged?.Invoke(_en);
        }

        public string GetLang(string lang)
        {
            if (_en)
            {
                if (_langsEn.ContainsKey(lang))
                    return _langsEn[lang];

                return lang;
            }

            if(_langsRu.ContainsKey(lang))
                return _langsRu[lang];

            return lang;
        }
    }
}
