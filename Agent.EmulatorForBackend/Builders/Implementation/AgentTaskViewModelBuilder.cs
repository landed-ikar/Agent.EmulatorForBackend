using Agent.EmulatorForBackend.Services;
using Agent.EmulatorForBackend.ViewModels;
using Agent.EmulatorForBackend.ViewModels.Implementation;
using Microsoft.AspNetCore.SignalR.Client;

namespace Agent.EmulatorForBackend.Builders.Implementation;
internal class AgentTaskViewModelBuilder : IAgentTaskViewModelBuilder
{
    public AgentTaskViewModelBuilder()
    {
    }
    public IAgentTaskViewModel Build(HubConnection connection) => new AgentTaskViewModel(connection);
}
