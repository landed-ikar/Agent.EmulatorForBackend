using Agent.EmulatorForBackend.Models;
using Mapster;
using Monq.Core.Models.Agents.Scheduler;
using Monq.Core.Models.Agents.Types;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using Agent.EmulatorForBackend.ViewModels.Implementation;
using Agent.EmulatorForBackend.ViewModels;
using System.Collections.ObjectModel;

namespace Agent.EmulatorForBackend.Configuration.MapsterProfiles;
public class AgentModelProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<IAgentViewModel, AgentStoredModel>();
        config.NewConfig<IAgentViewModel, AgentInfo>()
            .Map(dst => dst.OperationSystemType, _ => OperationSystemType.Windows)
            .Map(dst => dst.Key, src => src.ApiKey)
            .Map(dst => dst.Ip, _ => GetIPString())
            .Map(dst => dst.Version, _ => GetVersion());

        config.NewConfig<AgentStoredModel, IAgentViewModel>()
            .MapToTargetWith((src, dst) => GetConcreteAgentViewModel(src, dst));
        config.NewConfig<AgentStoredModel, AgentViewModel>();
    }

    static string GetIPString()
    {
        var hostName = Dns.GetHostName();
        var ipEntry = Dns.GetHostEntry(hostName);
        var ip = ipEntry.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);
        return ip?.ToString() ?? string.Empty;
    }

    static string GetVersion() => 
        Assembly.GetEntryAssembly()?.GetName().Version?.ToString()
        ?? string.Empty;

    IAgentViewModel GetConcreteAgentViewModel(AgentStoredModel src, IAgentViewModel dst)
    {
        if (dst is AgentViewModel agentViewModel)
            return src.Adapt(agentViewModel);

        throw new NotImplementedException();
    }
}
