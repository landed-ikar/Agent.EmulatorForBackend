using System.Collections.ObjectModel;

namespace Agent.EmulatorForBackend.ViewModels;
public interface IAgentViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ApiKey { get; set; }
    public string BaseUri { get; set; }
    public int SlotsCount { get; set; }
    public AgentConnectionState ConnectionState { get; set; }
    public ObservableCollection<string> Labels { get; set; }
    public ObservableCollection<IAgentTaskViewModel> AgentTasks { get; set; }
    public RelayCommand ConnectCommand { get; }
    public RelayCommand DisconnectCommand { get; }

}
