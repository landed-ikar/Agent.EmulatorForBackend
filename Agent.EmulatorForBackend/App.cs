using Agent.EmulatorForBackend;
using System.Windows;

public class App : Application
{
    readonly MainWindow _mainWindow;

    public App(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
        this.MainWindow = _mainWindow;
        //this.ShutdownMode = ShutdownMode.OnLastWindowClose;
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        _mainWindow.Show();
        base.OnStartup(e);
    }
}