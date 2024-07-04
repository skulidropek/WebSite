using Microsoft.Win32;
using WpfApp.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WpfApp.View;

public partial class ChoicePluginsUserControl : UserControl
{
    public ChoicePluginsUserControl()
    {
        InitializeComponent();

        DataContext = new ChoicePluginsViewModel();
    }
}