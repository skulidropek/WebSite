using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp.Core.Services.Interfaces;

namespace WpfApp.Core.Services
{
    internal class RegistryService
    {
        private const string _name = "RustErrorsFix";
        //private readonly ConfigurationService _configurationService;


        public void AddValue(string valueName, string value)
        {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(_name);
            if (registry == null)
                return;

            registry.SetValue(valueName, value);
            registry.Close();
        }

        public void SetValue(string valueName, string value)
        {
            var registry = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(_name, true);
            if (registry == null)
                return;

            registry.SetValue(valueName, value);
            registry.Close();
        }

        public string GetValue(string valueName)
        {
            using (var registry = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(_name))
            {
                if (registry == null)
                    return "";

                var value = registry?.GetValue(valueName);

                if (value == null)
                    return "";

                return (string)value;
            }
        }
    }
}
