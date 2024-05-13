using Agent.EmulatorForBackend.ViewModels;
using Agent.EmulatorForBackend.ViewModels.Implementation;

namespace Agent.EmulatorForBackend;
public interface IAgentViewModelBuilder
{
    IAgentViewModel Build();
}
