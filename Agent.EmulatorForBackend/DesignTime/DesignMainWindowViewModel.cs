using Agent.EmulatorForBackend.ViewModels;
using Monq.Core.Models.Agents.Scheduler;
using System.Collections.ObjectModel;

namespace Agent.EmulatorForBackend.DesignTime;
public class DesignMainWindowViewModel : IMainWindowViewModel
{
    public ObservableCollection<IAgentViewModel> Agents { get; set; } = new();

    public RelayCommand AddAgentCommand => throw new NotImplementedException();
    public RelayCommand DeleteAgentCommand => throw new NotImplementedException();


    public RelayCommand SaveAgentsCommand => throw new NotImplementedException();

    public DesignMainWindowViewModel()
    {
        Agents.Add(new DesignAgentViewModel
        {

        });
        Agents.Add(new DesignAgentViewModel
        {
            Name = "Disconnected Agent",
            Description = "Simple Disconnected Agent",
            ApiKey = "1b7b54dd-282f-4d11-be7a-9086eae9dfd5",
            BaseUri = @"http:\\localhost\",
            SlotsCount = 5,
            ConnectionState = AgentConnectionState.Disconnected,
            Labels = new ObservableCollection<string>() { "Label 1", "Label 2" }
        });
        Agents.Add(new DesignAgentViewModel
        {
            Name = "Connecting Agent",
            Description = "Simple Connecting Agent",
            ApiKey = "6be6b04c-26e8-40d4-a252-6aaa41a837d1",
            BaseUri = @"http:\\localhost\",
            SlotsCount = 5,
            ConnectionState = AgentConnectionState.Connecting,
            Labels = new ObservableCollection<string>() { "Label 1", "Label 2", "Label 3" }
        });
        Agents.Add(new DesignAgentViewModel
        {
            Name = "Connected Agent",
            Description = "Simple Connected Agent",
            ApiKey = "27fb7c84-44c4-4e65-8d23-2a2496eaa0ae",
            BaseUri = @"http:\\localhost\",
            SlotsCount = 5,
            ConnectionState = AgentConnectionState.Connecting,
            Labels = new ObservableCollection<string>() { "Label 1", "Label 2", "Label 3" },
            AgentTasks = new ObservableCollection<IAgentTaskViewModel>
            {
                new DesignAgentTaskViewModel()
                {
                    TaskEventId = Guid.Parse("cf5103a7-0214-4ee3-971c-2ffcb0924645"),
                    TaskInfo = new AgentTaskInfo()
                    {
                        Id = 1,
                        Name = "Task 1",
                    }
                },
                new DesignAgentTaskViewModel()
                {
                    TaskEventId = Guid.Parse("5ecdf2d9-cc5b-420e-b751-7426bccdd102"),
                    TaskInfo = new AgentTaskInfo()
                    {
                        Id = 1,
                        Name = "Task 1",
                    }
                }
            }
        });
    }
}
