using Agent.EmulatorForBackend.Services;
using Agent.EmulatorForBackend.ViewModels;
using Agent.EmulatorForBackend.ViewModels.Implementation;

namespace Agent.EmulatorForBackend.Builders.Implementation;
internal class AgentViewModelBuilder : IAgentViewModelBuilder
{
    readonly IConnectionManager _connectionManager;
    readonly IAgentTaskViewModelBuilder _agentTaskBuilder;

    public AgentViewModelBuilder(IConnectionManager connectionManager, IAgentTaskViewModelBuilder agentTaskBuilder)
    {
        _connectionManager = connectionManager;
        _agentTaskBuilder = agentTaskBuilder;
    }
    public IAgentViewModel Build() => new AgentViewModel(_connectionManager, _agentTaskBuilder);
}
