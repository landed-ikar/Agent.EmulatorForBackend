using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Agent.EmulatorForBackend.Controls;

/// <summary>
/// Interaction logic for TagListControl.xaml
/// </summary>
public partial class TagListControl : UserControl
{
    public ObservableCollection<string> ItemsSource { get; set; } = new ObservableCollection<string>();

    public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(nameof(ItemsSource), typeof(ObservableCollection<string>), typeof(TagListControl));
    public TagListControl()
    {
        InitializeComponent();
    }
}
