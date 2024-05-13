using Agent.EmulatorForBackend.ViewModels;
using System.Collections.ObjectModel;

namespace Agent.EmulatorForBackend.DesignTime;
public class DesignAgentViewModel : IAgentViewModel
{
    public string Name { get; set; } = "Agent Name";

    public string Description { get; set; } = "Agent Description";

    public string ApiKey { get; set; } = Guid.NewGuid().ToString();

    public string BaseUri { get; set; } = "localhost";

    public int SlotsCount { get; set; } = 15;

    public AgentConnectionState ConnectionState { get; set; } = AgentConnectionState.Connected;

    public ObservableCollection<string> Labels { get; set; } = new ObservableCollection<string>() { "Label_1", "Label_2", "Label_3"};

    public ObservableCollection<IAgentTaskViewModel> AgentTasks { get; set; } = new ObservableCollection<IAgentTaskViewModel>();

    public RelayCommand ConnectCommand => throw new NotImplementedException();

    public RelayCommand DisconnectCommand => throw new NotImplementedException();
}
