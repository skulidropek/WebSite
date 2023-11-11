using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using WpfApp.ViewModel;

namespace WpfApp.View;

public partial class RoslynUserControl : UserControl
{
    public RoslynUserControl()
    {
        InitializeComponent();
        DataContext = new RoslynViewModel();
    }
}