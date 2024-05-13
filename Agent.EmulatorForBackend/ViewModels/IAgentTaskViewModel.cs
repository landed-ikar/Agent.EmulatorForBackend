using Monq.Core.Models.Agents.Scheduler;

namespace Agent.EmulatorForBackend.ViewModels;
public interface IAgentTaskViewModel
{
    Guid TaskEventId { get; set; }
    AgentTaskInfo TaskInfo { get; set; }
    RelayCommand CompleteOkCommand { get; }
    RelayCommand CompleteErrorCommand { get; }

    event EventHandler<EventArgs> TaskCompleted;
}
