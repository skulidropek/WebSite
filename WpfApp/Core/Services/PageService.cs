using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp.View;

namespace WpfApp.Core.Services
{
    internal class PageService
    {
        private Frame _frame;

        public void Initilization(Frame frame)
        {
            if (_frame != null) return;

            _frame = frame;
        }

        public void OpenRoslyn()
        {
            _frame.Content = ServiceManager.ServiceProvider.GetRequiredService<RoslynUserControl>();
        }

        public void OpenChocePlugins()
        {
            var serviceProvider = ServiceManager.ServiceProvider;
            _frame.Content = serviceProvider.GetRequiredService<ChoicePluginsUserControl>();
        }
    }
}
