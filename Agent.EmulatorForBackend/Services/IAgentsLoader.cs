using Agent.EmulatorForBackend.ViewModels;

namespace Agent.EmulatorForBackend.Services;
public interface IAgentsLoader
{
    Task<IEnumerable<IAgentViewModel>> Load();
    Task Save(IEnumerable<IAgentViewModel> agents);
}
