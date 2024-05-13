using Microsoft.AspNetCore.SignalR.Client;
using Monq.Core.Models.Agents.Scheduler;

namespace Agent.EmulatorForBackend.Services;
public interface IConnectionManager
{
    HubConnection CreateHubConnection(string baseUri);
    Task<AgentRegistrationResultType> EstablishConnection(HubConnection hubConnection, AgentInfo agentInfo, CancellationToken cancellationToken);
}
