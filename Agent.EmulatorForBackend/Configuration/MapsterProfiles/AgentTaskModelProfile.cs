using Agent.EmulatorForBackend.Models;
using Mapster;
using Monq.Core.Models.Agents.Scheduler;
using Monq.Core.Models.Agents.Types;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using Agent.EmulatorForBackend.ViewModels.Implementation;

namespace Agent.EmulatorForBackend.Configuration.MapsterProfiles;
public class AgentTaskModelProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AgentTaskEvent, AgentTaskViewModel>()
            .Map(dst => dst.TaskEventId, src => src.Id)
            .Map(dst => dst.TaskInfo, src => src.TaskInfo);
    }
}
