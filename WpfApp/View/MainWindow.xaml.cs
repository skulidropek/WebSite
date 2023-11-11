using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Core.Services;
using WpfApp.Core;
using WpfApp.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var pageService = ServiceManager.ServiceProvider.GetRequiredService<PageService>();
            pageService.Initilization(FrameMain);
            DataContext = new MainWindowViewModel();
        }

        private void CloseUI(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BorderTopSection_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
