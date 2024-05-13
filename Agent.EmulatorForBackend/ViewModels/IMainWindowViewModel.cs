using System.Collections.ObjectModel;

namespace Agent.EmulatorForBackend.ViewModels;
public interface IMainWindowViewModel
{
    public ObservableCollection<IAgentViewModel> Agents { get; set; }
    RelayCommand AddAgentCommand { get; }
    RelayCommand DeleteAgentCommand { get; }
    RelayCommand SaveAgentsCommand { get; }


}
