using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Core.Services
{
    internal class RegistryService
    {
        const string RegistryName = "RustErrorsFix2";

        public void AddValue(string valueName, string value)
        {
            var registry = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegistryName);
            if (registry == null)
                return;

            registry.SetValue(valueName, value);
            registry.Close();
        }

        public void SetValue(string valueName, string value)
        {
            var registry = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryName, true);
            if (registry == null)
                return;

            registry.SetValue(valueName, value);
            registry.Close();
        }

        public string GetValue(string valueName)
        {
            using (var registry = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryName))
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
