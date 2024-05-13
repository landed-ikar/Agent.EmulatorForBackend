using Agent.EmulatorForBackend.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Agent.EmulatorForBackend.Controls;

/// <summary>
/// Interaction logic for AgentControl.xaml
/// </summary>
public partial class AgentControl : UserControl
{
    public ICommand DeleteCommand
    {
        get { return (ICommand)GetValue(DeleteCommandProperty); }
        set { SetValue(DeleteCommandProperty, value); }
    }

    public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register(nameof(DeleteCommand), typeof(ICommand), typeof(AgentControl));

    public AgentControl()
    {
        InitializeComponent();
    }

    private void btnDeleteAgent_Click(object sender, RoutedEventArgs e)
    {
        DeleteCommand?.Execute(DataContext);
    }

    private void btnEditAgent_Click(object sender, RoutedEventArgs e)
    {
        EditAgentDialog dlg = new EditAgentDialog(this.DataContext as IAgentViewModel);
        dlg.Owner = Window.GetWindow(this);
        dlg.ShowDialog();
    }
}
