using Agent.EmulatorForBackend.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;

namespace Agent.EmulatorForBackend;
public interface IAgentTaskViewModelBuilder
{
    IAgentTaskViewModel Build(HubConnection connection);
}
