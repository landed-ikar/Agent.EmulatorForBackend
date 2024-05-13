using Agent.EmulatorForBackend.ViewModels;
using Agent.EmulatorForBackend.ViewModels.Implementation;
using System.Reflection;
using System.Windows;

namespace Agent.EmulatorForBackend;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(IMainWindowViewModel model)
    {
        DataContext = model;
        InitializeComponent();
        Title += " " + Assembly.GetExecutingAssembly().GetName().Version;
    }
}